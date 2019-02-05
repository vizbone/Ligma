using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
	[SerializeField] ParticleSystem particleSystem;
	float lifeTime;
	float timeElapsed;

	// Start is called before the first frame update
	void Start()
    {
		particleSystem = GetComponentInChildren<ParticleSystem>();
		lifeTime = particleSystem.main.duration + 0.5f;
	}

	void Update()
	{
		if (ManaSystem.gameStateS == GameStates.started || ManaSystem.gameStateS == GameStates.afterWin)
		{
			if (particleSystem.isPaused) particleSystem.Play();

			timeElapsed += Time.deltaTime;

			if (timeElapsed >= lifeTime) Destroy(gameObject);
		}
		else
		{
			if (particleSystem.isPlaying) particleSystem.Pause();
		}
	}
}
