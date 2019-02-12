using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class UIButtonFunctions : MonoBehaviour {

	public GameObject settingsMenu;

	[Header("Systems and Managers")]
	ManaSystem manaSys;
	GridSystem gridSys;

	public GameObject currentBuild;

	[Header ("Sounds")]
	public AudioMixer masterMixer;
	public AudioSource uiSoundA;
	public AudioSource uiSoundB;

	public Slider masterSlider;
	public Slider bgmSlider;
	public Slider sfxSlider;

	public Toggle fastFoward;

	[Header("Time Scale")]
	[SerializeField] int timeScale;

	private void Start()
	{
		if (ManaSystem.inst != null)
		{
			manaSys = FindObjectOfType<ManaSystem>();
			gridSys = FindObjectOfType<GridSystem>();
			timeScale = PlayerPrefs.GetInt("Time Scale", 1);
			if (timeScale == 1) fastFoward.isOn = false;
			else fastFoward.isOn = true;
			Time.timeScale = timeScale;
		}
		else Time.timeScale = 1;

		//Load the Saved Values throughout scenes
		//masterSlider.value = PlayerPrefs.GetFloat("masterSavedVol", 0.3f);
		bgmSlider.value = PlayerPrefs.GetFloat("bgmSavedVol", 0.3f);
		sfxSlider.value = PlayerPrefs.GetFloat("sfxSavedVol", 0.3f);
		//masterMixer.SetFloat("masterVol", Mathf.Log(masterSlider.value) * 20);
		masterMixer.SetFloat("bgmVol", Mathf.Log(bgmSlider.value) * 20);
		masterMixer.SetFloat("sfxVol", Mathf.Log(sfxSlider.value) * 20);
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(1))
		{
			DestroyCurrentBuild();
			gridSys.buildMode = false;
		}
	}

	public void BackToMainMenu ()
	{
		if (Time.timeScale != 1) Time.timeScale = 1;

		if (ManaSystem.inst != null)
		{
			if (!ManaSystem.inst.gui.endScreenIsPlaying)  SceneManager.LoadScene("Main Menu");
		}
		else SceneManager.LoadScene("Main Menu");
	}
	
	public void Continue ()
	{
		if (!ManaSystem.inst.gui.endScreenIsPlaying)
		{
			if (Time.timeScale == 0) Time.timeScale = timeScale;

			ManaSystem.gameStateS = GameStates.afterWin;
			ManaSystem.inst.gui.lerpTime = null;
			ManaSystem.inst.gui.endScreenIsPlaying = true;
			ManaSystem.inst.gui.lerpTime = new float[2];
			ManaSystem.inst.gui.uiAnim += ManaSystem.inst.gui.HideEndScreen;
		}
	}

	public void NextLevel (string arg)
	{
		if (Time.timeScale == 0) Time.timeScale = timeScale;

		if (ManaSystem.inst != null)
		{
			if (!ManaSystem.inst.gui.endScreenIsPlaying) SceneManager.LoadScene(arg);
		}
		else SceneManager.LoadScene(arg);
	}

	public void Retry ()
	{
		if (Time.timeScale == 0) Time.timeScale = timeScale;

		SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
	}

	public void FastFoward(bool isOn)
	{
		if (isOn) timeScale = 2;
		else timeScale = 1;

		PlayerPrefs.SetInt("Time Scale", timeScale);

		if (Time.timeScale == 0) return;

		Time.timeScale = timeScale;
	}

	public void VolumeMaster(float masterLvl)
	{
		masterMixer.SetFloat("masterVol", Mathf.Log(masterLvl) * 20);
		PlayerPrefs.SetFloat("masterSavedVol", masterLvl);
	}

	public void VolumeBGM(float bgmLvl)
	{
		masterMixer.SetFloat("bgmVol", Mathf.Log(bgmLvl) * 20);
		PlayerPrefs.SetFloat("bgmSavedVol", bgmLvl);
	}

	public void VolumeSFX(float sfxLvl)
	{
		masterMixer.SetFloat("sfxVol", Mathf.Log(sfxLvl) * 20);
		PlayerPrefs.SetFloat("sfxSavedVol", sfxLvl);
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
			manaSys.projector.CancelBuildProjection();
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
}
