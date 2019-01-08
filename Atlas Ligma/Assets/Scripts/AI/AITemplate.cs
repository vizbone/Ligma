﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AITemplate : MonoBehaviour {

	public AttackType enemyType;
	public int hp = 3;
	public int manaDrop = 10;

	// Use this for initialization
	protected virtual void Start ()
	{
		WaveSystem.enemyListS.Add(this);

		if (enemyType == AttackType.ground)
		{
			hp = 100;
			manaDrop = 5;
		}
		else if (enemyType == AttackType.air)
		{
			hp = 200;
			manaDrop = 20;
		}
		//Sea Enemies will be overriding the Start Function
	}

	public abstract float CheckDistance();

	private void OnDestroy()
	{
		WaveSystem.enemyListS.Remove(this);
	}
}
