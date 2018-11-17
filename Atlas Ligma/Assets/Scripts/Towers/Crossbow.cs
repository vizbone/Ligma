using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : TurretTemplate {

	protected override void SetValues(bool isPrebuilt)
	{
		if (!isPrebuilt) turretValues = TurretValueSettings.crossbow1s;
		else turretValues = TurretValueSettings.prebuiltCrossbow0s;
	}

	protected override void UpgradeStats(bool isPrebuilt)
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
					turretValues = TurretValueSettings.crossbow1s;
					break;
			}
		}
		else
		{
			switch (level)
			{
				case 1:
					turretValues = TurretValueSettings.prebuiltCrossbow1s;
					break;
				case 2:
					turretValues = TurretValueSettings.prebuiltCrossbow2s;
					break;
				case 3:
					turretValues = TurretValueSettings.prebuiltCrossbow3s;
					break;
				default:
					turretValues = TurretValueSettings.prebuiltCrossbow0s;
					break;
			}
		}
	}
}
