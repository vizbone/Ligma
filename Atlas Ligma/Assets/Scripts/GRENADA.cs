using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GRENADA : MonoBehaviour
{
	public int FORTHEMOTHERLAND;
	public ManaSystem YEETUSFEETUS;
	public AudioClip bombSound;
	void OnCollisionEnter (Collision other)
	{
		ManaSystem.inst.ManaMinus(FORTHEMOTHERLAND, other.collider.transform.position, 1);
		AudioSource.PlayClipAtPoint(bombSound, other.collider.transform.position);
		Destroy(gameObject);
	}
}