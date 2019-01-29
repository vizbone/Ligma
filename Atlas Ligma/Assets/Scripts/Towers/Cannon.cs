using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : TurretTemplate
{
	protected override void SetValues ()
	{
		if (!isPrebuilt)
			turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.cannon1s);
		else
		{
			switch (level)
			{
				case 1:
					if (faction == Faction.black) turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.blackCannon1s);
					else if (faction == Faction.white) turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.whiteCannon1s);
					break;
				case 2:
					if (faction == Faction.black) turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.blackCannon2s);
					else if (faction == Faction.white) turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.whiteCannon2s);
					break;
				case 3:
					if (faction == Faction.black) turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.blackCannon3s);
					else if (faction == Faction.white) turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.whiteCannon3s);
					break;
				default:
					print("Error in Level");
					break;
			}
		}
	}

	public override void Hit (AITemplate enemy, bool fromPrebuilt, GameObject bullet, int hitCount, bool exploded = false)
	{
		if (enemy.hp > 0)
		{
			hitCount++;
			enemy.hp -= turretValues.dmg; //Decrease Enemy Health Upon Hit
			if (enemy.hp <= 0)
			{
				int addedMana = (int) (enemy.manaDrop * manaReturnPerc);
				manaSys.ManaAdd (addedMana, enemy.transform.position, 0);
				enemies.Remove(enemy);
				enemyDeathSfx.Play();
				if (closestEnemy == enemy) closestEnemy = null;
				Destroy (enemy.gameObject);
			}
		}

		if (hitCount >= 5)
			Destroy (gameObject);
	}

	protected override void UpgradeStats ()
	{
		if (!isPrebuilt)
		{
			switch (level)
			{
				case 1:
					turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.cannon1s);
					break;
				case 2:
					turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.cannon2s);
					break;
				case 3:
					turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.cannon3s);
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
					if (faction == Faction.black) turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.blackCannon1s);
					else if (faction == Faction.white) turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.whiteCannon1s);
					break;
				case 2:
					if (faction == Faction.black) turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.blackCannon2s);
					else if (faction == Faction.white) turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.whiteCannon2s);
					break;
				case 3:
					if (faction == Faction.black) turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.blackCannon3s);
					else if (faction == Faction.white) turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.whiteCannon3s);
					break;
				default:
					print ("Error in Level");
					break;
			}
		}
	}
}