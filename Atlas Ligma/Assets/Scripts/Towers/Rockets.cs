using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rockets : TurretTemplate {

	protected override void SetValues()
	{
		if (!isPrebuilt)
			turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.rocket1s);
		else
		{
			switch (level)
			{
				case 1:
					if (faction == Faction.black) turretValues = TurretValueSettings.SetValuesCorrectly(TurretValueSettings.blackRocket1s);
					else if (faction == Faction.white) turretValues = TurretValueSettings.SetValuesCorrectly(TurretValueSettings.whiteRocket1s);
					break;
				case 2:
					if (faction == Faction.black) turretValues = TurretValueSettings.SetValuesCorrectly(TurretValueSettings.blackRocket2s);
					else if (faction == Faction.white) turretValues = TurretValueSettings.SetValuesCorrectly(TurretValueSettings.whiteRocket2s);
					break;
				case 3:
					if (faction == Faction.black) turretValues = TurretValueSettings.SetValuesCorrectly(TurretValueSettings.blackRocket3s);
					else if (faction == Faction.white) turretValues = TurretValueSettings.SetValuesCorrectly(TurretValueSettings.whiteRocket3s);
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
					turretValues = TurretValueSettings.SetValuesCorrectly(TurretValueSettings.rocket1s);
					break;
				case 2:
					turretValues = TurretValueSettings.SetValuesCorrectly(TurretValueSettings.rocket2s);
					break;
				case 3:
					turretValues = TurretValueSettings.SetValuesCorrectly(TurretValueSettings.rocket3s);
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
					if (faction == Faction.black) turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.blackRocket1s);
					else if (faction == Faction.white) turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.whiteRocket1s);
					break;
				case 2:
					if (faction == Faction.black) turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.blackRocket2s);
					else if (faction == Faction.white) turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.whiteRocket2s);
					break;
				case 3:
					if (faction == Faction.black) turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.blackRocket3s);
					else if (faction == Faction.white) turretValues = TurretValueSettings.SetValuesCorrectly (TurretValueSettings.whiteRocket3s);
					break;
				default:
					print ("Error in Level");
					break;
			}
		}
	}
}
