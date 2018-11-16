using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : TurretTemplate {

	protected override void SetValues()
	{
		fireRate = 1;
		range = 75;
		bulletSpeed = 100;
		attackType = AttackType.both;
	}
}
