using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public Slider loadingBar;
	public GameObject loadingScreen;
	public GameObject creditsScreen;
	public Image currentImage;
	public Sprite[] loadingImages;
	public int currentLoadingImage;

	#region Fade
	//Fades
	public Image title;
	public Image start;
	public Image settings;
	public Image credits;
	public Image quit;
	public Image levelselbut;
	public Image startGame;

	public GameObject mainMenu;
	public GameObject levelSelect;

	public float fadeTime;
	public bool fadeFinish;

	public delegate void AnimationDelegates();
	public AnimationDelegates uiAnim;

	#endregion

	public void Start()
	{
		currentImage.sprite = loadingImages[0];
		fadeFinish = false; //MainToLevel
	}

	public void Update()
	{
		if (uiAnim != null) uiAnim();
	}

	public void LoadScreen(int sceneNumber)
	{
		StartCoroutine(LoadAsync(sceneNumber));
		InvokeRepeating("ChangeLoadingImage", 0f, 0.5f);
	}

	public void MainToLevelSelectFade()
	{
		uiAnim += FadeMTL;
	}

	public void LevelSelectToMainFade()
	{
		levelSelect.SetActive(false);
		start.gameObject.SetActive(true);
		settings.gameObject.SetActive(true);
		credits.gameObject.SetActive(true);
		quit.gameObject.SetActive(true);

		title.color = new Color(1, 1, 1, 1);
		start.color = new Color(1, 1, 1, 1);
		settings.color = new Color(1, 1, 1, 1);
		credits.color = new Color(1, 1, 1, 1);
		quit.color = new Color(1, 1, 1, 1);

		mainMenu.SetActive(true);
	}

	public void CreditsToMainMenu()
	{
		creditsScreen.SetActive(false);
		mainMenu.SetActive(true);
	}

	void FadeMTL()
	{
		#region FadeAnims
		fadeTime += 0.2f * Time.deltaTime;
		title.color = Color.Lerp(title.color, new Color(1, 1, 1, 0), fadeTime);

		if (title.color.a < 0.05f)
		{
			title.color = new Color(1, 1, 1, 0);
			title.gameObject.SetActive(false);
		}

		start.color = Color.Lerp(start.color, new Color(1, 1, 1, 0), fadeTime);

		if (start.color.a < 0.05f)
		{
			start.color = new Color(1, 1, 1, 0);
			start.gameObject.SetActive(false);
		}

		settings.color = Color.Lerp(settings.color, new Color(1, 1, 1, 0), fadeTime);

		if (settings.color.a < 0.05f)
		{
			settings.color = new Color(1, 1, 1, 0);
			settings.gameObject.SetActive(false);
		}

		credits.color = Color.Lerp(credits.color, new Color(1, 1, 1, 0), fadeTime);

		if (start.color.a < 0.05f)
		{
			credits.color = new Color(1, 1, 1, 0);
			credits.gameObject.SetActive(false);
		}

		quit.color = Color.Lerp(quit.color, new Color(1, 1, 1, 0), fadeTime);

		if (quit.color.a < 0.05f)
		{
			quit.color = new Color(1, 1, 1, 0);
			quit.gameObject.SetActive(false);
			mainMenu.SetActive(false);
			//fadeTime = MathFunctions.ResetLerpTime();
			fadeFinish = true;
		}

		if (fadeFinish)
		{
			levelSelect.SetActive(true);

			fadeTime += 0.2f * Time.deltaTime;
			levelselbut.color = Color.Lerp(levelselbut.color, new Color(1, 1, 1, 1), fadeTime);

			if (levelselbut.color.a > 0.90f)
			{
				levelselbut.color = new Color(1, 1, 1, 1);
				levelselbut.gameObject.SetActive(true);
			}

			fadeTime += 0.2f * Time.deltaTime;
			startGame.color = Color.Lerp(startGame.color, new Color(1, 1, 1, 1), fadeTime);

			if (startGame.color.a > 0.95f)
			{
				startGame.color = new Color(1, 1, 1, 1);
				startGame.gameObject.SetActive(true);
				uiAnim -= FadeMTL;
				fadeFinish = false;
				fadeTime = MathFunctions.ResetLerpTime();
			}

		}
		#endregion
	}

	IEnumerator LoadAsync(int sceneNumber)
	{
		#region LoadingAnims
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneNumber);

		float progress = 0;

		loadingScreen.SetActive(true);
		operation.allowSceneActivation = false; 

		while (progress < 0.8f)
		{
			progress = Mathf.Clamp01(operation.progress / .9f);

			loadingBar.value = progress;
		}

		CancelInvoke("ChangeLoadingImage");

		yield return null;

		operation.allowSceneActivation = true;

		#endregion
	}

	void ChangeLoadingImage()
	{
		currentLoadingImage++;
		currentImage.sprite = loadingImages[currentLoadingImage];
		currentImage.preserveAspect = true;

		if (currentLoadingImage == 2)
		{
			currentLoadingImage = -1;
		}
	}
}
