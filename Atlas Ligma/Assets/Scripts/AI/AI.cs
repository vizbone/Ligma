using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour {

	public AIMovement.Paths path;
	public int currentDestination;
	NavMeshAgent agent;
	AIMovement ai;

	void Start () 
	{
		ai = FindObjectOfType<AIMovement> ();
		agent = GetComponent<NavMeshAgent> ();
		currentDestination = 0;
		ai.NextPoint (agent, this, true);
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Waypoint") ai.NextPoint (agent, this, false);
	}

	public float CheckDistance () 
	{
		float fractional = 
		path == AIMovement.Paths.path1 ? (ai.path1[currentDestination].transform.position - transform.position).magnitude :
		path == AIMovement.Paths.path2 ? (ai.path2[currentDestination].transform.position - transform.position).magnitude :
		path == AIMovement.Paths.path3 ? (ai.path3[currentDestination].transform.position - transform.position).magnitude :
		(ai.path4[currentDestination].transform.position - transform.position).magnitude;

		int arrayLength = 
		path == AIMovement.Paths.path1 ? ai.path1DV.Length :
		path == AIMovement.Paths.path2 ? ai.path2DV.Length :
		path == AIMovement.Paths.path3 ? ai.path3DV.Length :
		ai.path4DV.Length;

		float remainingDist = 0;

		for (int i = currentDestination; i < arrayLength; i++) 
		{ 
			remainingDist += 
			path == AIMovement.Paths.path1 ? ai.path1DV[i] :
			path == AIMovement.Paths.path2 ? ai.path2DV[i] :
			path == AIMovement.Paths.path3 ? ai.path3DV[i] :
			ai.path4DV[i];
		}
		return fractional + remainingDist;
	}
}