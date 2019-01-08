using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : AITemplate {

	public AIMovement.Paths path;
	public int currentDestination;

	NavMeshAgent agent;
	AIMovement ai;

	public float fireRate;
	public int dmg;
	public GRENADA YYYEEEEEEETTTT;
	public Vector3 offset;

	float time;
	bool cLock;

	ManaSystem manaSys;
	public AudioSource skeletonAttackSound;

	protected override void Start () 
	{
		ai = FindObjectOfType<AIMovement>();
		manaSys = FindObjectOfType<ManaSystem> ();
		if (path != AIMovement.Paths.air)
		{
			int temp = Find ();
			if (temp == 0)
			{
				path = AIMovement.Paths.path1;
			} else if (temp == 1)
			{
				path = AIMovement.Paths.path2;
			} else if (temp == 2)
			{
				path = AIMovement.Paths.path3;
			} else if (temp == 3)
			{
				path = AIMovement.Paths.path4;
			}
		}

		base.Start();
		agent = GetComponent<NavMeshAgent> ();
		currentDestination = 0;
		ai.NextPoint (agent, this, true);
		cLock = false;
		
		if (enemyType == AttackType.air)
		{
			if (fireRate == 0) fireRate = 1;
			time = 1 / fireRate;
		}
	}

	int Find()
	{
		float[] temp = new float[4];
		int returnee = 0;

		temp[0] = (ai.path1[0].position - transform.position).magnitude;
		temp[1] = (ai.path2[0].position - transform.position).magnitude;
		temp[2] = (ai.path3[0].position - transform.position).magnitude;
		temp[3] = (ai.path4[0].position - transform.position).magnitude;

		float tempTwo = temp[0];

		for (int i = 1; i < temp.Length; i++)
		{
			if (temp[i] < tempTwo)
			{
				tempTwo = temp[i];
				returnee = i;
			}
		}

		return returnee;
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Waypoint" && path != AIMovement.Paths.air) ai.NextPoint (agent, this, false);
	}

	void OnTriggerStay (Collider other)
	{
		if (other.tag == "Town Hall" && enemyType == AttackType.air && !cLock) StartCoroutine (ALLAHUAKBAAR ());
	}

	void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.tag == "Town Hall" && enemyType == AttackType.ground)
		{
			//FindObjectOfType<AudioManager>().AudioToPlay("SkeletonAttack");
			skeletonAttackSound.Play();
			manaSys.ManaMinus (dmg, transform.position, 0);
			Destroy (gameObject);
		}
	}

	IEnumerator ALLAHUAKBAAR ()
	{
		cLock = true;
		GRENADA GRENADA = Instantiate (YYYEEEEEEETTTT, transform.GetChild (0).position + offset, Quaternion.identity);
		GRENADA.FORTHEMOTHERLAND = dmg;
		GRENADA.YEETUSFEETUS = manaSys;
		yield return new WaitForSeconds (time);
		cLock = false;
	}

	public override float CheckDistance () 
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