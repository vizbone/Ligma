using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : TurretTemplate {

	public GameObject explosion;
	public Explosion explosionScript;

	protected override void Start()
	{
		base.Start();
		explosionScript = explosion.GetComponent<Explosion>() ? explosion.GetComponent<Explosion>() : explosion.AddComponent<Explosion>();
		explosionScript.turret = this;
	}

	protected override void SetValues(bool isPrebuilt)
	{
		if (!isPrebuilt) turretValues = TurretValueSettings.catapult1s;
		else turretValues = TurretValueSettings.prebuiltCatapult0s;
	}

	public override void Hit(AITemplate enemy, bool fromPrebuilt, GameObject bullet, bool exploded = false)
	{
		if (!exploded)
		{
			Instantiate(explosion, bullet.transform.position, Quaternion.identity);
			Destroy(bullet);
		}
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
	}

	protected override void UpgradeStats(bool isPrebuilt)
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
					turretValues = TurretValueSettings.catapult1s;
					break;
			}
		}
		else
		{
			switch (level)
			{
				case 1:
					turretValues = TurretValueSettings.prebuiltCatapult1s;
					break;
				case 2:
					turretValues = TurretValueSettings.prebuiltCatapult2s;
					break;
				case 3:
					turretValues = TurretValueSettings.prebuiltCatapult3s;
					break;
				default:
					turretValues = TurretValueSettings.prebuiltCatapult0s;
					break;
			}
		}
	}
}
