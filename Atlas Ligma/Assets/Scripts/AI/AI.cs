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

	float cooldown;
	float time;

	bool canShoot;

	protected override void Start () 
	{
		ai = FindObjectOfType<AIMovement>();
		if (path != AIMovement.Paths.air)
		{
			int temp = Find ();
			if (temp == 0)
			{
				path = AIMovement.Paths.path1;
				gameObject.layer = 13;
			} else if (temp == 1)
			{
				path = AIMovement.Paths.path2;
				gameObject.layer = 14;
			} else if (temp == 2)
			{
				path = AIMovement.Paths.path3;
				gameObject.layer = 15;
			} else if (temp == 3)
			{
				path = AIMovement.Paths.path4;
				gameObject.layer = 16;
			}
		}

		base.Start();
		agent = GetComponent<NavMeshAgent> ();
		defaultAngularSpeed = agent.angularSpeed;
		currentDestination = 0;
		ai.NextPoint (agent, this, true);
		canShoot = true;
		
		if (enemyType == AttackType.air)
		{
			if (fireRate == 0) fireRate = 1;
			cooldown = 1 / fireRate;
			time = cooldown;
		}
	}

	private void Update()
	{
		if (ManaSystem.gameStateS == GameStates.started || ManaSystem.gameStateS == GameStates.afterWin)
		{
			if (agent.velocity.magnitude == 0 && defaultMoveSpeed.magnitude > 0) agent.velocity = defaultMoveSpeed;

			if (agent.angularSpeed == 0) agent.angularSpeed = defaultAngularSpeed;
		}
		else
		{
			if (agent.velocity != Vector3.zero) defaultMoveSpeed = agent.velocity;

			if (agent.angularSpeed > 0) agent.angularSpeed = 0;

			agent.velocity = Vector3.zero;
		}


		if (ManaSystem.gameStateS == GameStates.started || ManaSystem.gameStateS == GameStates.afterWin)
		{
			time -= Time.deltaTime;
			if (time <= 0) canShoot = true;
		}

		base.Update ();
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
		if (other.tag == "Town Hall" && enemyType == AttackType.air && canShoot && (ManaSystem.gameStateS == GameStates.started || ManaSystem.gameStateS == GameStates.afterWin))
		{
			ALLAHUAKBAAR ();
			canShoot = false;
		}
	}

	void OnCollisionStay (Collision other)
	{
		if (other.gameObject.tag == "Town Hall" && enemyType == AttackType.ground)
		{
			//ManaSystem.inst.audioLibrary.PlayAudio(ManaSystem.inst.audioLibrary.skeletonAttack, audioSource);
			ManaSystem.inst.ManaMinus (dmg, transform.position, 0);
			Destroy (gameObject);
		}
	}

	void ALLAHUAKBAAR ()
	{
		GRENADA GRENADA = Instantiate (YYYEEEEEEETTTT, transform.GetChild (0).position + offset, Quaternion.identity);
		GRENADA.FORTHEMOTHERLAND = dmg;
		GRENADA.YEETUSFEETUS = ManaSystem.inst; //Not sure if using a static inst would change anything
		time = cooldown;
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