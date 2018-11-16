using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon :  TurretTemplate
{
	protected override void SetValues()
	{
		attackType = AttackType.ground;
		bulletSpeed = 10;
		fireRate = 5;
		range = 100;
	}
}
