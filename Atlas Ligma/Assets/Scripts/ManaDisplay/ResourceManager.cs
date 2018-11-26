using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
	public ManaAnimatedPrefabScript manaAnimatedprefab;

	public Transform townhall;
	public float manaSpeed;

	public Canvas numberCanvas;
	public Text txtSample;
	public float destroyTime;

	public Camera cam;

	public void CollectResource (Transform enemyPos)
	{
		ManaAnimatedPrefabScript manaSpawn = Instantiate (manaAnimatedprefab, enemyPos.position, Quaternion.identity);
		manaSpawn.endpos = townhall.position;
		manaSpawn.speed = manaSpeed;
	}

	public void DisplayText (Transform enemyPos, int addedMana) 
	{
		if (addedMana != 0)
		{
			Text txt = Instantiate (txtSample, enemyPos.position, numberCanvas.transform.rotation, numberCanvas.transform);
			txt.text = "+" + addedMana.ToString ();
			Destroy (txt, destroyTime);
		}
	}
}