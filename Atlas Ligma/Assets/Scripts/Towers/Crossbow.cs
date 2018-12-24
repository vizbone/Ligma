using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : TurretTemplate {

	protected override void SetValues()
	{
		if (isPrebuilt)
			turretValues = TurretValueSettings.crossbow1s;
		else
		{
			switch (level)
			{
				case 1:
					if (faction == Faction.black) turretValues = TurretValueSettings.blackCrossbow1s;
					else if (faction == Faction.white) turretValues = TurretValueSettings.whiteCrossbow1s;
					break;
				case 2:
					if (faction == Faction.black) turretValues = TurretValueSettings.blackCrossbow2s;
					else if (faction == Faction.white) turretValues = TurretValueSettings.whiteCrossbow2s;
					break;
				case 3:
					if (faction == Faction.black) turretValues = TurretValueSettings.blackCrossbow3s;
					else if (faction == Faction.white) turretValues = TurretValueSettings.whiteCrossbow3s;
					break;
				default:
					print("Error in Level");
					break;
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
					turretValues = TurretValueSettings.crossbow1s;
					break;
				case 2:
					turretValues = TurretValueSettings.crossbow2s;
					break;
				case 3:
					turretValues = TurretValueSettings.crossbow3s;
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
					if (faction == Faction.black) turretValues = TurretValueSettings.blackCrossbow1s;
					else if (faction == Faction.white) turretValues = TurretValueSettings.whiteCrossbow1s;
					break;
				case 2:
					if (faction == Faction.black) turretValues = TurretValueSettings.blackCrossbow2s;
					else if (faction == Faction.white) turretValues = TurretValueSettings.whiteCrossbow2s;
					break;
				case 3:
					if (faction == Faction.black) turretValues = TurretValueSettings.blackCrossbow3s;
					else if (faction == Faction.white) turretValues = TurretValueSettings.whiteCrossbow3s;
					break;
				default:
					print("Error in Level");
					break;
			}
		}
	}
}
