using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretValueSettings : MonoBehaviour
{
	//Script Under Testing
	//Crossbow Settings
	[Header("Crossbow Level 1")]
	public TurretValues crossbow1;
	public static TurretValues crossbow1s;

	[Header("Crossbow Level 2")]
	public TurretValues crossbow2;
	public static TurretValues crossbow2s;

	[Header("Crossbow Level 3")]
	public TurretValues crossbow3;
	public static TurretValues crossbow3s;

	[Header("Prebuilt Crossbow Level 0")]
	public TurretValues prebuiltCrossbow0;
	public static TurretValues prebuiltCrossbow0s;

	[Header("Prebuilt Crossbow Level 1")]
	public TurretValues prebuiltCrossbow1;
	public static TurretValues prebuiltCrossbow1s;

	[Header("Prebuilt Crossbow Level 2")]
	public TurretValues prebuiltCrossbow2;
	public static TurretValues prebuiltCrossbow2s;

	[Header("Prebuilt Crossbow Level 3")]
	public TurretValues prebuiltCrossbow3;
	public static TurretValues prebuiltCrossbow3s;

	//Catapult Settings
	[Header("Catapult Level 1")]
	public TurretValues catapult1;
	public static TurretValues catapult1s;

	[Header("Catapult Level 2")]
	public TurretValues catapult2;
	public static TurretValues catapult2s;

	[Header("Catapult Level 3")]
	public TurretValues catapult3;
	public static TurretValues catapult3s;

	[Header("Prebuilt Catapult Level 0")]
	public TurretValues prebuiltCatapult0;
	public static TurretValues prebuiltCatapult0s;

	[Header("Prebuilt Catapult Level 1")]
	public TurretValues prebuiltCatapult1;
	public static TurretValues prebuiltCatapult1s;

	[Header("Prebuilt Catapult Level 2")]
	public TurretValues prebuiltCatapult2;
	public static TurretValues prebuiltCatapult2s;

	[Header("Prebuilt Catapult Level 3")]
	public TurretValues prebuiltCatapult3;
	public static TurretValues prebuiltCatapult3s;

	//Cannon Settings
	[Header("Cannon Level 1")]
	public TurretValues cannon1;
	public static TurretValues cannon1s;

	[Header("Cannon Level 2")]
	public TurretValues cannon2;
	public static TurretValues cannon2s;

	[Header("Cannon Level 3")]
	public TurretValues cannon3;
	public static TurretValues cannon3s;

	[Header("Prebuilt Cannon Level 0")]
	public TurretValues prebuiltCannon0;
	public static TurretValues prebuiltCannon0s;

	[Header("Prebuilt Cannon Level 1")]
	public TurretValues prebuiltCannon1;
	public static TurretValues prebuiltCannon1s;

	[Header("Prebuilt Cannon Level 2")]
	public TurretValues prebuiltCannon2;
	public static TurretValues prebuiltCannon2s;

	[Header("Prebuilt Cannon Level 3")]
	public TurretValues prebuiltCannon3;
	public static TurretValues prebuiltCannon3s;

	//Rocket Settings
	[Header("Rocket Level 1")]
	public TurretValues rocket1;
	public static TurretValues rocket1s;

	[Header("Rocket Level 2")]
	public TurretValues rocket2;
	public static TurretValues rocket2s;

	[Header("Rocket Level 3")]
	public TurretValues rocket3;
	public static TurretValues rocket3s;

	[Header("Prebuilt Rocket Level 0")]
	public TurretValues prebuiltRocket0;
	public static TurretValues prebuiltRocket0s;

	[Header("Prebuilt Rocket Level 1")]
	public TurretValues prebuiltRocket1;
	public static TurretValues prebuiltRocket1s;

	[Header("Prebuilt Rocket Level 2")]
	public TurretValues prebuiltRocket2;
	public static TurretValues prebuiltRocket2s;

	[Header("Prebuilt Rocket Level 3")]
	public TurretValues prebuiltRocket3;
	public static TurretValues prebuiltRocket3s;

	private void Start()
	{
		//Set Values For Crossbows
		crossbow1s = crossbow1;
		crossbow2s = crossbow2;
		crossbow3s = crossbow3;
		prebuiltCrossbow0s = prebuiltCrossbow0;
		prebuiltCrossbow1s = prebuiltCrossbow1;
		prebuiltCrossbow2s = prebuiltCrossbow2;
		prebuiltCrossbow3s = prebuiltCrossbow3;

		//Set Values For Catapults
		catapult1s = catapult1;
		catapult2s = catapult2;
		catapult3s = catapult3;
		prebuiltCatapult0s = prebuiltCatapult0;
		prebuiltCatapult1s = prebuiltCatapult1;
		prebuiltCatapult2s = prebuiltCatapult2;
		prebuiltCatapult3s = prebuiltCatapult3;

		//Set Values For Cannons
		cannon1s = cannon1;
		cannon2s = cannon2;
		cannon3s = cannon3;
		prebuiltCannon0s = prebuiltCannon0;
		prebuiltCannon1s = prebuiltCannon1;
		prebuiltCannon2s = prebuiltCannon2;
		prebuiltCannon3s = prebuiltCannon3;

		//Set Values For Rockets
		rocket1s = rocket1;
		rocket2s = rocket2;
		rocket3s = rocket3;
		prebuiltRocket0s = prebuiltRocket0;
		prebuiltRocket1s = prebuiltRocket1;
		prebuiltRocket2s = prebuiltRocket2;
		prebuiltRocket3s = prebuiltRocket3;
	}
}
