using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public Audios[] sfx;

	private void Awake()
	{
		foreach(Audios x in sfx)
		{
			x.source = gameObject.AddComponent<AudioSource>();      //x.source of class Audios = the gameobject's AudioSource
			x.source.clip = x.audioClip;                            //x.source of class Audios = the gameobject's AudioClip
			x.source.volume = x.volume;                             //x.source of class Audios = the gameobject's Volume
			x.source.pitch = x.pitch;                               //x.source of class Audios = the gameobject's Pitch
		}
	}
	public void AudioToPlay(string name)
	{
		Audios x = Array.Find(sfx, Audios => Audios.name == name);	//Searching of the same name in the array
		if(x == null)
		{
			Debug.Log("Sound" + name + "Not Found");
			return;
		}
		x.source.Play();											//Playing the audio clip of that name

	}
}
