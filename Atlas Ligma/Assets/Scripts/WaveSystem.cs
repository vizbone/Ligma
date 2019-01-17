﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSystem : MonoBehaviour {
	
	[System.Serializable]
	public struct Waves
	{
		public EnemySpawnTemplate[] enemy;
	}

	[Header("For Prep Phase")]
	public bool prepPhase;
	public int currentWave; //Refers to Index of Wave Array
	public GameObject nextWaveButton;
	public Text prepPhaseText;

	[Header("Enemy Spawn")]
	public Waves[] wave;
	[SerializeField] int enemySpawnIndex; //Stores the Index of Which Enemy to Spawn according to the Waves struct
	[SerializeField] Transform[] spawnPos;
	[SerializeField] bool cLock;

	[Header("For Enemy Tracking")]
	[SerializeField] List<AITemplate> enemyList;
	public static List<AITemplate> enemyListS; //Allows Adding and Removing of Enemies more easily.

	[Header("Prebuilt Turrets")]
	public TurretTemplate[] allPrebuiltTurrets;

	//[Header("For Events")]
	//[SerializeField] EventsManager em;

	private void Awake()
	{
		prepPhase = true;
		currentWave = 0;
		enemySpawnIndex = -1;
		enemyListS = new List<AITemplate>();
		//em = FindObjectOfType<EventsManager>();

		allPrebuiltTurrets = FindObjectsOfType<TurretTemplate>();
	}

	void Update()
	{
		if (ManaSystem.gameStateS == GameStates.started || ManaSystem.gameStateS == GameStates.afterWin)
		{
			if (!prepPhase)
			{
				//Update Enemy List for checking in Inspector
				enemyList = enemyListS;

				if (!cLock)
				{
					enemySpawnIndex = Mathf.Min(++enemySpawnIndex, wave[currentWave].enemy.Length);

					//Only if Reached Last Enemy
					if (enemySpawnIndex == wave[currentWave].enemy.Length)
					{
						if (enemyList.Count == 0) WaveEnded(); //Wait for Last Enemy to Die to end the Wave
					}
					else StartCoroutine(SpawnClock());
				}
			}
		}
	}

	//Link to Start Wave Button
	public void WaveStarted()
	{
		prepPhase = false;

		prepPhaseText.text = "Wave " + (currentWave + 1);

		nextWaveButton.SetActive(false);
	}

	public void WaveEnded()
	{
		prepPhase = true;

		prepPhaseText.text = "Preparation Phase";

		foreach (TurretTemplate turret in allPrebuiltTurrets)
		{
			turret.investmentLevel = 0;
			turret.manaReturnPerc = 0;
		}

		//If Current Wave is the Final Wave
		if (currentWave == wave.Length - 1)
		{
			if (ManaSystem.gameStateS == GameStates.started) ManaSystem.gameStateS = GameStates.lose;
			else if (ManaSystem.gameStateS == GameStates.afterWin)
			{
				ManaSystem.inst.gui.continueButton.interactable = false;
				ManaSystem.inst.gui.uiAnim += ManaSystem.inst.gui.DisplayWin;
			}
		}
		else
		{
			currentWave = Mathf.Min(++currentWave, wave.Length);
			enemySpawnIndex = -1;
			nextWaveButton.SetActive(true);

			//em.EventEnd();
			//em.ExecuteEvent();

			//em.ExecuteEvent = null;
		}
	}

	IEnumerator SpawnClock()
	{
		cLock = true;

		if (wave[currentWave].enemy[enemySpawnIndex].type == AttackType.sea)
		{
			int result = Random.Range (2, 4);
			GameObject seaEnemy = Instantiate (wave[currentWave].enemy[enemySpawnIndex].typeOfEnemy, spawnPos[result].position, Quaternion.identity);
			if (result == 2) seaEnemy.GetComponent<AISea> ().path = AIMovement.Paths.seaPath1;
			else seaEnemy.GetComponent<AISea> ().path = AIMovement.Paths.seaPath2;
		} else if (wave[currentWave].enemy[enemySpawnIndex].type == AttackType.air)
		{
			Instantiate (wave[currentWave].enemy[enemySpawnIndex].typeOfEnemy, spawnPos[Random.Range(4, 7)].position, Quaternion.identity);
		} else Instantiate (wave[currentWave].enemy[enemySpawnIndex].typeOfEnemy, spawnPos[Random.Range (0, 2)].position, Quaternion.identity);

		yield return new WaitForSeconds(wave[0].enemy[0].interval);

		cLock = false;
	}
}