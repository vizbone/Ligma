using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon :  TurretTemplate
{
	protected override void SetValues(bool isPrebuilt)
	{
		if (!isPrebuilt) turretValues = TurretValueSettings.cannon1s;
		else turretValues = TurretValueSettings.prebuiltCannon0s;
	}

	public override void Hit(AITemplate enemy, bool fromPrebuilt, GameObject bullet, int hitCount, bool exploded = false)
	{
		if (enemy.hp > 0)
		{
			hitCount++;
			enemy.hp -= turretValues.dmg; //Decrease Enemy Health Upon Hit
			if (enemy.hp <= 0)
			{
				manaSys.ManaAdd((int)(enemy.manaDrop * manaReturnPercentageF));
				print(manaSys.currentMana.ToString());
				Destroy(enemy.gameObject);
			}
		}

		if (hitCount >= 5) Destroy(gameObject);
	}

	protected override void UpgradeStats(bool isPrebuilt)
	{
		if (!isPrebuilt)
		{
			switch (level)
			{
				case 1:
					turretValues = TurretValueSettings.cannon1s;
					break;
				case 2:
					turretValues = TurretValueSettings.cannon2s;
					break;
				case 3:
					turretValues = TurretValueSettings.cannon3s;
					break;
				default:
					turretValues = TurretValueSettings.cannon1s;
					break;
			}
		}
		else
		{
			switch (level)
			{
				case 1:
					turretValues = TurretValueSettings.prebuiltCannon1s;
					break;
				case 2:
					turretValues = TurretValueSettings.prebuiltCannon2s;
					break;
				case 3:
					turretValues = TurretValueSettings.prebuiltCannon3s;
					break;
				default:
					turretValues = TurretValueSettings.prebuiltCannon0s;
					break;
			}
		}
	}
}
