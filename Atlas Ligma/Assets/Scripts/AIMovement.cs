using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour {

	public Transform[] points;
	
	public void NextPoint (NavMeshAgent ai, AI script) 
	{
		script.currentDestination++;
		ai.SetDestination (points[script.currentDestination].position); 
	}
}