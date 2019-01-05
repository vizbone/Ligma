using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	public TurretTemplate turret;
	[SerializeField] ParticleSystem particleSystem;
	[SerializeField] Collider collider;

	public AudioSource explosionSound;

	// Use this for initialization
	void Start ()
	{
		particleSystem = GetComponent<ParticleSystem>();
		collider = GetComponent<Collider>();
		Destroy(gameObject, particleSystem.main.duration + 0.5f);
		Invoke("OffCollider", 0.25f);
		//FindObjectOfType<AudioManager>().AudioToPlay("CatapultExplosion");
		explosionSound.Play();
	}

	public void OffCollider()
	{
		collider.enabled = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		//Turret should be a catapult
		//0 refers to the hitCount. Not needed for explosion
		if (other.tag == "AI") if (turret != null) turret.Hit(other.GetComponentInParent<AITemplate>(), turret.isPrebuilt, gameObject, 0, true);
	}
}
