using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaSystem : MonoBehaviour
{

	public int maxMana;
	public int currentMana;
	public int manaRegenPerTick;
	public int manaRegenRate;

	bool cLock;
	float time;

	void Start ()
	{
		if (manaRegenRate <= 0) { manaRegenRate = 1; }
		time = 1 / manaRegenRate;
		cLock = false;
		currentMana = maxMana; 
	}

	void Update ()
	{
		if (!cLock) { StartCoroutine ("spawnClock"); }
	}

	IEnumerator spawnClock () 
	{
		cLock = true;
		if (currentMana < maxMana) { currentMana += manaRegenPerTick; }
		if (currentMana > maxMana) { currentMana = maxMana; }
		yield return new WaitForSeconds (time);
		cLock = false;
	}

	//minuses mana from bank
	public void ManaMinus (int amount) 
	{
		currentMana -= amount;
	}

	//adds mana from bank
	public void ManaAdd (int amount)
	{
		currentMana += amount;
		if (currentMana > maxMana) { currentMana = maxMana; }
	}
}