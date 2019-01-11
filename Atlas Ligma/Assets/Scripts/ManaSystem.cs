using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameStates {preStart, started, pause, win, lose};

public class ManaSystem : MonoBehaviour
{
	//Game State
	public GameStates gameState = GameStates.preStart; //Stores the state of the game

	//Mana System
	[SerializeField] private int startingMana = 1000; //Amt of mana players will start with. Will differ for each level
	public int maxMana; //Amt of mana players will have to reach to win the game. MAY differ for each level
	public int currentMana; //Amt of mana player has. Updated throughout the game

	//Wave System
	public int totalWaves = 10; //Total number of waves the level will have
	public int currentWave; //Stores the current wave the player is in

	//Mana UI
	public Text currentManaDisplay;
	public Slider manaSlider;
	public ManaFeedback manaDrop;
	public Transform canvas;
	public AudioSource manaGainedSound;

	private void Start()
	{
		TurretTemplate.amplitude = 3;

		gameState = GameStates.preStart;
		currentMana = startingMana;
	}

	void Update()
	{
		Functions ();
	}

	void Functions ()
	{
		UpdateGameState ();

		//Players win the game once their Current Mana is >= Max Mana
		if (currentMana > maxMana)
			gameState = GameStates.win;
		else if (currentMana <= 0)
			gameState = GameStates.lose;

		//Display Current Mana
		currentManaDisplay.text = currentMana.ToString () + "/" + "2000";
		manaSlider.value = currentMana;
	}

	void UpdateGameState()
	{
		if (gameState == GameStates.pause) Time.timeScale = 0; //Time scale does not work with animation but mehhh
		else Time.timeScale = 1;
	}

	//minuses mana from bank
	public void ManaMinus(int amount, Vector3 pos, float offset)
	{
		currentMana = Mathf.Max (0, currentMana - amount);
		DisplayText (-amount, pos, offset);
	}

	//adds mana from bank
	public void ManaAdd(int amount, Vector3 pos, float offset)
	{
		//FindObjectOfType<AudioManager>().AudioToPlay("ManaGain");
		manaGainedSound.Play();
		currentMana += amount;
		if (currentMana > maxMana) { currentMana = maxMana; }
		DisplayText (amount, pos, offset);
	}

	public void DisplayText (int addedMana, Vector3 pos, float offset)
	{
		ManaFeedback manaDrop = Instantiate (this.manaDrop, pos, canvas.transform.rotation, canvas);
		Text txt = manaDrop.manaDrop;
		if (addedMana < 0)
		{
			manaDrop.targetColor = Color.red;
			txt.text = addedMana.ToString();
		} 
		else
		{
			manaDrop.targetColor = new Color (0, 0.65f, 1);
			txt.text = "+" + addedMana;
		}
		//manaDrop.icon.color = Color.clear;
		//manaDrop.manaDrop.color = Color.clear;
		manaDrop.transform.localPosition += (transform.forward * -1) * 5;
	}

	/*public int maxMana;
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
	}*/
}