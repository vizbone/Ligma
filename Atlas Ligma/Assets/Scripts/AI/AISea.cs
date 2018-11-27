using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AISea : AITemplate
{
	public enum SeaEnemy { ship, boat };
	[SerializeField] SeaEnemy whichType;

	public AIMovement.Paths path;
	public int currentDestination;
	public GameObject enemies;
	public int enemyBatchSpawnCount;
	public float enemySpawnRate;
	public int maxInstances;
	public int instanceCount;

	[SerializeField] Transform townHall;
	NavMeshAgent agent;
	AIMovement ai;
	bool unloading;
	float time;
	bool cLock;
	bool firstTime;

	int currentEnemySpawnCount;

	protected override void Start () 
	{
		//Setting Values
		if (whichType == SeaEnemy.boat)
		{
			hp = 5;
			manaDrop = 100;
		}
		else
		{
			hp = 10;
			manaDrop = 200;
		}

		ai = FindObjectOfType<AIMovement> ();
		agent = GetComponent<NavMeshAgent> ();
		townHall = GameObject.Find ("Townhall").transform;

		cLock = false;
		currentDestination = 0;
		firstTime = true;
		unloading = false;
		
		Instance ();

		if (enemySpawnRate == 0) enemySpawnRate = 1;
		time = 1 / enemySpawnRate;
	}

	void Update () 
	{
		if (unloading && !cLock)
			StartCoroutine (spawn ());
	}

	//Do we need this???
	public override float CheckDistance()
	{
		return (transform.position - townHall.position).magnitude;
	}

	IEnumerator spawn () 
	{
		cLock = true;
		GameObject temp = Instantiate (enemies, path == AIMovement.Paths.seaPath1 ? ai.seaPath1Spawn.transform.position : ai.seaPath2Spawn.transform.position, Quaternion.identity);
		currentEnemySpawnCount++;
		if (currentEnemySpawnCount >= enemyBatchSpawnCount)
		{
			ai.NextPointSea (agent, this);
			unloading = false;
		}
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
		if (other.tag == "WaypointSea")
		{
			if (currentDestination == 1)
				if (firstTime)
					firstTime = false;
				else
					unloading = true;
			else if (instanceCount >= maxInstances)
				Destroy (gameObject);
			else
			{
				Instance ();
				currentEnemySpawnCount = 0;
			}
		}
	}
}