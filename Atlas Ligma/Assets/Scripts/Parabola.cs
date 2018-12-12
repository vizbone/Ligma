using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parabola : MonoBehaviour
{

	public float speedAmp;
	public float amplitude;
	public float distance;

	Vector3 InitialVelocity (float amp, float dist)
	{
		return new Vector3 (1, amp * (1 / dist) * Mathf.PI, 0);
	}

	void Set (Rigidbody rb)
	{
		rb.velocity = InitialVelocity (amplitude, distance);
	}
	
}