using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Catapult : TurretTemplate
{

	[Header ("Catapult Exclusives")]
	public Explosion explosion;

	[Header("Catapult Mesh Exclusive")]
	[SerializeField] Renderer[] handleAndBowlR;
	[SerializeField] MeshFilter[] handleAndBowlMF;

	[SerializeField] Vector3[] handleLocalPos;
	[SerializeField] Vector3[] bowlLocalPos;

	public Animator anim;

	protected override void Start()
	{
		base.Start();
		anim = GetComponent<Animator>();
		anim.SetFloat ("SpeedMulti", turretValues.fireRate);
	}

	protected override void SetValues()
	{
		if (!isPrebuilt)
			turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.catapult1s);
		else
		{
			switch (level)
			{
				case 1:
					if (faction == Faction.black) turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.blackCatapult1s);
					else if (faction == Faction.white) turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.whiteCatapult1s);
					break;
				case 2:
					if (faction == Faction.black) turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.blackCatapult2s);
					else if (faction == Faction.white) turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.whiteCatapult2s);
					break;
				case 3:
					if (faction == Faction.black) turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.blackCatapult3s);
					else if (faction == Faction.white) turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.whiteCatapult3s);
					break;
				default:
					print("Error in Level");
					break;
			}
		}
	}

	//Only For Own Turrets
	public override void Upgrade()
	{
		int cost = turretValues.upgradeOrInvestCost[0];

		if (manaSys.currentMana > cost) manaSys.ManaMinus(cost, transform.position, 5);
		else
		{
			print("Not Enough Mana");
			return;
		}

		level = Mathf.Min(++level, 3);

		//Change Model According to new level
		switch (level)
		{
			case 1:
				baseModel.mesh = lvl1Model[0];
				turretModel.mesh = lvl1Model[1];
				meshCollider.sharedMesh = lvl1Model[2];
				handleAndBowlMF[0].mesh = lvl1Model[3]; //3 is handle
				handleAndBowlMF[1].mesh = lvl1Model[4]; //4 is bowl

				handleAndBowlMF[0].gameObject.transform.localPosition = handleLocalPos[0];
				handleAndBowlMF[1].gameObject.transform.localPosition = bowlLocalPos[0];
				break;
			case 2:
				baseModel.mesh = lvl2Model[0];
				turretModel.mesh = lvl2Model[1];
				meshCollider.sharedMesh = lvl2Model[2];
				handleAndBowlMF[0].mesh = lvl2Model[3]; //3 is handle
				handleAndBowlMF[1].mesh = lvl2Model[4]; //4 is bowl

				handleAndBowlMF[0].gameObject.transform.localPosition = handleLocalPos[1];
				handleAndBowlMF[1].gameObject.transform.localPosition = bowlLocalPos[1];
				break;
			case 3:
				baseModel.mesh = lvl3Model[0];
				turretModel.mesh = lvl3Model[1];
				meshCollider.sharedMesh = lvl3Model[2];
				handleAndBowlMF[0].mesh = lvl3Model[3]; //3 is handle
				handleAndBowlMF[1].mesh = lvl3Model[4]; //4 is bowl

				handleAndBowlMF[0].gameObject.transform.localPosition = handleLocalPos[2];
				handleAndBowlMF[1].gameObject.transform.localPosition = bowlLocalPos[2];

				investOrUpgradeDisabled = true;
				break;
			default:
				baseModel.mesh = lvl1Model[0];
				turretModel.mesh = lvl1Model[1];
				meshCollider.sharedMesh = lvl1Model[2];
				handleAndBowlMF[0].mesh = lvl1Model[3]; //3 is handle
				handleAndBowlMF[1].mesh = lvl1Model[4]; //4 is bowl

				handleAndBowlMF[0].gameObject.transform.localPosition = handleLocalPos[0];
				handleAndBowlMF[1].gameObject.transform.localPosition = bowlLocalPos[0];
				print("Invalid Level Detected");
				break;
		}

		ChangeMaterial(level);
		//Add Changes to Stats as well
		UpgradeStats();

		ManaSystem.inst.gui.turretInfo.UpdateTurretInfo(this);
		anim.SetFloat ("SpeedMulti", turretValues.fireRate);
		collider.radius = (turretValues.range / 2) / gameObject.transform.localScale.x;

		manaLight.gameObject.transform.localPosition = turretValues.lightingPos;
	}

	protected override void ChangeMaterial(int lvlIndex)
	{
		switch (lvlIndex)
		{
			case 1:
				baseR.material = turretBaseMaterials[0];
				if (turretR.materials.Length > 1)
				{
					Material[] mat = new Material[2];
					mat[0] = turretBaseMaterials[0];
					mat[1] = turretMaterials[0];

					turretR.materials = mat;
				}
				else turretR.material = turretMaterials[0];

				handleAndBowlR[0].material = turretMaterials[0];
				handleAndBowlR[1].material = turretMaterials[0];
				break;

			case 2:
				baseR.material = turretBaseMaterials[1];
				if (turretR.materials.Length > 1)
				{
					Material[] mat = new Material[2];
					mat[0] = turretBaseMaterials[1];
					mat[1] = turretMaterials[1];

					turretR.materials = mat;
				}
				else turretR.material = turretMaterials[1];

				handleAndBowlR[0].material = turretMaterials[1];
				handleAndBowlR[1].material = turretMaterials[1];
				break;

			case 3:
				baseR.material = turretBaseMaterials[2];
				if (turretR.materials.Length > 1)
				{
					Material[] mat = new Material[2];
					mat[0] = turretBaseMaterials[2];
					mat[1] = turretMaterials[2];

					turretR.materials = mat;
				}
				else turretR.material = turretMaterials[2];

				handleAndBowlR[0].material = turretMaterials[2];
				handleAndBowlR[1].material = turretMaterials[2];
				break;
		}
	}

	protected override void Shoot(bool arcTravel)
	{
		if (enemies.Count > 0) anim.Play ("Catapult Fire");
	}

	public void CatapultShoot ()
	{
		//Remove any "Enemy" from list if the Enemy Reference is not present
		enemies = enemies.Where (AI => AI != null).ToList ();

		if (closestEnemy == null) closestEnemy = EnemyToLookAt ();

		if (enemies.Count > 0)
		{
			Vector3 direction = closestEnemy.enemyType == AttackType.air ? -(transform.position - closestEnemy.transform.GetChild (0).position).normalized : new Vector3 (transform.position.x - closestEnemy.transform.position.x, 0, transform.position.z - closestEnemy.transform.position.z).normalized * -1;
			Vector3 direction2D = new Vector3 (direction.x, 0, direction.z);
			Bullet currentBullet = Instantiate (BulletSelect (level), turretGO.transform);
			currentBullet.transform.localPosition = turretValues.firingPos;
			currentBullet.transform.parent = null;
			//print(currentBullet.name);
			currentBullet.turret = this;

			ManaSystem.inst.audioLibrary.PlayAudio (ManaSystem.inst.audioLibrary.catapult, audioSource);

			anim.SetTrigger ("Fire");

			currentBullet.speed = turretValues.bulletSpeed;
			currentBullet.amplitude = amplitude;
			currentBullet.target = closestEnemy.transform.position;
			currentBullet.frequency1 = currentBullet.transform.position.x - currentBullet.target.x > 0 ? currentBullet.transform.position.x - currentBullet.target.x : -(currentBullet.transform.position.x - currentBullet.target.x);
			currentBullet.catapult = true;
			currentBullet.currentY = turretValues.firingPos.y;

			currentBullet = null;

			coolDown = 1 / turretValues.fireRate;
			//print ("Shortest: " + shortestDist);
		} else return;
	}

	public override void Hit(AITemplate enemy, bool fromPrebuilt, GameObject bullet, int hitCount, bool exploded = false)
	{
		if (!exploded)
		{
			Explosion explosionInst = Instantiate(explosion, bullet.transform.position, Quaternion.identity);
			explosionInst.turret = this;
			Destroy(bullet);
		}
		else
		{
			if (enemy.hp > 0)
			{
				enemy.hp -= turretValues.dmg; //Decrease Enemy Health Upon Hit
				enemy.ResetTimer ();
				if (enemy.hp <= 0)
				{
					int addedMana = (int)(enemy.manaDrop * manaReturnPerc);
					manaSys.ManaAdd(addedMana, transform.position, 10);

					AudioSource source = enemy.GetComponent<AudioSource>();
					ManaSystem.inst.audioLibrary.PlayAudio(ManaSystem.inst.audioLibrary.skeletonDeath, audioSource);

					enemies.Remove(enemy);
					if (closestEnemy == enemy) closestEnemy = null;
					Destroy(enemy.gameObject);
				}
			}
		}
	}

	protected override void UpgradeStats()
	{
		if (!isPrebuilt)
		{
			switch (level)
			{
				case 1:
					turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.catapult1s);
					break;
				case 2:
					turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.catapult2s);
					break;
				case 3:
					turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.catapult3s);
					break;
				default:
					print("Error in Level");
					break;
			}
		}
		else //Incase there is a need for upgrading of turrets as events
		{
			switch (level)
			{
				case 1:
					if (faction == Faction.black) turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.blackCatapult1s);
					else if (faction == Faction.white) turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.whiteCatapult1s);
					break;
				case 2:
					if (faction == Faction.black) turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.blackCatapult2s);
					else if (faction == Faction.white) turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.whiteCatapult2s);
					break;
				case 3:
					if (faction == Faction.black) turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.blackCatapult3s);
					else if (faction == Faction.white) turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.whiteCatapult3s);
					break;
				default:
					print ("Error in Level");
					break;
			}
		}
	}
}
