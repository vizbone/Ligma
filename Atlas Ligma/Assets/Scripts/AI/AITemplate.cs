using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AITemplate : MonoBehaviour {

	public AttackType enemyType;
	public int hp = 3;
	public int manaDrop = 10;

	// Use this for initialization
	protected virtual void Start ()
	{
		if (enemyType == AttackType.lowGround)
		{
			hp = 3;
			manaDrop = 5;
		}
		else if (enemyType == AttackType.air)
		{
			hp = 10;
			manaDrop = 20;
		}
	}

	public abstract float CheckDistance();
}
