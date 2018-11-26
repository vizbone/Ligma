using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonFunctions : MonoBehaviour {

	public GameObject settingsMenu;

	ManaSystem manaSys;

	private void Start()
	{
		manaSys = FindObjectOfType<ManaSystem>();
	}

	public void Pause()
	{
		manaSys.gameState = GameStates.pause;
		settingsMenu.SetActive(true);
	}

	public void FromSettingsToGameplay()
	{
		manaSys.gameState = GameStates.started;
		settingsMenu.SetActive(false);
	}
}
