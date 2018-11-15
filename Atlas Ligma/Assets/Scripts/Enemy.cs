using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
	public Transform townhall;
	public float maxHealth = 5.0f;
	public float currentHealth;
	public bool frozen;

	NavMeshAgent agent;

	void Start ()
	{
		agent = GetComponent<NavMeshAgent>();
		townhall = FindObjectOfType<EnemySpawn>().townHallPos;
		agent.SetDestination (townhall.position + new Vector3 (0, 0.5f, 0));
		frozen = false;
		currentHealth = maxHealth;
	}

	void Update ()
	{
		//When enemy dies
		if (currentHealth <= 0)
		{
			Destroy(gameObject);
		}
	}

	public float CheckDistance()
	{
		//checks how far it is away from the townhall
		return (transform.position - townhall.position).magnitude;
	}

	//Speed Decrease when in contact with the net traps
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "TrapSlow")
		{
			StartCoroutine(DurationOfSlow());
		}

		if (other.tag == "Bullet") 
		{ 
			currentHealth -= 2;
		}
	}

	private IEnumerator DurationOfSlow()
	{
		agent.speed = 1.0f;
		yield return new WaitForSeconds(2.0f);
		agent.speed = 3.5f;
	}

	public IEnumerator FreezeCo()
	{
		agent.speed = 0;
		frozen = true;
		yield return new WaitForSeconds(3.0f);
		agent.speed = 3.5f;
		frozen = false;
	}
}