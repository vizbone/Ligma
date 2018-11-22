using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour
{
	public float spawnRate;
	public int count;
	public bool active;
	public bool dev;
	public int maxCount;
	public int indexOfEnemy;
	public GameObject[] enemies;
	public Transform townHallPos;
	public Transform devPos;
	public Transform[] spawnPos;
	public float distance;
	public Text waveNumber;

	float time;
	bool cLock;

	WaveSystem waveSys;
	PreparationPhase prepPhase;

	void Start ()
	{ 
		if (spawnRate <= 0) { spawnRate = 1; }
		time = 1 / spawnRate;
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
		if (count == maxCount) active = false;
		if (indexOfEnemy == waveSys.wave[prepPhase.currentWave].enemy.Length - 1) prepPhase.WaveEnded();
		waveNumber.text = "Wave " + (prepPhase.currentWave + 1).ToString();
	}

	Vector3 GenerateRandomPos () 
	{
		float x = Random.Range (0, distance);
		float z = Mathf.Sqrt (Mathf.Pow (distance, 2) - Mathf.Pow (x, 2));
		x = Random.Range (0, 2) == 1 ? -x : x;
		z = Random.Range (0, 2) == 1 ? -z : z;
		return townHallPos.position + new Vector3 (x, 0.5f, z);
	}

	IEnumerator SpawnClock () 
	{
		Instantiate (waveSys.wave[prepPhase.currentWave].enemy[indexOfEnemy].typeOfEnemy, spawnPos[Random.Range(0,2)].position, Quaternion.identity);
		cLock = true;
		count++;
		yield return new WaitForSeconds (waveSys.wave[0].enemy[0].interval);
		cLock = false;
	}
}