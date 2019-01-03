using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour
{
	//public float spawnRate;
	//public int count;
	//public bool active;
	//public bool dev;
	//public int maxCount;
	public int indexOfEnemy;
	//public Transform devPos;
	public Transform[] spawnPos;
	//public float distance;
	//public Text waveNumber;

	bool cLock;

	WaveSystem waveSys;
	PreparationPhase prepPhase;

	void Start ()
	{ 
		//if (spawnRate <= 0) { spawnRate = 1; }
		cLock = false;
		waveSys = FindObjectOfType<WaveSystem>();
		prepPhase = FindObjectOfType<PreparationPhase>();
		indexOfEnemy = -1;
	}

	void Update ()
	{
		if (!prepPhase.prepPhaseActive && !cLock) {
			indexOfEnemy++;
			StartCoroutine ("SpawnClock");
		}
		//if (count == maxCount) active = false;
		if (prepPhase.currentWave < waveSys.wave.Length)
		{
			if (indexOfEnemy == waveSys.wave[prepPhase.currentWave].enemy.Length - 1) prepPhase.WaveEnded ();
		}
		//waveNumber.text = "Wave " + (prepPhase.currentWave + 1).ToString();
	}

	IEnumerator SpawnClock () 
	{
		if (waveSys.wave[prepPhase.currentWave].enemy[indexOfEnemy].type == AttackType.ground || waveSys.wave[prepPhase.currentWave].enemy[indexOfEnemy].type == AttackType.air)
		{
			Instantiate(waveSys.wave[prepPhase.currentWave].enemy[indexOfEnemy].typeOfEnemy, spawnPos[Random.Range(0, 2)].position, Quaternion.identity);
		}
		if (waveSys.wave[prepPhase.currentWave].enemy[indexOfEnemy].type == AttackType.sea)
		{
			int result = Random.Range(2, 4);
			GameObject seaEnemy = Instantiate(waveSys.wave[prepPhase.currentWave].enemy[indexOfEnemy].typeOfEnemy, spawnPos[result].position, Quaternion.identity);
			if (result == 2) seaEnemy.GetComponent<AISea>().path = AIMovement.Paths.seaPath1;
			else seaEnemy.GetComponent<AISea>().path = AIMovement.Paths.seaPath2;
		}
		cLock = true;
		//count++;
		yield return new WaitForSeconds (waveSys.wave[0].enemy[0].interval);
		cLock = false;
	}
}