using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIButtonFunctions : MonoBehaviour {

	public GameObject settingsMenu;

	ManaSystem manaSys;
	GridSystem gridSys;

	private void Start()
	{
		manaSys = FindObjectOfType<ManaSystem>();
		gridSys = FindObjectOfType<GridSystem>();
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			gridSys.buildMode = false;
		}
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

	public void CrossbowBuild()
	{
		gridSys.buildIndex = 0;
		gridSys.buildMode = true;
		GameObject build = Instantiate(gridSys.towers[gridSys.buildIndex].buildModel, Vector3.zero, gridSys.towers[gridSys.buildIndex].buildModel.transform.rotation);
		gridSys.currentBuild = build;
	}

	public void CannonBuild()
	{
		gridSys.buildIndex = 1;
		gridSys.buildMode = true;
		GameObject build = Instantiate(gridSys.towers[gridSys.buildIndex].buildModel, Vector3.zero, gridSys.towers[gridSys.buildIndex].buildModel.transform.rotation);
		gridSys.currentBuild = build;
	}

	public void CatapultBuild()
	{
		gridSys.buildIndex = 2;
		gridSys.buildMode = true;
		GameObject build = Instantiate(gridSys.towers[gridSys.buildIndex].buildModel, Vector3.zero, gridSys.towers[gridSys.buildIndex].buildModel.transform.rotation);
		gridSys.currentBuild = build;
	}

	public void RocketsBuild()
	{
		gridSys.buildIndex = 3;
		gridSys.buildMode = true;
		GameObject build = Instantiate(gridSys.towers[gridSys.buildIndex].buildModel, Vector3.zero, gridSys.towers[gridSys.buildIndex].buildModel.transform.rotation);
		gridSys.currentBuild = build;
	}

	public void FireRateBuild()
	{
		gridSys.buildIndex = 4;
		gridSys.buildMode = true;
		GameObject build = Instantiate(gridSys.towers[gridSys.buildIndex].buildModel, Vector3.zero, gridSys.towers[gridSys.buildIndex].buildModel.transform.rotation);
		gridSys.currentBuild = build;
	}

	public void InvestmentBuild()
	{
		gridSys.buildIndex = 5;
		gridSys.buildMode = true;
		GameObject build = Instantiate(gridSys.towers[gridSys.buildIndex].buildModel, Vector3.zero, gridSys.towers[gridSys.buildIndex].buildModel.transform.rotation);
		gridSys.currentBuild = build;
	}
}
