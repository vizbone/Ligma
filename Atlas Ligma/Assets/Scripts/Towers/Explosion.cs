using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	public TurretTemplate turret;
	[SerializeField] ParticleSystem particleSystem;
	[SerializeField] Collider collider;

	// Use this for initialization
	void Start ()
	{
		particleSystem = GetComponent<ParticleSystem>();
		collider = GetComponent<Collider>();
		Destroy(gameObject, particleSystem.main.duration + 0.5f);
		Invoke("OffCollider", 0.25f);
	}

	public void OffCollider()
	{
		collider.enabled = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		//Turret should be a catapult
		if (other.tag == "AI") if (turret != null) turret.Hit(other.GetComponent<AITemplate>(), turret.isPrebuilt, gameObject, true);
	}
}
