using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
	[Header ("Main Particle Effect")]
	[SerializeField] ParticleSystem particleSystem;
	float lifeTime;
	float timeElapsed;

	[Header("Sound Effect")]
	[SerializeField] AudioSource audioSource;
	[SerializeField] bool isPaused;
	[SerializeField] bool finishedPlaying;

	// Start is called before the first frame update
	void Start()
    {
		audioSource = GetComponent<AudioSource>() ? GetComponent<AudioSource>() : null;

		finishedPlaying = audioSource == null ? true : false; //If have AudioSource, set finished playing to false

		if (audioSource != null) ManaSystem.inst.audioLibrary.PlayAudio(ManaSystem.inst.audioLibrary.airShipBomb, audioSource);

		isPaused = false;

		particleSystem = GetComponentInChildren<ParticleSystem>();
		lifeTime = particleSystem.main.duration + 0.5f;
	}

	void Update()
	{
		if (ManaSystem.gameStateS == GameStates.started || ManaSystem.gameStateS == GameStates.afterWin)
		{
			if (particleSystem.isPaused) particleSystem.Play();

			if (audioSource != null)
			{
				if (!audioSource.isPlaying)
				{
					if (isPaused)
					{
						isPaused = false;
						audioSource.UnPause();
					}
					else
					{
						finishedPlaying = true;
					}
				}
			}

			timeElapsed += Time.deltaTime;

			if (timeElapsed >= lifeTime && finishedPlaying) Destroy(gameObject);
		}
		else
		{
			if (particleSystem.isPlaying) particleSystem.Pause();

			if (audioSource != null && audioSource.isPlaying)
			{
				audioSource.Pause();
				isPaused = true;
			} 
		}
	}
}
