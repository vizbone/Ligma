using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour {
	
	[System.Serializable]
	public struct Waves
	{
		public EnemySpawnTemplate[] enemy;
	}

	public Waves[] wave;
	
}