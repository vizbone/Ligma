using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Audios
{
	public string name;				//name of the audio
	public AudioClip audioClip;			//the audio clip itself;

	[Range(0f,1f)]					//min and max of the volume level
	public float volume;			//volume of the clip
		
	[Range(0.1f, 3f)]				//min and max of the pitch
	public float pitch;				//oitch of the clip

	[HideInInspector]
	public AudioSource source;		//the value to store the audio 
	
}
