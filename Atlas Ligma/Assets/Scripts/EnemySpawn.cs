using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
	public float spawnRate;
	public int count;
	public bool active;
	public bool dev;
	public int maxCount;
	public GameObject[] enemies;
	public Transform townHallPos;
	public Transform devPos;
	public float distance;

	float time;
	bool cLock;

	void Start ()
	{ 
		if (spawnRate <= 0) { spawnRate = 1; }
		time = 1 / spawnRate;
		cLock = false;
	}

	void Update ()
	{
		if (active && !cLock) { StartCoroutine ("spawnClock"); }
		if (count == maxCount) active = false;
	}

	Vector3 GenerateRandomPos () 
	{
		float x = Random.Range (0, distance);
		float z = Mathf.Sqrt (Mathf.Pow (distance, 2) - Mathf.Pow (x, 2));
		x = Random.Range (0, 2) == 1 ? -x : x;
		z = Random.Range (0, 2) == 1 ? -z : z;
		return townHallPos.position + new Vector3 (x, 0.5f, z);
	}

	IEnumerator spawnClock () 
	{
		Instantiate (enemies[Random.Range (0, enemies.Length)], dev ? GenerateRandomPos () : devPos.position, Quaternion.identity);
		cLock = true;
		count++;
		yield return new WaitForSeconds (time);
		cLock = false;
	}
}