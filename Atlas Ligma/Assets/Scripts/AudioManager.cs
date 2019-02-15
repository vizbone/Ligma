using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public struct SFX
{
	public AudioClip audioClip; //the audio clip itself;
	[Space(10)]

	[Range(0f, 1f)]
	public float defaultVolume; //Set Default Volume of the clip
	[Range(0f, 1f)]
	public float minVolume; //Set Minimum Volume for Randomisation
	[Range(0f, 1f)]
	public float maxVolume; //Set Maximum Volume for Randomisation
	[Space (10)]

	[Range(0f, 1f)]
	public float defaultPitch; //Set Default Pitch of the clip
	[Range(0f, 1f)]
	public float minPitch; //Set Minimum Volume for Randomisation
	[Range(0f, 1f)]
	public float maxPitch; //Set Maximum Volume for Randomisation
}

public class AudioManager : MonoBehaviour
{
	[Header("All Turret Audio")]
	#region 
	public SFX cannon;
	[Space(15)]

	public SFX catapult;
	public SFX catapultExplosion;
	[Space(15)]

	public SFX crossbow;
	[Space(15)]

	public SFX rocket;
	[Space(15)]

	public SFX build;
	[Space (15)]

	public SFX destroy;
	[Space (15)]
	#endregion

	[Header("All Enemy Audio")]
	#region 
	//public SFX skeletonWalk;
	public SFX skeletonAttack;
	public SFX skeletonDeath;
	[Space(15)]

	//public SFX boatMovement;
	public SFX unloading;
	public SFX seaEnemyDestroyed;
	[Space(15)]

	//public SFX airShipMovement;
	//public SFX airShipAttack;
	public SFX airShipBomb;
	public SFX airShipDestroyed;
	[Space(15)]
	#endregion

	[Header("All Misc Sounds")]
	#region
	public SFX manaGain;
	public SFX turretSelect;
	public SFX siren;
	#endregion

	/// <summary>
	/// Plays the Specified SFX on the given Audio Source. Only keep Randomise Volume and Randomise Pitch to true
	/// if the Max and Min Difference is >= 0.1;
	/// </summary>
	/// <param name="audioToPlay"></param>
	/// <param name="source"></param>
	/// <param name="randomiseVolume"></param>
	/// <param name="randomisePitch"></param>
	public void PlayAudio(SFX audioToPlay, AudioSource source, bool randomiseVolume = true, bool randomisePitch = true)
	{
		source.clip = audioToPlay.audioClip;

		if (audioToPlay.maxVolume <= audioToPlay.minVolume)
		{
			//print("Minimum Volume and Maximum Volume has Error. Minimum Value is more than or equals to Maximum Value. Switching to Default Volume");
			randomiseVolume = false;
		}

		if (audioToPlay.maxPitch <= audioToPlay.minPitch)
		{
			//print("Minimum Pitch and Maximum Pitch has Error. Minimum Value is more than or equals to Maximum Value. Switching to Default Pitch");
			randomisePitch = false;
		}

		source.volume = randomiseVolume ? Random.Range(audioToPlay.minVolume, audioToPlay.maxVolume) : audioToPlay.defaultVolume;
		source.pitch = randomisePitch ? Random.Range(audioToPlay.minPitch, audioToPlay.maxPitch) : audioToPlay.defaultPitch;

		source.Play();
	}
}
