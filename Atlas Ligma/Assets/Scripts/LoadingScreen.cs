using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
	public static LoadingScreen loadingScreen;

	[Header("Image Components")]
	public Image bg;
	public Slider loadingBar;
	public Image currentImage;
	public Sprite[] loadingImages;
	public int currentLoadingImage;

	[SerializeField] AsyncOperation operation;

	private void Awake()
	{
		//Set a Singularity
		if (loadingScreen == null)
		{
			loadingScreen = this;
			DontDestroyOnLoad(loadingScreen);
		}
		else
		{
			Destroy(this);
		}
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (operation != null)
		{
			if (operation.isDone)
			{
				bg.gameObject.SetActive(false);
				CancelInvoke("ChangeLoadingImage");
				currentImage.sprite = loadingImages[0];
				loadingBar.value = 0;
				operation = null;
			}
			else
			{
				if (bg.gameObject.activeInHierarchy)
				{
					loadingBar.value = operation.progress;
				}
			}
		}
    }

	public static void LoadSceneStatic(int sceneNumber)
	{
		loadingScreen.LoadScene(sceneNumber);
	}

	public static void LoadSceneStatic(string sceneName)
	{
		loadingScreen.LoadScene(sceneName);
	}

	public void LoadScene(int sceneNumber)
	{
		bg.gameObject.SetActive(true);

		InvokeRepeating("ChangeLoadingImage", 0, 0.5f);

		operation = SceneManager.LoadSceneAsync(sceneNumber, LoadSceneMode.Single);
	}

	public void LoadScene(string sceneName)
	{
		bg.gameObject.SetActive(true);

		InvokeRepeating("ChangeLoadingImage", 0, 0.5f);

		operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
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
