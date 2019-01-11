using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : TurretTemplate
{
	[Header ("Catapult Exclusives")]
	public Explosion explosion;

	public AudioSource skeletonDeathSound;

	protected override void Start()
	{
		base.Start();
	}

	protected override void SetValues()
	{
		if (!isPrebuilt)
			turretValues = TurretValueSettings.catapult1s;
		else
		{
			switch (level)
			{
				case 1:
					if (faction == Faction.black) turretValues = TurretValueSettings.blackCatapult1s;
					else if (faction == Faction.white) turretValues = TurretValueSettings.whiteCatapult1s;
					break;
				case 2:
					if (faction == Faction.black) turretValues = TurretValueSettings.blackCatapult2s;
					else if (faction == Faction.white) turretValues = TurretValueSettings.whiteCatapult2s;
					break;
				case 3:
					if (faction == Faction.black) turretValues = TurretValueSettings.blackCatapult3s;
					else if (faction == Faction.white) turretValues = TurretValueSettings.whiteCatapult3s;
					break;
				default:
					print("Error in Level");
					break;
			}
		}
	}

	/*protected override void Shoot ()
	{
		//Remove any "Enemy" from list if the Enemy Reference is not present
		if (enemies.Contains (null)) enemies.RemoveAll (AI => AI == null);

		if (enemies.Count > 0)
		{
			float shortestDist = Mathf.Infinity;
			int index = 0;
			for (int i = 0; i < enemies.Count; i++)
			{
				//For attacking enemies closest to Townhall
				float enemyDistance = enemies[i].CheckDistance ();
				if (enemyDistance < shortestDist)
				{
					shortestDist = enemyDistance;
					index = i;
				}
			}
			Bullet currentBullet = Instantiate (bullet, transform.position + new Vector3 (0, 0.5f, 0), Quaternion.identity);
			currentBullet.turret = this;

			currentBullet.speed = speed;
			currentBullet.amplitude = amplitude;
			currentBullet.target = enemies[index].transform.position;
			currentBullet.frequency1 = currentBullet.transform.position.x - currentBullet.target.x > 0 ? currentBullet.transform.position.x - currentBullet.target.x : -(currentBullet.transform.position.x - currentBullet.target.x);
			currentBullet.catapult = true;

			currentBullet = null;

			coolDown = 1 / totalFireRate;
			FindObjectOfType<AudioManager>().AudioToPlay("CatapultFire");
		} else
			return;
	}*/

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
				if (enemy.hp <= 0)
				{
					int addedMana = (int)(enemy.manaDrop * manaReturnPerc);
					manaSys.ManaAdd(addedMana, enemy.transform.position, 0);
					//print (manaSys.currentMana.ToString ());
					//FindObjectOfType<AudioManager>().AudioToPlay("SkeletonDeath");
					skeletonDeathSound.Play();
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
					turretValues = TurretValueSettings.catapult1s;
					break;
				case 2:
					turretValues = TurretValueSettings.catapult2s;
					break;
				case 3:
					turretValues = TurretValueSettings.catapult3s;
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
					if (faction == Faction.black) turretValues = TurretValueSettings.blackCatapult1s;
					else if (faction == Faction.white) turretValues = TurretValueSettings.whiteCatapult1s;
					break;
				case 2:
					if (faction == Faction.black) turretValues = TurretValueSettings.blackCatapult2s;
					else if (faction == Faction.white) turretValues = TurretValueSettings.whiteCatapult2s;
					break;
				case 3:
					if (faction == Faction.black) turretValues = TurretValueSettings.blackCatapult3s;
					else if (faction == Faction.white) turretValues = TurretValueSettings.whiteCatapult3s;
					break;
				default:
					print("Error in Level");
					break;
			}
		}
	}
}
