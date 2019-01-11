﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class UIButtonFunctions : MonoBehaviour {

	public GameObject settingsMenu;

	ManaSystem manaSys;
	GridSystem gridSys;

	public GameObject currentBuild;

	public AudioMixer masterMixer;
	public AudioSource uiSoundA;
	public AudioSource uiSoundB;

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
		//FindObjectOfType<AudioManager>().AudioToPlay("MenuAudioA");
		uiSoundA.Play();
		manaSys.gameState = GameStates.pause;
		settingsMenu.SetActive(true);
	}

	public void FromSettingsToGameplay()
	{
		//FindObjectOfType<AudioManager>().AudioToPlay("MenuAudioB");
		uiSoundB.Play();
		manaSys.gameState = GameStates.started;
		settingsMenu.SetActive(false);
	}

	public void VolumeBGM(float bgmLvl)
	{
		masterMixer.SetFloat("bgmVol", bgmLvl);
	}

	public void VolumeSFX(float sfxLvl)
	{
		masterMixer.SetFloat("sfxVol", sfxLvl);
	}

	/*public void MuteAllVolumes()
	{
		masterMixer.SetFloat("masterVol", -80.0f);
	}*/

	public void MuteVolumeBGM()
	{
		masterMixer.SetFloat("bgmVol", -80.0f);
		/*if (masterMixer.SetFloat("sfxVol", 0.0f)) bgmMuteActive.SetActive(true);
		else bgmMuteActive.SetActive(false);*/
	}

	public void MuteVolumeSFX()
	{
		masterMixer.SetFloat("sfxVol", -80.0f);
		/*if (masterMixer.SetFloat("sfxVol", 0.0f)) sfxMuteActive.SetActive(true);
		else sfxMuteActive.SetActive(false);*/
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
