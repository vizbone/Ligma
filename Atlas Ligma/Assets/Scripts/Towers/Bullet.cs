using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum BulletType {crossbow, rocket, catapult, cannon};

public class Bullet : MonoBehaviour
{
	public TurretTemplate turret;
	public float lifetime;

	private void Start()
	{
		//SetValues(bulletType);
		lifetime = 5;
		Destroy(gameObject, lifetime);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "AI")
		{
			if (turret != null) turret.Hit(other.GetComponent<AITemplate>(), turret.isPrebuilt, gameObject);
		}
	}
}