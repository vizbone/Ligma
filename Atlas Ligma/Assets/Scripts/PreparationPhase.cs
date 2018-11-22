using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreparationPhase : MonoBehaviour {

	public bool prepPhaseActive;
	public int currentWave;
	public GameObject nextWaveButton;

	EnemySpawn enemySpawn;

	public Text prepPhaseText;

	private void Awake()
	{
		prepPhaseActive = true;
		currentWave = 0;
		enemySpawn = FindObjectOfType<EnemySpawn>();
	}

	void Update()
	{
		//if (Input.GetKeyDown(KeyCode.A)) prepPhaseActive = false;
	
		/*if (Input.GetKeyDown(KeyCode.S))
		{
			prepPhaseActive = true;
			currentWave++;
			enemySpawn.indexOfEnemy = -1;
		}*/

		if (prepPhaseActive) prepPhaseText.text = "Preparation Phase";

	}

	public void WaveStarted()
	{
		prepPhaseActive = false;
		nextWaveButton.SetActive(false);
	}

	public void WaveEnded()
	{
		prepPhaseActive = true;
		currentWave++;
		enemySpawn.indexOfEnemy = -1;
		nextWaveButton.SetActive(true);
	}
}
