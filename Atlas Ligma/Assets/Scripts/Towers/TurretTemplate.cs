using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType {ground, air, sea};

[System.Serializable]
public struct TurretValues
{
	public int dmg; //Damage of each bullets
	public float fireRate; //Fire rate
	public float bulletSpeed; //Speed of the bullet
	public float range; //Diameter of where the Turret can detect enemies
	public AttackType[] attackType; //Check which enemy it can attack
}

[RequireComponent(typeof(CapsuleCollider))]
public abstract class TurretTemplate : MonoBehaviour
{
	[Header("Values To Be Set")]
	public TurretValues turretValues;

	[Header("Turret Status")]
	[SerializeField] protected ManaSystem manaSys;
	public float coolDown; //Turret cooldown time
	public int level; //Stores the turret level
	public bool isPrebuilt = false; //Check if the Turret is a prebuilt turret
	public float manaReturnPercentageB; //Stores BASE percentage(in decimal) of mana gained for each kill
	public float manaReturnPercentageS; //Stores STurrets percentage(in decimal) of mana gained for each kill
	public float manaReturnPercentageF; //Stores FINAL percentage(in decimal) of mana gained for each kill

	public float totalFireRate;
	public float fireRateBuff;

	[Header ("Collider and Enemy List")]
	[SerializeField] protected CapsuleCollider collider; //Stores the collider for enemy detection
	[SerializeField] List<AITemplate> enemies; //Stores all valid enemies detected

	[Header ("For Bullets")]
	[SerializeField] private GameObject bullet; //To be set in Inspector
	[SerializeField] Bullet bulletScript;

	protected virtual void Start()
	{
		level = isPrebuilt ? 0 : 1; //If is prebuilt, Set Turret Level to 0, else it is a level 1
		manaReturnPercentageB = isPrebuilt ? 0 : 1; //If is prebuilt, Player should not be gaining any mana at the start.
		manaReturnPercentageS = 0;
		manaReturnPercentageF = manaReturnPercentageB;
		manaSys = FindObjectOfType<ManaSystem>();

		collider = GetComponent<CapsuleCollider>();
		collider.isTrigger = true;
		enemies = new List<AITemplate>();
		bulletScript = (bool)bullet.GetComponent<Bullet>() ? bullet.GetComponent<Bullet>() : bullet.AddComponent<Bullet>(); //Get Bullet Component to Alter Values
		bulletScript.turret = this;
		SetValues(isPrebuilt);

		//Set range of turret depending on type
		//Can Set in Inspector if wanted
		collider.height = 10 / transform.localScale.x;
		//Set Center of Collider Based on the capsule collider's direction
		switch (collider.direction)
		{
			case 0:
				collider.center = new Vector3(-transform.position.y / transform.localScale.x, 0, 0);
				break;
			case 1:
				collider.center = new Vector3(0, -transform.position.y / transform.localScale.x, 0);
				break;
			case 2:
				collider.center = new Vector3(0, 0, -transform.position.y / transform.localScale.x);
				break;
			default:
				collider.center = new Vector3(0, -transform.position.y / transform.localScale.x, 0);
				break;
		}
		collider.radius = (turretValues.range/2) / gameObject.transform.localScale.x;
		//Set cooldown
		totalFireRate = turretValues.fireRate;
		coolDown = 1 / totalFireRate;
	}

	protected virtual void Update()
	{
		coolDown = Mathf.Max(coolDown -= Time.deltaTime, 0);
		if (coolDown <= 0) Shoot();

		if (Input.GetKeyDown(KeyCode.P)) Upgrade();
	}

	protected abstract void SetValues(bool isPrebuilt);

	protected abstract void UpgradeStats(bool isPrebuilt);

	//To be added when turrets are first placed
	public void BoostStats (TurretValues turretValues, STurretValues sTurretValues, float oldInvestmentValue, float oldFireRate, bool boostStats)
	{
		if (boostStats)
		{
			fireRateBuff = MathFunctions.ReturnNewIncrement (fireRateBuff, oldFireRate, sTurretValues.buff);
			RecalculateFireRate ();
		}
		if (isPrebuilt)
		{
			manaReturnPercentageS = MathFunctions.ReturnNewIncrement (manaReturnPercentageS, oldInvestmentValue, sTurretValues.investmentValue);
			RecalculateInvestmentValue ();
		}
	}

	public void RecalculateInvestmentValue ()
	{
		if (isPrebuilt) manaReturnPercentageF = manaReturnPercentageB + manaReturnPercentageS;
	}

	public void RecalculateFireRate ()
	{
		totalFireRate = turretValues.fireRate + fireRateBuff;
	}

	void Upgrade()
	{
		level = Mathf.Min(++level, 3);

		//Add Changes to Stats as well
		UpgradeStats(isPrebuilt);
		collider.center = new Vector3(0, -transform.position.y * transform.localScale.x, 0);
		collider.radius = (turretValues.range / 2) / gameObject.transform.localScale.x;
		RecalculateFireRate ();

		//Only if it is prebuilt, manaReturnPercentageB will change
		if (isPrebuilt)
		{
			float newPerc;

			switch (level)
			{
				case 1:
					newPerc = 0.1f;
					break;
				case 2:
					newPerc = 0.25f;
					break;
				case 3:
					newPerc = 0.5f;
					break;
				default:
					newPerc = 0;
					break;
			}
			manaReturnPercentageB = newPerc;
			RecalculateInvestmentValue ();
		}
	}

	protected virtual void Shoot()
	{
		//Remove any "Enemy" from list if the Enemy Reference is not present
		if (enemies.Contains(null)) enemies.RemoveAll(AI => AI == null);

		if (enemies.Count > 0)
		{
			float shortestDist = Mathf.Infinity;
			int index = 0;
			//print ("==========================================================================");
			for (int i = 0; i < enemies.Count; i++)
			{
				//For attacking enemies closest to Townhall
				float enemyDistance = enemies[i].CheckDistance();
				//print ("Index " + i + ": " + enemyDistance);
				if (enemyDistance < shortestDist)
				{
					shortestDist = enemyDistance;
					index = i;
					//print ("i updated");
				}

				//For attacking enemies closest to Turret
				/*if ((transform.position - enemies[i].transform.position).magnitude < distance)
				{
					distance = (transform.position - enemies[i].transform.position).magnitude;
					//index = i;
				}*/
			}

			Vector3 direction = enemies[index].enemyType == AttackType.air ? -(transform.position - enemies[index].transform.GetChild(0).position).normalized : -(transform.position - enemies[index].transform.position).normalized;
			GameObject currentBullet = Instantiate(bullet, transform.position + direction * 0.5f, Quaternion.identity);
			currentBullet.GetComponent<Rigidbody>().velocity = direction * turretValues.bulletSpeed;
			coolDown = 1 / totalFireRate;
			//print ("Shortest: " + shortestDist);
		}
		else return;
	}

	//Only requires overriding for Cannon and Catapult
	//Function for Bullet to utilise on hit
	//Exploded is specially for Catapults
	public virtual void Hit(AITemplate enemy, bool fromPrebuilt, GameObject bullet, int hitCount, bool exploded = false)
	{
		if (enemy.hp > 0)
		{
			enemy.hp -= turretValues.dmg; //Decrease Enemy Health Upon Hit
			if (enemy.hp <= 0)
			{
				manaSys.ManaAdd((int)(enemy.manaDrop * manaReturnPercentageF));
				print(manaSys.currentMana.ToString());
				Destroy(enemy.gameObject);
			}
		}
		Destroy(bullet);
	}

	/*void OnTriggerStay (Collider other)
	{
		if (other.tag == "AI" && !enemies.Contains(other.GetComponent<AI>())) enemies.Add(other.GetComponent<AI>());
		//print("Working");
	}*/

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "AI")
		{
			AITemplate enemy = other.GetComponent<AITemplate>();
			foreach (AttackType attackType in turretValues.attackType)
			{
				if (attackType == enemy.enemyType)
				{
					enemies.Add(enemy);
					break; //Get out of loop once enemy is added
				}
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "AI") enemies.Remove(other.GetComponent<AITemplate>());
	}
}
