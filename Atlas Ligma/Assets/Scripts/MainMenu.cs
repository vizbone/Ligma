using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public Slider loadingBar;
	public GameObject loadingScreen;
	public Image currentImage;
	public Sprite[] loadingImages;
	public int currentLoadingImage;

	public void Start()
	{
		currentImage.sprite = loadingImages[0];
	}

	public void LoadScreen(int sceneNumber)
	{
		StartCoroutine(LoadAsync(sceneNumber));
		InvokeRepeating("ChangeLoadingImage", 0f, 0.5f);
	}

	IEnumerator LoadAsync(int sceneNumber)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneNumber);

		float progress = 0;

		loadingScreen.SetActive(true);
		print("senzawa");
		operation.allowSceneActivation = false; 

		while (progress < 0.8f)
		{
			progress = Mathf.Clamp01(operation.progress / .9f);

			loadingBar.value = progress;
		}

		CancelInvoke("ChangeLoadingImage");

		yield return null;

		//operation.allowSceneActivation = true;
	}

	void ChangeLoadingImage()
	{
		currentLoadingImage++;
		currentImage.sprite = loadingImages[currentLoadingImage];
		currentImage.preserveAspect = true;

		print("hi");

		if (currentLoadingImage == 2)
		{
			currentLoadingImage = -1;
		}
	}
}
