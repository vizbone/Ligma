using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameStates {preStart, started, pause, win, lose, afterWin, gameComplete};

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
	public ManaFeedback manaDrop;
	public Transform canvas;
	public AudioSource manaGainedSound;

	[Header("Overlayed GUI")]
	public GUIOverlay gui;
	public UIButtonFunctions buttonFunctionUI;

	[Header("Systems and Managers")]
	public WaveSystem waveSystem;
	public EventsManager eventsManager;

	private void Start()
	{
		gui = FindObjectOfType<GUIOverlay>();
		buttonFunctionUI = FindObjectOfType<UIButtonFunctions>();
		inst = this;

		TurretTemplate.amplitude = 3;

		gameStateS = GameStates.started;
		gameState = gameStateS;
		currentMana = startingMana;

		waveSystem = GetComponent<WaveSystem>();
		eventsManager = GetComponent<EventsManager>();
	}

	void Update()
	{
		if (gameState == GameStates.started || gameStateS == GameStates.afterWin)
		{
			/*if (Input.GetKeyDown(KeyCode.L)) gameStateS = GameStates.lose;
			if (Input.GetKeyDown(KeyCode.O)) gameStateS = GameStates.win;*/

			if (Input.GetKeyDown(KeyCode.Escape))
			{
					//FindObjectOfType<AudioManager>().AudioToPlay("MenuAudioA");
					buttonFunctionUI.uiSoundA.Play();
					gameStateS = GameStates.pause;
					buttonFunctionUI.settingsMenu.SetActive(true);
			}
		}

		Functions ();
	}

	void Functions ()
	{
		UpdateGameState ();
	}

	void UpdateGameState()
	{
		if (gameState == GameStates.started)
		{
			//Players win the game once their Current Mana is >= Max Mana
			if (currentMana >= maxMana)
				gameStateS = GameStates.win;
			else if (currentMana <= 0)
				gameStateS = GameStates.lose;
		}
		else if (gameState == GameStates.afterWin)
		{
			if (currentMana <= 0 || waveSystem.allWavesCleared)
				gameStateS = GameStates.gameComplete;
		}
		/*if (gameState == GameStates.pause) Time.timeScale = 0; //Time scale does not work with animation but mehhh
		else Time.timeScale = 1;*/

		gameState = gameStateS;
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
		manaDrop.transform.localPosition += (transform.forward * -1) * offset;
	}
}