using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GRENADA : MonoBehaviour
{
	public int FORTHEMOTHERLAND;
	public ManaSystem YEETUSFEETUS;
	public AudioSource bombSound;

	Rigidbody rb;
	Vector3 vel;

	public bool tick;

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		tick = false;
	}

	void OnCollisionEnter (Collision other)
	{
		ManaSystem.inst.ManaMinus(FORTHEMOTHERLAND, other.collider.transform.position, 1);
		bombSound = GetComponentInChildren<AudioSource>();
		ManaSystem.inst.audioLibrary.PlayAudio(ManaSystem.inst.audioLibrary.airShipBomb, bombSound);
		Destroy(gameObject);
	}

	void Update ()
	{
		if (ManaSystem.gameStateS != GameStates.started && ManaSystem.gameStateS != GameStates.afterWin && !tick)
		{
			vel = rb.velocity;
			rb.velocity = Vector3.zero;
			rb.useGravity = false;
			tick = true;
			print ("yes");
		} else if ((ManaSystem.gameStateS == GameStates.started || ManaSystem.gameStateS == GameStates.afterWin) && tick)
		{
			rb.velocity = vel;
			rb.useGravity = true;
			tick = false;
			print ("wahtttt");
		}
	}
}