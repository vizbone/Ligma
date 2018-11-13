using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour {

	public int currentDestination;
	NavMeshAgent agent;
	AIMovement ai;

	void Start () 
	{
		ai = FindObjectOfType<AIMovement> ();
		agent = GetComponent<NavMeshAgent> ();
		currentDestination = 0;
		agent.SetDestination (ai.points[0].transform.position);
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Waypoint") ai.NextPoint (agent, this);
	}
}