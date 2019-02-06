using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretValueSettings : MonoBehaviour
{
	//Crossbow Settings
	[Header("Crossbow Level 1")]
	[SerializeField] TurretValues crossbow1;
	public static TurretValues crossbow1s;

	[Header("Crossbow Level 2")]
	[SerializeField] TurretValues crossbow2;
	public static TurretValues crossbow2s;

	[Header("Crossbow Level 3")]
	[SerializeField] TurretValues crossbow3;
	public static TurretValues crossbow3s;

	[Header("Black Crossbow Level 1")]
	[SerializeField] TurretValues blackCrossbow1;
	public static TurretValues blackCrossbow1s;

	[Header("Black Crossbow Level 2")]
	[SerializeField] TurretValues blackCrossbow2;
	public static TurretValues blackCrossbow2s;

	[Header("Black Crossbow Level 3")]
	[SerializeField] TurretValues blackCrossbow3;
	public static TurretValues blackCrossbow3s;

	[Header("White Crossbow Level 1")]
	[SerializeField] TurretValues whiteCrossbow1;
	public static TurretValues whiteCrossbow1s;

	[Header("White Crossbow Level 2")]
	[SerializeField] TurretValues whiteCrossbow2;
	public static TurretValues whiteCrossbow2s;

	[Header("White Crossbow Level 3")]
	[SerializeField] TurretValues whiteCrossbow3;
	public static TurretValues whiteCrossbow3s;

	//Catapult Settings
	[Header("Catapult Level 1")]
	[SerializeField] TurretValues catapult1;
	public static TurretValues catapult1s;

	[Header("Catapult Level 2")]
	[SerializeField] TurretValues catapult2;
	public static TurretValues catapult2s;

	[Header("Catapult Level 3")]
	[SerializeField] TurretValues catapult3;
	public static TurretValues catapult3s;

	[Header("Black Catapult Level 1")]
	[SerializeField] TurretValues blackCatapult1;
	public static TurretValues blackCatapult1s;

	[Header("Black Catapult Level 2")]
	[SerializeField] TurretValues blackCatapult2;
	public static TurretValues blackCatapult2s;

	[Header("Black Catapult Level 3")]
	[SerializeField] TurretValues blackCatapult3;
	public static TurretValues blackCatapult3s;

	[Header("White Catapult Level 1")]
	[SerializeField] public TurretValues whiteCatapult1;
	public static TurretValues whiteCatapult1s;

	[Header("White Catapult Level 2")]
	[SerializeField] TurretValues whiteCatapult2;
	public static TurretValues whiteCatapult2s;

	[Header("White Catapult Level 3")]
	[SerializeField] TurretValues whiteCatapult3;
	public static TurretValues whiteCatapult3s;

	//Cannon Settings
	[Header("Cannon Level 1")]
	[SerializeField] TurretValues cannon1;
	public static TurretValues cannon1s;

	[Header("Cannon Level 2")]
	[SerializeField] TurretValues cannon2;
	public static TurretValues cannon2s;

	[Header("Cannon Level 3")]
	[SerializeField] TurretValues cannon3;
	public static TurretValues cannon3s;

	[Header("Black Cannon Level 1")]
	[SerializeField] TurretValues blackCannon1;
	public static TurretValues blackCannon1s;

	[Header("Black Cannon Level 2")]
	[SerializeField] TurretValues blackCannon2;
	public static TurretValues blackCannon2s;

	[Header("Black Cannon Level 3")]
	[SerializeField] TurretValues blackCannon3;
	public static TurretValues blackCannon3s;

	[Header("White Cannon Level 1")]
	[SerializeField] TurretValues whiteCannon1;
	public static TurretValues whiteCannon1s;

	[Header("White Cannon Level 2")]
	[SerializeField] TurretValues whiteCannon2;
	public static TurretValues whiteCannon2s;

	[Header("White Cannon Level 3")]
	[SerializeField] TurretValues whiteCannon3;
	public static TurretValues whiteCannon3s;

	//Rocket Settings
	[Header("Rocket Level 1")]
	[SerializeField] TurretValues rocket1;
	public static TurretValues rocket1s;

	[Header("Rocket Level 2")]
	[SerializeField] TurretValues rocket2;
	public static TurretValues rocket2s;

	[Header("Rocket Level 3")]
	[SerializeField] TurretValues rocket3;
	public static TurretValues rocket3s;

	[Header("Black Rocket Level 1")]
	[SerializeField] TurretValues blackRocket1;
	public static TurretValues blackRocket1s;

	[Header("Black Rocket Level 2")]
	[SerializeField] TurretValues blackRocket2;
	public static TurretValues blackRocket2s;

	[Header("Black Rocket Level 3")]
	[SerializeField] TurretValues blackRocket3;
	public static TurretValues blackRocket3s;

	[Header("White Rocket Level 1")]
	[SerializeField] TurretValues whiteRocket1;
	public static TurretValues whiteRocket1s;

	[Header("White Rocket Level 2")]
	[SerializeField] TurretValues whiteRocket2;
	public static TurretValues whiteRocket2s;

	[Header("White Rocket Level 3")]
	[SerializeField] TurretValues whiteRocket3;
	public static TurretValues whiteRocket3s;

	[Header("Black Investment % in 1.00")]
	[SerializeField] float blackInvestPerc1;
	public static float blackInvestPerc1s;

	[SerializeField] float blackInvestPerc2;
	public static float blackInvestPerc2s;

	[SerializeField] float blackInvestPerc3;
	public static float blackInvestPerc3s;

	[Header ("White Investment % in 1.00")]
	[SerializeField] float whiteInvestPerc1;
	public static float whiteInvestPerc1s;

	[SerializeField] float whiteInvestPerc2;
	public static float whiteInvestPerc2s;

	[SerializeField] float whiteInvestPerc3;
	public static float whiteInvestPerc3s;

	private void Awake()
	{

		//Set Values For Crossbows
		crossbow1s = crossbow1;
		crossbow2s = crossbow2;
		crossbow3s = crossbow3;

		blackCrossbow1s = blackCrossbow1;
		blackCrossbow2s = blackCrossbow2;
		blackCrossbow3s = blackCrossbow3;

		whiteCrossbow1s = whiteCrossbow1;
		whiteCrossbow2s = whiteCrossbow2;
		whiteCrossbow3s = whiteCrossbow3;

		//Set Values For Catapults
		catapult1s = catapult1;
		catapult2s = catapult2;
		catapult3s = catapult3;

		blackCatapult1s = blackCatapult1;
		blackCatapult2s = blackCatapult2;
		blackCatapult3s = blackCatapult3;

		whiteCatapult1s = whiteCatapult1;
		whiteCatapult2s = whiteCatapult2;
		whiteCatapult3s = whiteCatapult3;

		//Set Values For Cannons
		cannon1s = cannon1;
		cannon2s = cannon2;
		cannon3s = cannon3;

		blackCannon1s = blackCannon1;
		blackCannon2s = blackCannon2;
		blackCannon3s = blackCannon3;

		whiteCannon1s = whiteCannon1;
		whiteCannon2s = whiteCannon2;
		whiteCannon3s = whiteCannon3;

		//Set Values For Rockets
		rocket1s = rocket1;
		rocket2s = rocket2;
		rocket3s = rocket3;

		blackRocket1s = blackRocket1;
		blackRocket2s = blackRocket2;
		blackRocket3s = blackRocket3;

		whiteRocket1s = whiteRocket1;
		whiteRocket2s = whiteRocket2;
		whiteRocket3s = whiteRocket3;

		//For Investments
		blackInvestPerc1s = blackInvestPerc1;
		blackInvestPerc2s = blackInvestPerc2;
		blackInvestPerc3s = blackInvestPerc3;

		whiteInvestPerc1s = whiteInvestPerc1;
		whiteInvestPerc2s = whiteInvestPerc2;
		whiteInvestPerc3s = whiteInvestPerc3;
	}

	public static TurretValues SetValuesCorrectly (TurretValues staticValues)
	{
		TurretValues turretValues;
		turretValues.attackType = staticValues.attackType;
		turretValues.bulletSpeed = staticValues.bulletSpeed;
		turretValues.dmg = staticValues.dmg;
		turretValues.fireRate = staticValues.fireRate;
		turretValues.range = staticValues.range;
		turretValues.upgradeOrInvestCost = new int[staticValues.upgradeOrInvestCost.Length];
		for (int i = 0; i < staticValues.upgradeOrInvestCost.Length; i++)
		{
			turretValues.upgradeOrInvestCost[i] = staticValues.upgradeOrInvestCost[i];
		}
		turretValues.firingPos = staticValues.firingPos;
		turretValues.lightingPos = staticValues.lightingPos;
		return turretValues;
	}
}
