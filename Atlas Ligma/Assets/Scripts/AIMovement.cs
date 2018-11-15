using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour {
	
	public enum Paths {path1, path2, path3, path4};
	public Transform[] path1;
	public Transform[] path2;
	public Transform[] path3;
	public Transform[] path4;

	public float[] path1DV;
	public float[] path2DV;
	public float[] path3DV;
	public float[] path4DV;

	void Awake () 
	{
		path1DV = new float[path1.Length - 1];
		path2DV = new float[path2.Length - 1];
		path3DV = new float[path3.Length - 1];
		path4DV = new float[path4.Length - 1];

		for (int i = 0; i < path1DV.Length; i++) path1DV[i] = (path1[i + 1].position - path1[i].position).magnitude;
		for (int i = 0; i < path2DV.Length; i++) path2DV[i] = (path2[i + 1].position - path2[i].position).magnitude;
		for (int i = 0; i < path3DV.Length; i++) path3DV[i] = (path3[i + 1].position - path3[i].position).magnitude;
		for (int i = 0; i < path4DV.Length; i++) path4DV[i] = (path4[i + 1].position - path4[i].position).magnitude;
	}

	public void NextPoint (NavMeshAgent ai, AI script, bool isZero) 
	{
		if (!isZero) script.currentDestination++;
		if (script.path == Paths.path1) ai.SetDestination (path1[script.currentDestination].position);
		if (script.path == Paths.path2) ai.SetDestination (path2[script.currentDestination].position);
		if (script.path == Paths.path3) ai.SetDestination (path3[script.currentDestination].position); 
		if (script.path == Paths.path4) ai.SetDestination (path4[script.currentDestination].position); 
	}
}