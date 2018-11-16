using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : TurretTemplate {

	protected override void SetValues()
	{
		range = 5;
		fireRate = 0.5f;
		attackType = AttackType.both;
		bulletSpeed = 50;
	}
}
