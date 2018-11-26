using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GRENADA : MonoBehaviour
{
	public int FORTHEMOTHERLAND;
	public ManaSystem YEETUSFEETUS;
	void OnCollisionEnter (Collision other)
	{
		YEETUSFEETUS.ManaMinus (FORTHEMOTHERLAND);
		Destroy (gameObject);
	}
}