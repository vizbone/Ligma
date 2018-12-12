﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum BulletType {crossbow, rocket, catapult, cannon};

public class Bullet : MonoBehaviour
{
	public TurretTemplate turret;
	public float lifetime;
	public int hitCount; //Only used by Cannon

	public float frequency1;
	public Vector3 target;
	public float amplitude;
	public float speed;
	public bool catapult;

	public Vector3 oriPos;
	public float currentStep;
	bool cLock;
	Rigidbody rb;

	private void Start()
	{
		//SetValues(bulletType);
		hitCount = 0;
		//lifetime = 5;
		Destroy(gameObject, lifetime);
		oriPos = transform.position;

		if (catapult)
		{
			rb = GetComponent<Rigidbody> ();
			rb.useGravity = false;
			cLock = false;
			currentStep = 0;
			target = new Vector3 (target.x, target.y + 0.5f, target.z);
		}
	}

	void Update ()
	{
		if (catapult && !cLock) StartCoroutine (Iteration ());
	}

	IEnumerator Iteration ()
	{
		cLock = true;
		if (currentStep < 1) MathFunctions.ParabolicCurve (target, amplitude, currentStep, transform, frequency1, oriPos);
		currentStep = Mathf.Min (currentStep + 2 * Time.deltaTime, 1);
		yield return new WaitForSeconds (1 / speed);
		cLock = false;
		if (currentStep >= 1) Destroy (gameObject);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "AI")
		{
			if (turret != null)
			{
				turret.Hit(other.GetComponentInParent<AITemplate>(), turret.isPrebuilt, gameObject, hitCount);
			}
		}
	}
}