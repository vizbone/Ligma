using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GRENADA : MonoBehaviour
{
	public int FORTHEMOTHERLAND;
	void OnCollisionEnter (Collision other)
	{
		FindObjectOfType<ManaSystem> ().currentMana -= FORTHEMOTHERLAND;
		Destroy (gameObject);
	}
}