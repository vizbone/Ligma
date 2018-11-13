using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaSystem : MonoBehaviour
{
	public int maxMana; //Amt of mana players will have to reach to win the game. MAY differ for each level
	public int currentMana;

	[SerializeField] private int startingMana; //Amt of mana players will start with. Will differ for each level

	private void Start()
	{
		currentMana = startingMana;
	}

	void Update()
	{
		//Players win the game once their Current Mana is >= Max Mana
		if (currentMana > maxMana) print("Victory");
		else if (currentMana <= 0) print("Defeated");
	}

	//Subtracts mana from bank
	public void ManaMinus (int amount) 
	{
		currentMana -= amount;
	}

	//Adds mana from bank
	public void ManaAdd (int amount)
	{
		currentMana += amount;
	}

	//public int manaRegenPerTick;
	//public int manaRegenRate;

	//bool cLock;
	//float time;

	//Old Mana System
	/*void Start ()
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
	}*/
}