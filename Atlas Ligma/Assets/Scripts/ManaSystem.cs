using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates {preStart, started, pause, win, lose};

public class ManaSystem : MonoBehaviour
{
<<<<<<< HEAD
=======
	//Game State
	public GameStates gameState = GameStates.preStart; //Stores the state of the game

	//Mana System
	[SerializeField] private int startingMana = 1000; //Amt of mana players will start with. Will differ for each level
	public int maxMana; //Amt of mana players will have to reach to win the game. MAY differ for each level
	public int currentMana; //Amt of mana player has. Updated throughout the game

	//Wave System
	public int totalWaves = 10; //Total number of waves the level will have
	public int currentWave; //Stores the current wave the player is in

	private void Start()
	{
		gameState = GameStates.preStart;
		currentMana = startingMana;
	}

	void Update()
	{
		UpdateGameState();

		//Players win the game once their Current Mana is >= Max Mana
		if (currentMana > maxMana) gameState = GameStates.win;
		else if (currentMana <= 0) gameState = GameStates.lose;
	}

	void UpdateGameState()
	{
		if (gameState == GameStates.pause) Time.timeScale = 0; //Time scale does not work with animation but mehhh
		else Time.timeScale = 1;
	}
>>>>>>> c39434590d8fedafe283bbae31b9903dd879a195

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