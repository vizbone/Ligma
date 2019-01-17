using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameStates {preStart, started, pause, win, lose, afterWin};

public class ManaSystem : MonoBehaviour
{
	public static ManaSystem inst;

	[Header ("Game State Manager")]
	public static GameStates gameStateS = GameStates.preStart; //Stores the state of the game
	public GameStates gameState;

	[Header ("Mana System")]
	[SerializeField] private int startingMana = 1000; //Amt of mana players will start with. Will differ for each level
	public int maxMana; //Amt of mana players will have to reach to win the game. MAY differ for each level
	public int currentMana; //Amt of mana player has. Updated throughout the game

	[Header("Mana UI (World Space)")]
	public Text currentManaDisplay;
	public Slider manaSlider;
	public ManaFeedback manaDrop;
	public Transform canvas;
	public AudioSource manaGainedSound;

	[Header("Overlayed GUI")]
	public GUIOverlay gui;

	private void Start()
	{
		gui = FindObjectOfType<GUIOverlay>();
		inst = this;

		TurretTemplate.amplitude = 3;

		gameStateS = GameStates.started;
		gameState = gameStateS;
		currentMana = startingMana;
	}

	void Update()
	{
		if (gameState == GameStates.started)
		{
			if (Input.GetKeyDown(KeyCode.L)) gameStateS = GameStates.lose;
			if (Input.GetKeyDown(KeyCode.O)) gameStateS = GameStates.win;
		}

		Functions ();
	}

	void Functions ()
	{
		UpdateGameState ();

		//Display Current Mana
		currentManaDisplay.text = currentMana.ToString () + "/" + "2000";
		manaSlider.value = currentMana;
	}

	void UpdateGameState()
	{
		gameState = gameStateS;

		if (gameState == GameStates.started)
		{
			//Players win the game once their Current Mana is >= Max Mana
			if (currentMana >= maxMana)
				gameState = GameStates.win;
			else if (currentMana <= 0)
				gameState = GameStates.lose;
		}
		/*if (gameState == GameStates.pause) Time.timeScale = 0; //Time scale does not work with animation but mehhh
		else Time.timeScale = 1;*/
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

	//UI Animations
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