using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreparationPhase : MonoBehaviour {

	public bool prepPhaseActive;
	public int currentWave;
	EnemySpawn enemySpawn;

	private void Awake()
	{
		prepPhaseActive = false;
		currentWave = 0;
		enemySpawn = FindObjectOfType<EnemySpawn>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.A)) prepPhaseActive = true;
	
		if (Input.GetKeyDown(KeyCode.S))
		{
			prepPhaseActive = false;
			currentWave++;
			enemySpawn.indexOfEnemy = -1;
		}
	}

}
