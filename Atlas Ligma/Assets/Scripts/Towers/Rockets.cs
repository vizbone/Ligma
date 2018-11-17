using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rockets : TurretTemplate {

	protected override void SetValues()
	{
		range = 5;
		fireRate = 0.5f;
		attackType = AttackType.air;
		bulletSpeed = 10;
	}
}
