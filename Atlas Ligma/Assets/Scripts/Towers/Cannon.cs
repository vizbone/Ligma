using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon :  TurretTemplate
{
	protected override void SetValues()
	{
		range = 5;
		fireRate = 1;
		attackType = AttackType.lowGround;
		bulletSpeed = 10;
	}
}
