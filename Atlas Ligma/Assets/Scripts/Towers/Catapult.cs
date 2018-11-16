using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : TurretTemplate {

	protected override void SetValues()
	{
		range = 10;
		fireRate = 2f;
		attackType = AttackType.ground;
		bulletSpeed = 50;
	}
}
