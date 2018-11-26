using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
	public Transform townHall;
	public ManaAnimatedPrefabScript manaDrop;
	public float manaSpeed;

	public Text text;
	public Transform canvas;

	public void CollectResource (Vector3 enemyPos)
	{
		ManaAnimatedPrefabScript mana = Instantiate (manaDrop, enemyPos, Quaternion.identity);
		mana.speed = manaSpeed;
		mana.endPos = townHall.position;
	}

	public void DisplayText (int addedMana, Vector3 enemyPos)
	{
		if (addedMana != 0)
		{
			Text txt = Instantiate (text, enemyPos, Quaternion.identity, canvas);
			txt.text = "+" + addedMana;
		}
	}
}