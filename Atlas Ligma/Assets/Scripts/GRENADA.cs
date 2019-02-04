using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GRENADA : MonoBehaviour
{
	public int FORTHEMOTHERLAND;
	public ManaSystem YEETUSFEETUS;
	public AudioSource bombSound;
	void OnCollisionEnter (Collision other)
	{
		ManaSystem.inst.ManaMinus(FORTHEMOTHERLAND, other.collider.transform.position, 1);
		bombSound = GetComponent<AudioSource>();
		ManaSystem.inst.audioLibrary.PlayAudio(ManaSystem.inst.audioLibrary.airShipBomb, bombSound);
		Destroy(gameObject);
	}
}