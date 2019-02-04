using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
	[SerializeField] ParticleSystem particleSystem;

	// Start is called before the first frame update
	void Start()
    {
		particleSystem = GetComponent<ParticleSystem>();
		Destroy(gameObject, particleSystem.main.duration + 0.5f);
	}
}
