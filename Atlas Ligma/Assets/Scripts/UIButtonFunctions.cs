using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIButtonFunctions : MonoBehaviour {

	public GameObject settingsMenu;

	ManaSystem manaSys;
	GridSystem gridSys;

	public GameObject currentBuild;

	private void Start()
	{
		manaSys = FindObjectOfType<ManaSystem>();
		gridSys = FindObjectOfType<GridSystem>();
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(1))
		{
			DestroyCurrentBuild();
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

	void DestroyCurrentBuild()
	{
		if (currentBuild != null)
		{
			Destroy(currentBuild);
			currentBuild = null;
		}
	}

	public void CrossbowBuild()
	{
		DestroyCurrentBuild();
		gridSys.buildIndex = 0;
		gridSys.buildMode = true;
		GameObject build = Instantiate(gridSys.towers[gridSys.buildIndex].buildModel, Vector3.zero, gridSys.towers[gridSys.buildIndex].buildModel.transform.rotation);
		currentBuild = build;
		gridSys.currentBuild = build;
	}

	public void CannonBuild()
	{
		DestroyCurrentBuild();
		gridSys.buildIndex = 1;
		gridSys.buildMode = true;
		GameObject build = Instantiate(gridSys.towers[gridSys.buildIndex].buildModel, Vector3.zero, gridSys.towers[gridSys.buildIndex].buildModel.transform.rotation);
		currentBuild = build;
		gridSys.currentBuild = build;
	}

	public void CatapultBuild()
	{
		DestroyCurrentBuild();
		gridSys.buildIndex = 2;
		gridSys.buildMode = true;
		GameObject build = Instantiate(gridSys.towers[gridSys.buildIndex].buildModel, Vector3.zero, gridSys.towers[gridSys.buildIndex].buildModel.transform.rotation);
		currentBuild = build;
		gridSys.currentBuild = build;
	}

	public void RocketsBuild()
	{
		DestroyCurrentBuild();
		gridSys.buildIndex = 3;
		gridSys.buildMode = true;
		GameObject build = Instantiate(gridSys.towers[gridSys.buildIndex].buildModel, Vector3.zero, gridSys.towers[gridSys.buildIndex].buildModel.transform.rotation);
		currentBuild = build;
		gridSys.currentBuild = build;
	}

	public void FireRateBuild()
	{
		DestroyCurrentBuild();
		gridSys.buildIndex = 4;
		gridSys.buildMode = true;
		GameObject build = Instantiate(gridSys.towers[gridSys.buildIndex].buildModel, Vector3.zero, gridSys.towers[gridSys.buildIndex].buildModel.transform.rotation);
		currentBuild = build;
		gridSys.currentBuild = build;
	}

	public void InvestmentBuild()
	{
		DestroyCurrentBuild();
		gridSys.buildIndex = 5;
		gridSys.buildMode = true;
		GameObject build = Instantiate(gridSys.towers[gridSys.buildIndex].buildModel, Vector3.zero, gridSys.towers[gridSys.buildIndex].buildModel.transform.rotation);
		currentBuild = build;
		gridSys.currentBuild = build;
	}
}
