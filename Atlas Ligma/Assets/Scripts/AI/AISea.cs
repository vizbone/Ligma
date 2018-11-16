using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AISea : MonoBehaviour
{
	public AIMovement.Paths path;
	public int currentDestination;
	public GameObject enemies;
	public int enemyBatchSpawnCount;
	public float enemySpawnRate;
	public int maxInstances;
	public int instanceCount;

	NavMeshAgent agent;
	AIMovement ai;
	bool unloading;
	float time;
	bool cLock;

	int currentEnemySpawnCount;

	void Start () 
	{
		ai = FindObjectOfType<AIMovement> ();
		agent = GetComponent<NavMeshAgent> ();

		cLock = false;
		currentDestination = 0;
		
		Instance ();

		if (enemySpawnRate == 0) enemySpawnRate = 1;
		time = 1 / enemySpawnRate;
	}

	void Update () 
	{
		if (unloading && !cLock) StartCoroutine (spawn ());
	}

	IEnumerator spawn () 
	{
		cLock = true;
		Instantiate (enemies, path == AIMovement.Paths.seaPath1 ? ai.seaPath1Spawn.transform.position : ai.seaPath2Spawn.transform.position, Quaternion.identity);
		currentEnemySpawnCount++;
		if (currentEnemySpawnCount >= enemyBatchSpawnCount) { }
		yield return new WaitForSeconds (time);
		cLock = false;
	}

	void Instance () 
	{
		instanceCount++;
		ai.NextPointSea (agent, this);
		
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "WaypointSea") unloading = unloading ? false : true;
	}
}