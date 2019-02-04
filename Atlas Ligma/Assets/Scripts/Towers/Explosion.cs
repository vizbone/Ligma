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
		explosionSound = GetComponent<AudioSource>();
		Destroy(gameObject, particleSystem.main.duration + 0.5f);
		Invoke("OffCollider", 0.25f);

		ManaSystem.inst.audioLibrary.PlayAudio(ManaSystem.inst.audioLibrary.catapultExplosion, explosionSound);
	}

	public void OffCollider()
	{
		collider.enabled = false;
	}

	private void Update()
	{
		if (ManaSystem.gameStateS == GameStates.started || ManaSystem.gameStateS == GameStates.afterWin)
		{
			if (particleSystem.isPaused) particleSystem.Play();
		}
		else
		{
			if (particleSystem.isPlaying) particleSystem.Pause();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		//Turret should be a catapult
		//0 refers to the hitCount. Not needed for explosion
		if (other.tag == "AI")
		{
			if (turret != null)
			{
				turret.Hit(other.GetComponentInParent<AITemplate>(), turret.isPrebuilt, gameObject, 0, true);
			}
		}
	}
}
