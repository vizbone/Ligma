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

	Transform townHall;
	NavMeshAgent agent;
	AIMovement ai;
	bool unloading;
	float time;
	bool cLock;
	bool firstTime;

	public AudioSource seaEnemySpawnAudio;

	int currentEnemySpawnCount;

	protected override void Start () 
	{
		WaveSystem.enemyListS.Add(this);

		//Setting Values
		if (whichType == SeaEnemy.boat)
		{
			hp = 200;
			manaDrop = 100;
		}
		else
		{
			hp = 300;
			manaDrop = 200;
		}

		ai = FindObjectOfType<AIMovement> ();
		agent = GetComponent<NavMeshAgent> ();
		defaultMoveSpeed = agent.speed;
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
		if (ManaSystem.gameStateS == GameStates.started || ManaSystem.gameStateS == GameStates.afterWin)
		{
			agent.speed = defaultMoveSpeed;
			if (unloading && !cLock)
			StartCoroutine(spawn());
		}
		else agent.speed = 0;
	}

	//Do we need this???
	public override float CheckDistance()
	{
		return (transform.position - townHall.position).magnitude;
	}

	IEnumerator spawn () 
	{
		cLock = true;
		Instantiate (enemies, path == AIMovement.Paths.seaPath1 ? ai.seaPath1Spawn.transform.position : ai.seaPath2Spawn.transform.position, Quaternion.identity);
		//seaEnemySpawnAudio.Play();
		currentEnemySpawnCount++;
		if (currentEnemySpawnCount >= enemyBatchSpawnCount)
		{
			agent.enabled = true;
			ai.NextPointSea (agent, this);
			unloading = false;
		}
		yield return new WaitForSeconds (time);
		cLock = false;
	}

	void Instance () 
	{
		instanceCount++;
		agent.enabled = true;
		ai.NextPointSea (agent, this);
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "WaypointSea")
		{
			if (currentDestination == 1)
			{
				if (firstTime)
					firstTime = false;
				else
				{
					unloading = true;
					agent.enabled = false;
				}
			} else if (instanceCount >= maxInstances)
				Destroy (gameObject);
			else
			{
				agent.enabled = true;
				Instance ();
				currentEnemySpawnCount = 0;
			}
		}
	}	
}