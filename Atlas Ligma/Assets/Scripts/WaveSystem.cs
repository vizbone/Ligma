using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class WaveSystem : MonoBehaviour {
	
	[System.Serializable]
	public struct Waves
	{
		public EnemySpawnTemplate[] enemy;
	}

	[Header("For Prep Phase")]
	public bool prepPhase;
	public int currentWave; //Refers to Index of Wave Array

	[Header("Enemy Spawn")]
	public Waves[] wave;
	[SerializeField] int enemySpawnIndex; //Stores the Index of Which Enemy to Spawn according to the Waves struct
	[SerializeField] Transform[] spawnPos;
	[SerializeField] bool cLock;
	public bool allWavesCleared;

	[Header("For Enemy Tracking")]
	[SerializeField] List<AITemplate> enemyList;
	public static List<AITemplate> enemyListS; //Allows Adding and Removing of Enemies more easily.

	[Header("Prebuilt Turrets")]
	public TurretTemplate[] allPrebuiltTurrets;

	[Header("For Events")]
	[SerializeField] EventsManager em;
	public bool endless;
	public EnemySpawnTemplate[] airEnemies;
	public EnemySpawnTemplate[] groundEnemies;
	public EnemySpawnTemplate[] shipEnemies;

	[Header("For AudioMixerSnapShot System")]
	[SerializeField] AudioMixerSnapshot prepPhaseSnapShot;
	[SerializeField] AudioMixerSnapshot battlePhaseSnapShot;

	private void Start()
	{
		allWavesCleared = false;

		prepPhase = true;
		currentWave = 0;
		enemySpawnIndex = -1;
		enemyListS = new List<AITemplate>();
		em = GetComponent<EventsManager> ();

		allPrebuiltTurrets = FindObjectsOfType<TurretTemplate>();

		//Check if the current Scene is the tutorial level
		//To ensure that the press start to play is not activated during Tutorial
		Scene currentScene = SceneManager.GetActiveScene();
		string currentSceneName = currentScene.name;
		if (!(currentSceneName == "Level 1")){
			ManaSystem.inst.gui.EndWaveAppearance(); //Prep Phase Mode
		}
	}

	void Update()
	{
		if (ManaSystem.gameStateS == GameStates.started || ManaSystem.gameStateS == GameStates.afterWin)
		{
			if (!prepPhase)
			{
				if (!endless)
				{
					//Update Enemy List for checking in Inspector
					enemyListS = enemyListS.Where (AITemplate => AITemplate != null).ToList ();
					enemyList = enemyListS;

					if (!cLock)
					{
						enemySpawnIndex = Mathf.Min (++enemySpawnIndex, wave[currentWave].enemy.Length);

						//Only if Reached Last Enemy
						if (enemySpawnIndex == wave[currentWave].enemy.Length)
						{
							if (enemyList.Count == 0) WaveEnded (); //Wait for Last Enemy to Die to end the Wave
						} else StartCoroutine (SpawnClock (false));
					}
				} 
				else
				{
					if (!cLock) StartCoroutine (SpawnClock (true));
				}
			}
			else
			{
				if (Input.GetKeyDown(KeyCode.Space) && !ManaSystem.inst.inTutorial) WaveStarted();
			}
		}
	}

	//Link to Start Wave Button
	public void WaveStarted()
	{
		prepPhase = false;

		//BGM Transition from PrepPhase to BattlePhase
		battlePhaseSnapShot.TransitionTo(1.0f);
		ManaSystem.inst.gui.StartWaveAppearance();
	}

	public void WaveEnded()
	{
		prepPhase = true;

		//BGM Transition from BattlePhase to PrepPhase
		prepPhaseSnapShot.TransitionTo(1.0f);

		//If Current Wave is the Final Wave
		if (currentWave == wave.Length - 1)
		{
			if (ManaSystem.gameStateS == GameStates.started) ManaSystem.gameStateS = GameStates.lose;
			else if (ManaSystem.gameStateS == GameStates.afterWin) allWavesCleared = true;
		}
		else
		{
			currentWave = Mathf.Min(++currentWave, wave.Length);
			enemySpawnIndex = -1;

			ManaSystem.inst.gui.EndWaveAppearance();

			if (em.EventEnd != null) em.EventEnd ();
			if (em.ExecuteEvent != null) em.ExecuteEvent ();
			em.ExecuteEvent = null;

			ManaSystem.inst.gui.CheckEventNotifications();
		}

		ManaSystem.inst.gui.UpdateWave(currentWave, wave.Length);

		//Reset Investment after checking events as some of the Event rely on checking investment levelss
		foreach (TurretTemplate turret in allPrebuiltTurrets)
		{
			turret.investmentLevel = 0;
			turret.manaReturnPerc = 0;
			turret.DestroyInvestmentParticles();
		}
	}

	IEnumerator SpawnClock (bool endless)
	{
		cLock = true;
		float interval = 0;
		GameObject enemy = null;
		if (!endless)
		{
			if (wave[currentWave].enemy[enemySpawnIndex].type == AttackType.sea)
			{
				int result = Random.Range (2, 4);

				enemy = Instantiate (wave[currentWave].enemy[enemySpawnIndex].typeOfEnemy, spawnPos[result].position, Quaternion.identity);

				if (result == 2) enemy.GetComponent<AISea> ().path = AIMovement.Paths.seaPath1;
				else enemy.GetComponent<AISea> ().path = AIMovement.Paths.seaPath2;
			} 
			else if (wave[currentWave].enemy[enemySpawnIndex].type == AttackType.air)
			{
				enemy = Instantiate (wave[currentWave].enemy[enemySpawnIndex].typeOfEnemy, spawnPos[Random.Range (4, 7)].position, Quaternion.identity);
			} 
			else enemy = Instantiate (wave[currentWave].enemy[enemySpawnIndex].typeOfEnemy, spawnPos[Random.Range (0, 2)].position, Quaternion.identity);
		}
		else
		{
			int random = Random.Range (0, 3);
			
			if (random == 0)
			{
				//air
				int random2 = Random.Range (0, airEnemies.Length);
				enemy = Instantiate (airEnemies[random2].typeOfEnemy, spawnPos[Random.Range (4, 7)].position, Quaternion.identity);
				interval = airEnemies[random2].interval;
			}
			else if (random == 1)
			{
				//ground
				int random2 = Random.Range (0, groundEnemies.Length);
				enemy = Instantiate (groundEnemies[random2].typeOfEnemy, spawnPos[Random.Range (0, 2)].position, Quaternion.identity);
				interval = groundEnemies[random2].interval;
			} 
			else if (random == 2)
			{
				//ship
				int result = Random.Range (2, 4);
				int random2 = Random.Range (0, shipEnemies.Length);
				enemy = Instantiate (shipEnemies[random2].typeOfEnemy, spawnPos[result].position, Quaternion.identity);
				interval = shipEnemies[random2].interval;

				if (result == 2) enemy.GetComponent<AISea> ().path = AIMovement.Paths.seaPath1;
				else enemy.GetComponent<AISea> ().path = AIMovement.Paths.seaPath2;
			}
		}
		/*for (int i = 0; i <= 30; i++)
		{
			Instantiate (wave[currentWave].enemy[enemySpawnIndex].typeOfEnemy, spawnPos[Random.Range (0, 2)].position, Quaternion.identity);
		}*/

		AITemplate ai = enemy.GetComponent<AITemplate> ();
		ai.worldCanvas = ManaSystem.inst.worldSpaceCanvas;
		if (!endless) ai.hp += (int) (25 * Mathf.Floor (currentWave / 2));

		if (!endless) yield return new WaitForSeconds(wave[currentWave].enemy[enemySpawnIndex].interval);
		else yield return new WaitForSeconds (interval);

		cLock = false;
	}
}