using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rockets : TurretTemplate {

	protected override void SetValues(bool isPrebuilt)
	{
		if (!isPrebuilt) turretValues = TurretValueSettings.rocket1s;
		else turretValues = TurretValueSettings.prebuiltRocket0s;
	}

	protected override void UpgradeStats(bool isPrebuilt)
	{
		if (!isPrebuilt)
		{
			switch (level)
			{
				case 1:
					turretValues = TurretValueSettings.rocket1s;
					break;
				case 2:
					turretValues = TurretValueSettings.rocket2s;
					break;
				case 3:
					turretValues = TurretValueSettings.rocket3s;
					break;
				default:
					turretValues = TurretValueSettings.rocket1s;
					break;
			}
		}
		else
		{
			switch (level)
			{
				case 1:
					turretValues = TurretValueSettings.prebuiltRocket1s;
					break;
				case 2:
					turretValues = TurretValueSettings.prebuiltRocket2s;
					break;
				case 3:
					turretValues = TurretValueSettings.prebuiltRocket3s;
					break;
				default:
					turretValues = TurretValueSettings.prebuiltRocket0s;
					break;
			}
		}
	}
}
