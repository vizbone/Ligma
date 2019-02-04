using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum AttackType {ground, air, sea};
public enum Faction {own, black, white};
//Black more damage, low returns; 
//White less damage, high returns;
//Same Investment Systems, Investment will reset after every wave
//Different Levels of Investment - Cannot Invest Mid Wave

[System.Serializable]
public struct TurretValues
{
	public int dmg; //Damage of each bullets
	public float fireRate; //Fire rate
	public float bulletSpeed; //Speed of the bullet
	public float range; //Diameter of where the Turret can detect enemies
	public int[] upgradeOrInvestCost; //Stores cost of Upgrade or Investment
	public AttackType[] attackType; //Check which enemy it can attack
}

[RequireComponent (typeof (CapsuleCollider))]
[RequireComponent (typeof (AudioSource))]
public abstract class TurretTemplate : MonoBehaviour
{
	[Header ("Static Variables")]
	public static float amplitude;
	public static float rotationSpeed;

	[Header ("Values To Be Set")]
	public TurretValues turretValues;
	public Faction faction;

	[Header ("Turret Status")]
	[SerializeField]
	protected ManaSystem manaSys;
	public float manaReturnPerc; //Return Percentage of Mana
	public float coolDown; //Turret cooldown time
	public int level; //Stores the turret level
	public int investmentLevel; //Stores the Investment Level of the Turrets
	public bool isPrebuilt = false; //Check if the Turret is a prebuilt turret
	public bool investOrUpgradeDisabled; //Check if can be invested

	[Header("For Model Change")]
	[SerializeField] GameObject turretGO; //Stores the Game Object where the Mesh is to be the Main Turret Component
	[SerializeField] MeshFilter baseModel;
	[SerializeField] MeshFilter turretModel;
	//Chose not to put in array since it should be set in the inspector
	//Three different Meshfilters to completely prevent errors
	[SerializeField] Mesh[] lvl1Model;
	[SerializeField] Mesh[] lvl2Model;
	[SerializeField] Mesh[] lvl3Model;
	[SerializeField] Renderer baseR;
	[SerializeField] Renderer turretR;
	[SerializeField] Material[] turretMaterials;

	[Header ("Collider and Enemy List")]
	[SerializeField] protected CapsuleCollider collider; //Stores the collider for enemy detection
	[SerializeField] public List<AITemplate> enemies; //Stores all valid enemies detected
	[SerializeField] public AITemplate closestEnemy;
	[SerializeField] float xRotation;
	[SerializeField] Vector3 designatedAngle;

	public MeshCollider meshCollider;
	public MeshCollider turretMeshCollider;

	[Header ("For Bullets")]
	public bool arcTravel;
	public Bullet bullet;

	[Header ("For Events")]
	[SerializeField] EventsManager eventManager;

	[Header("SFX")]
	[SerializeField] protected AudioSource audioSource; //For Turret Fire

	[Header("Particles")]
	[SerializeField] Transform particlePos;
	[SerializeField] Particles smokeAndFlash;

	protected virtual void Start ()
	{
		level = isPrebuilt ? this.level : 1; //All Self Built Turrets are Level 1. PREBUILT TURRET LEVELS SHOULD BE SET IN THE PREFAB ITSELF
		investmentLevel = 0;
		manaReturnPerc = isPrebuilt ? 0 : 1; //If is prebuilt, Player should not be gaining any mana at the start.
		manaSys = FindObjectOfType<ManaSystem> ();
		turretGO = gameObject;
		meshCollider = GetComponent<MeshCollider> ();
		turretMeshCollider = turretGO.GetComponent<MeshCollider>();

		audioSource = GetComponentInChildren<AudioSource>(); //For now use Get Component in Children because the Prefabs are messy
		//shootingSounds = transform.GetChild(0).GetComponent<AudioSource>();
		//enemyDeathSfx = transform.GetChild(1).GetComponent<AudioSource>();

		//Check if its bullets should travel in an arc
		if (this.GetType() == typeof(Catapult)) arcTravel = true;

		xRotation = transform.eulerAngles.x;

		//Remove once reached finalised stage
		baseR = GetComponent<Renderer>();
		turretR = turretGO.GetComponent<Renderer>();
		ChangeMaterial (level);
 
		baseModel = GetComponent<MeshFilter> ();
		turretModel = turretGO.GetComponent<MeshFilter>();

		//Set For Enemy Detection
		collider = GetComponent<CapsuleCollider> ();
		collider.isTrigger = true;
		enemies = new List<AITemplate> ();
		//Set range of turret depending on type
		//Can Set in Inspector if wanted
		collider.height = 10 / transform.localScale.x;
		//Find event manager
		eventManager = FindObjectOfType<EventsManager> ();

		SetValues();

		//Set Center of Collider Based on the capsule collider's direction
		switch (collider.direction)
		{
			case 0:
				collider.center = new Vector3 (-transform.position.y / transform.localScale.x, 0, 0);
				break;
			case 1:
				collider.center = new Vector3 (0, -transform.position.y / transform.localScale.x, 0);
				break;
			case 2:
				collider.center = new Vector3 (0, 0, -transform.position.y / transform.localScale.x);
				break;
			default:
				collider.center = new Vector3 (0, -transform.position.y / transform.localScale.x, 0);
				break;
		}
		collider.radius = (turretValues.range / 2) / gameObject.transform.localScale.x;
		
		//Set cooldown
		coolDown = 1 / turretValues.fireRate;
	}

	protected virtual void Update ()
	{
		if (ManaSystem.gameStateS == GameStates.started || ManaSystem.gameStateS == GameStates.afterWin)
		{
			//Check for closest Enemy if it is not assigned
			if (closestEnemy == null) closestEnemy = EnemyToLookAt();

			if (closestEnemy != null)
			{
				LookAtEnemy();
				if (turretGO.transform.rotation != Quaternion.Euler(designatedAngle)) turretGO.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(designatedAngle), 5f);
			}

			coolDown = Mathf.Max(coolDown -= Time.deltaTime, 0);

			if (coolDown <= 0)
			{
				float angleDiff = Mathf.Abs(designatedAngle.z - turretGO.transform.eulerAngles.y) % 360; //Compare with y since z rotation checks the euler of y
				if (angleDiff <= 20) Shoot(arcTravel); //If the angle difference is minimal, then allow to shoot
			}

			//if (Input.GetKeyDown(KeyCode.P) && level < 3) Upgrade();
		}
	}

	protected abstract void SetValues();

	protected abstract void UpgradeStats();

	//Only For Own Turrets
	public void Upgrade()
	{
		int cost = turretValues.upgradeOrInvestCost[0];

		if (manaSys.currentMana > cost) manaSys.ManaMinus (cost, transform.position, 2);
		else
		{
			print ("Not Enough Mana");
			return;
		}

		level = Mathf.Min (++level, 3);

		//Change Model According to new level
		switch (level)
		{
			case 1:
				baseModel.mesh = lvl1Model[0];
				turretModel.mesh = lvl1Model[1];
				break;
			case 2:
				baseModel.mesh = lvl2Model[0];
				turretModel.mesh = lvl2Model[1];
				break;
			case 3:
				baseModel.mesh = lvl3Model[0];
				turretModel.mesh = lvl3Model[1];
				investOrUpgradeDisabled = true;
				break;
			default:
				baseModel.mesh = lvl1Model[0];
				turretModel.mesh = lvl1Model[1];
				print("Invalid Level Detected");
				break;
		}
		meshCollider.sharedMesh = baseModel.mesh;

		ChangeMaterial (level);
		//Add Changes to Stats as well
		UpgradeStats ();
		collider.radius = (turretValues.range / 2) / gameObject.transform.localScale.x;
	}

	//Only for Prebuilt Turrets
	public void Invest(int investLevel)
	{
		if (faction == Faction.own) return;
		if (investmentLevel >= investLevel) return;

		int cost = 0;
		int newLevel = investmentLevel;
		float newPerc = manaReturnPerc;

		switch (investLevel)
		{
			case 1:
				if (manaSys.currentMana > turretValues.upgradeOrInvestCost[0])
				{
					cost = turretValues.upgradeOrInvestCost[0];
					newLevel = 1;
					if (faction == Faction.black) newPerc = TurretValueSettings.blackInvestPerc1s;
					else if (faction == Faction.white) newPerc = TurretValueSettings.whiteInvestPerc1s;
				}
				else print("Not Enough Mana");
				break;
			case 2:
				if (manaSys.currentMana > turretValues.upgradeOrInvestCost[1])
				{
					cost = turretValues.upgradeOrInvestCost[1];
					newLevel = 2;
					if (faction == Faction.black) newPerc = TurretValueSettings.blackInvestPerc2s;
					else if (faction == Faction.white) newPerc = TurretValueSettings.whiteInvestPerc2s;
				}
				else print("Not Enough Mana");
				break;
			case 3:
				if (manaSys.currentMana > turretValues.upgradeOrInvestCost[2])
				{
					cost = turretValues.upgradeOrInvestCost[2];
					newLevel = 3;
					if (faction == Faction.black) newPerc = TurretValueSettings.blackInvestPerc3s;
					else if (faction == Faction.white) newPerc = TurretValueSettings.whiteInvestPerc3s;
				}
				else print("Not Enough Mana");
				break;
			default:
				print("Invalid Investment Level");
				break;
		}

		manaSys.ManaMinus(cost, transform.position, 2);
		investmentLevel = newLevel;
		manaReturnPerc = newPerc;
	}

	public void ResetManaReturnPerc()
	{
		if (isPrebuilt) manaReturnPerc = 0;
	}

	/// <summary>
	/// Checks which is its closests enemy
	/// Only checks when
	/// 1. A new enemy enters its range
	/// 2. An enemy exits its range
	/// 3. When the Enemy Count is > 0 and Closest Enemy is null
	/// </summary>
	protected AITemplate EnemyToLookAt()
	{
		AITemplate enemyToLookAt = null;

		//Remove any "Enemy" from list if the Enemy Reference is not present
		enemies = enemies.Where(AI => AI != null).ToList();
		//if (enemies.Contains(null)) enemies.RemoveAll(AI => AI == null);

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
					enemyToLookAt = enemies[index];
					//print ("i updated");
				}

				//For attacking enemies closest to Turret
				/*if ((transform.position - enemies[i].transform.position).magnitude < distance)
				{
					distance = (transform.position - enemies[i].transform.position).magnitude;
					//index = i;
				}*/
			}
			//Vector3 direction = enemies[index].enemyType == AttackType.air ? -(transform.position - enemies[index].transform.GetChild(0).position).normalized : -(transform.position - enemies[index].transform.position).normalized;
		}

		return enemyToLookAt;
	}

	protected virtual void LookAtEnemy()
	{
		Vector3 direction = closestEnemy.enemyType == AttackType.air ? -(transform.position - closestEnemy.transform.GetChild(0).position) : -(transform.position - closestEnemy.transform.position);

		//For First Quadrant
		float designatedAngle = Mathf.Atan(direction.x/direction.z) * Mathf.Rad2Deg;
		if (direction.x > 0)
		{
			//Second Quad
			if (direction.z < 0) designatedAngle = 180 - Mathf.Abs(designatedAngle);
		}
		else
		{
			//Third Quad
			if (direction.z < 0) designatedAngle += 180;
			//Fourth Quad
			else designatedAngle = 360 - Mathf.Abs(designatedAngle);
		}
		//print(designatedAngle);

		this.designatedAngle = new Vector3(xRotation, 0, designatedAngle);
	}

	protected virtual void Shoot (bool arcTravel)
	{
		//Remove any "Enemy" from list if the Enemy Reference is not present
		enemies = enemies.Where(AI => AI != null).ToList();

		if (closestEnemy == null) closestEnemy = EnemyToLookAt();

		if (enemies.Count > 0)
		{
			Vector3 direction = closestEnemy.enemyType == AttackType.air ? -(transform.position - closestEnemy.transform.GetChild(0).position).normalized : -(transform.position - closestEnemy.transform.position).normalized;
			Bullet currentBullet = Instantiate (bullet, transform.position + direction * 0.5f + new Vector3 (0, 0.5f, 0), Quaternion.identity);
			//print(currentBullet.name);
			currentBullet.turret = this;

			//Help to refine this if possible. Best that I can think of for now
			if (this.GetType() == typeof(Cannon)) ManaSystem.inst.audioLibrary.PlayAudio(ManaSystem.inst.audioLibrary.cannon, audioSource);
			else if (this.GetType() == typeof(Catapult)) ManaSystem.inst.audioLibrary.PlayAudio(ManaSystem.inst.audioLibrary.catapult, audioSource);
			else if (this.GetType() == typeof(Crossbow)) ManaSystem.inst.audioLibrary.PlayAudio(ManaSystem.inst.audioLibrary.crossbow, audioSource);
			else if (this.GetType() == typeof(Rockets)) ManaSystem.inst.audioLibrary.PlayAudio(ManaSystem.inst.audioLibrary.rocket, audioSource);
			else print("Error in Getting Correct Audio Source");

			if (arcTravel)
			{
				currentBullet.speed = turretValues.bulletSpeed;
				currentBullet.amplitude = amplitude;
				currentBullet.target = closestEnemy.transform.position;
				currentBullet.frequency1 = currentBullet.transform.position.x - currentBullet.target.x > 0 ? currentBullet.transform.position.x - currentBullet.target.x : -(currentBullet.transform.position.x - currentBullet.target.x);
				currentBullet.catapult = true;

				currentBullet = null;
			}
			else
			{
				currentBullet.catapult = false;
				if (this.GetType () == typeof (Cannon))
				{
					currentBullet.velocity = new Vector3 (direction.x, 0, direction.z) * turretValues.bulletSpeed;
					Quaternion rotation = transform.rotation;
					rotation.eulerAngles = rotation.eulerAngles - new Vector3 (rotation.eulerAngles.x, 0, 0);
					Instantiate (smokeAndFlash, particlePos.position, rotation); //i put the cannon particle here for now until more particles
				} else currentBullet.velocity = direction * turretValues.bulletSpeed;
			}
			
			coolDown = 1 / turretValues.fireRate;
			//print ("Shortest: " + shortestDist);
		}
		else return;
	}

	//Only requires overriding for Cannon and Catapult
	//Function for Bullet to utilise on hit
	//Exploded is specially for Catapults
	public virtual void Hit (AITemplate enemy, bool fromPrebuilt, GameObject bullet, int hitCount, bool exploded = false)
	{
		if (enemy.hp > 0)
		{
			enemy.hp -= turretValues.dmg; //Decrease Enemy Health Upon Hit
			enemy.ResetTimer ();
			if (enemy.hp <= 0)
			{
				int addedMana = (int) (enemy.manaDrop * manaReturnPerc);
				manaSys.ManaAdd (addedMana, transform.position, 5);
				enemies.Remove(enemy);

				AudioSource source = enemy.GetComponent<AudioSource>();
				ManaSystem.inst.audioLibrary.PlayAudio(ManaSystem.inst.audioLibrary.skeletonDeath, audioSource);

				if (closestEnemy == enemy) closestEnemy = null;
				Destroy (enemy.gameObject);
			}
		}
		Destroy (bullet);
	}

	protected void DamageEffect()
	{
		Vector3 direction = closestEnemy.enemyType == AttackType.air ? -(transform.position - closestEnemy.transform.GetChild(0).position) : -(transform.position - closestEnemy.transform.position);

		//For First Quadrant
		float designatedAngle = Mathf.Atan(direction.x / direction.z) * Mathf.Rad2Deg;
		if (direction.x > 0)
		{
			//Second Quad
			if (direction.z < 0) designatedAngle = 180 - Mathf.Abs(designatedAngle);
		}
		else
		{
			//Third Quad
			if (direction.z < 0) designatedAngle += 180;
			//Fourth Quad
			else designatedAngle = 360 - Mathf.Abs(designatedAngle);
		}
		//print(designatedAngle);

		this.designatedAngle = new Vector3(xRotation, 0, designatedAngle);
	}

	void ChangeMaterial (int lvlIndex)
	{
		switch (lvlIndex)
		{
			case 1:
				baseR.material = turretMaterials[0];
				turretR.material = turretMaterials[0];
				break;

			case 2:
				baseR.material = turretMaterials[1];
				turretR.material = turretMaterials[1];
				break;

			case 3:
				baseR.material = turretMaterials[2];
				turretR.material = turretMaterials[2];
				break;
		}
	}

	/*void OnTriggerStay (Collider other)
	{
		if (other.tag == "AI" && !enemies.Contains(other.GetComponent<AI>())) enemies.Add(other.GetComponent<AI>());
		//print("Working");
	}*/

	private void OnTriggerEnter (Collider other)
	{
		if (other.tag == "AI")
		{
			AITemplate enemy = other.GetComponentInParent<AITemplate> ();
			if (enemies.Contains(null)) enemies.RemoveAll(AI => AI == null);
			foreach (AttackType attackType in turretValues.attackType)
			{
				if (attackType == enemy.enemyType)
				{
					enemies.Add (enemy);
					closestEnemy = EnemyToLookAt();
					break; //Get out of loop once enemy is added
				}
			}
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.tag == "AI")
		{
			enemies.Remove(other.GetComponent<AITemplate>());
			closestEnemy = EnemyToLookAt();
		}
	}
}