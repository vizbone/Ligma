using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCircle : MonoBehaviour {

	public bool castSpell;

	// Use this for initialization
	void Start ()
	{
		castSpell = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	private void OnTriggerStay(Collider other)
	{
		if (castSpell)
		{
			if (other.tag == "Enemy" && !other.GetComponent<Enemy> ().frozen)
			{
				StartCoroutine(other.GetComponent<Enemy>().FreezeCo());
			}
		}
	}
}
