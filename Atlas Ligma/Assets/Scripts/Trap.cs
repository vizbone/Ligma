using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

	public float dmgDoneToHealth = 10.0f;
	
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Enemy")
		{
			other.GetComponent<Enemy>().currentHealth -= dmgDoneToHealth;
			Destroy (gameObject);
		}
	}
}
