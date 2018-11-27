using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GRENADA : MonoBehaviour
{
	public int FORTHEMOTHERLAND;
	public ManaSystem YEETUSFEETUS;
	void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.tag == "TownHall")
		{
			YEETUSFEETUS.ManaMinus (FORTHEMOTHERLAND, other.collider.transform.position, 1);
			Destroy (gameObject);
		}
	}
}