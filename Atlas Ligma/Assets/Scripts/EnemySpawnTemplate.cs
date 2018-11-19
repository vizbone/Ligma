using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemySpawn", menuName = "EnemySpawn")]
public class EnemySpawnTemplate : ScriptableObject {

	public GameObject typeOfEnemy;
	public float interval;
}
