using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despacito : MonoBehaviour
{
	AudioSource audio;

	void Start ()
	{
		audio = GetComponent<AudioSource> ();
	}

	void Update ()
	{
		if (ManaSystem.gameStateS != GameStates.pause && !audio.isPlaying)
		{
			Destroy (gameObject);
		}
	}
}