using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType {lowGround, air, both};

[System.Serializable]
public struct TurretValues
{
	public int dmg; //Damage of each bullets
	public float fireRate; //Time at which a bullet is fired
	public float bulletSpeed; //Speed of the bullet
	public float range; //Diameter of where the Turret can detect enemies
	public AttackType attackType; //Check which enemy it can attack
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
	public float manaReturnPercentageF; //Stores FINAL percentage(in decimal) of mana gained for each kill

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
		manaReturnPercentageF = manaReturnPercentageB;
		manaSys = FindObjectOfType<ManaSystem>();

		collider = GetComponent<CapsuleCollider>();
		collider.isTrigger = true;
		enemies = new List<AITemplate>();
		bulletScript = (bool)bullet.GetComponent<Bullet>() ? bullet.GetComponent<Bullet>() : bullet.AddComponent<Bullet>(); //Get Bullet Component to Alter Values
		bulletScript.turret = this;
		SetValues(isPrebuilt);

		//Set range of turret depending on type
		collider.radius = turretValues.range/2;
		//Set cooldown
		coolDown = turretValues.fireRate;
	}

	protected virtual void Update()
	{
		coolDown = Mathf.Max(coolDown -= Time.deltaTime, 0);
		if (coolDown <= 0) Shoot();

		if (Input.GetKeyDown(KeyCode.P)) Upgrade();
	}

	protected abstract void SetValues(bool isPrebuilt);

	protected abstract void UpgradeStats(bool isPrebuilt);

	void Upgrade()
	{
		level = Mathf.Min(++level, 3);

		//Add Changes to Stats as well
		UpgradeStats(isPrebuilt);

		//Only if it is prebuilt, manaReturnPercentageB will change
		if (isPrebuilt)
		{
			switch (level)
			{
				case 1:
					manaReturnPercentageB = 0.1f;
					break;
				case 2:
					manaReturnPercentageF -= manaReturnPercentageB;
					manaReturnPercentageB = 0.25f;
					manaReturnPercentageF += manaReturnPercentageB;
					break;
				case 3:
					manaReturnPercentageF -= manaReturnPercentageB;
					manaReturnPercentageB = 0.5f;
					manaReturnPercentageF += manaReturnPercentageB;
					break;
				default:
					manaReturnPercentageB = 0;
					break;
			}
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

			Vector3 direction = enemies[index].enemyType == AttackType.lowGround ? -(transform.position - enemies[index].transform.position).normalized : -(transform.position - enemies[index].transform.GetChild(0).position).normalized;
			GameObject currentBullet = Instantiate(bullet, transform.position + direction * 0.5f, Quaternion.identity);
			currentBullet.GetComponent<Rigidbody>().velocity = direction * turretValues.bulletSpeed;
			coolDown = turretValues.fireRate;
			//print ("Shortest: " + shortestDist);
		}
		else return;
	}

	//Only requires overriding for Cannon and Catapult
	//Function for Bullet to utilise on hit
	//Exploded is specially for Catapults
	public virtual void Hit(AITemplate enemy, bool fromPrebuilt, GameObject bullet, bool exploded = false)
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
			if (turretValues.attackType == AttackType.both || turretValues.attackType == enemy.enemyType) enemies.Add(enemy);
			else return;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "AI") enemies.Remove(other.GetComponent<AITemplate>());
	}
}
