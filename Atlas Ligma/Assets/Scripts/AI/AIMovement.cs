using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour {
	
	public enum Paths {path1, path2, path3, path4, seaPath1, seaPath2, air};
	public Transform[] path1;
	public Transform[] path2;
	public Transform[] path3;
	public Transform[] path4;

	public Transform[] seaPath1;
	public Transform[] seaPath2;

	public Transform seaPath1Spawn;
	public Transform seaPath2Spawn;

	public Transform townHall;

	public float[] path1DV;
	public float[] path2DV;
	public float[] path3DV;
	public float[] path4DV;

	void Awake () 
	{
		townHall = FindObjectOfType<Townhall>().transform;

		path1 = SetLastToTownHall(path1);
		path2 = SetLastToTownHall(path2);
		path3 = SetLastToTownHall(path3);
		path4 = SetLastToTownHall(path4);

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
		if (!isZero && script.path != Paths.air) script.currentDestination++;
		if (script.path == Paths.path1) ai.SetDestination (path1[script.currentDestination].position);
		if (script.path == Paths.path2) ai.SetDestination (path2[script.currentDestination].position);
		if (script.path == Paths.path3) ai.SetDestination (path3[script.currentDestination].position); 
		if (script.path == Paths.path4) ai.SetDestination (path4[script.currentDestination].position);
		if (script.path == Paths.seaPath1) ai.SetDestination (seaPath1[script.currentDestination].position);
		if (script.path == Paths.seaPath2) ai.SetDestination (seaPath2[script.currentDestination].position);
		if (script.path == Paths.air) ai.SetDestination (townHall.position);
	}

	public void NextPointSea (NavMeshAgent ai, AISea script)
	{
		script.currentDestination = script.currentDestination == 0 ? 1 : 0;
		if (script.path == Paths.seaPath1) ai.SetDestination (seaPath1[script.currentDestination].position);
		if (script.path == Paths.seaPath2) ai.SetDestination (seaPath2[script.currentDestination].position);
	}

	public Transform[] SetLastToTownHall(Transform[] paths)
	{
		if (paths.Length != 0)
		{
			if (paths[paths.Length - 1] == null)
			{
				paths[paths.Length - 1] = townHall;
			}
			else
			{
				//If not null but last way point is not townhall
				if (paths[paths.Length - 1].GetComponent<Townhall>() == null)
				{
					Transform[] newPath = new Transform[paths.Length + 1];

					for (int i = 0; i < paths.Length; i++)
					{
						newPath[i] = paths[i];
					}

					newPath[newPath.Length - 1] = townHall;

					paths = new Transform[newPath.Length];

					for (int i = 0; i < paths.Length; i++)
					{
						paths[i] = newPath[i];
					}
				}
			}
		}
		return paths;
	}
}