using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//NEED TO ENSURE BUTTON IS UNITNERACTABLE WHEN DISPLAYING ANIMATIONS

public class GUIOverlay : MonoBehaviour
{
	[Header("Mana")]
	public Text currentManaDisplay;
	public Slider manaSlider;

	[Header("End Screen GUIs")]
	[SerializeField] Image winObj;
	[SerializeField] Image win;
	[SerializeField] RectTransform[] winButtons;
	[SerializeField] Button continueButton;

	[SerializeField] Image loseObj;
	[SerializeField] Image lose;
	[SerializeField] RectTransform[] loseButtons;

	public bool endScreenIsPlaying;

	public float[] lerpTime; //0 is for Image, 1 is for Text, subsequent is for Buttons

	[Header("Prep Phase GUI")]
	[SerializeField] Text waveNumber;
	[SerializeField] Image phaseImage;
	[SerializeField] Sprite[] phasesSprites; //0 is Prep, 1 is Battle
	[SerializeField] Text pressSpaceToStart;
	[SerializeField] float prepPhaseLerpTime;
	[SerializeField] float prepPhaseLerpSpeed; //default value is 1

	[Header("Events Notification")]
	[SerializeField] Image notification;
	[SerializeField] Text eventText;
	[SerializeField] float eventNotifLerpTime;
	[SerializeField] float eventNotificationSpeed;
	[SerializeField] bool notificationShown;

	[Header("Events Chat Box")]
	[SerializeField] RectTransform notificationChatBox;
	[SerializeField] GameObject chatLayout;
	[SerializeField] Text chatText; //Stores the Prefab
	[SerializeField] float currentChatStep;
	[SerializeField] float chatSpeed;
	[SerializeField] bool show;

	public System.Action uiAnim;

	// Start is called before the first frame update
	void Start()
    {
		SetWinLoseObj();
		endScreenIsPlaying = false;
		show = false;

		winObj.gameObject.SetActive(false);
		loseObj.gameObject.SetActive(false);

		waveNumber.text = "Wave " + (ManaSystem.inst.waveSystem.currentWave + 1) + "/" + ManaSystem.inst.waveSystem.wave.Length;
		phaseImage.sprite = phasesSprites[0];

		if (prepPhaseLerpSpeed <= 0) prepPhaseLerpSpeed = 1;
		if (eventNotificationSpeed <= 0) eventNotificationSpeed = 0.05f;

		notificationShown = false;
		manaSlider.maxValue = ManaSystem.inst.maxMana;
	}

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			uiAnim -= ShowHideChatBox;
			show = !show;
			uiAnim += ShowHideChatBox;
		}

		CheckWinLose();
		currentManaDisplay.text = ManaSystem.inst.currentMana.ToString () + "/" + ManaSystem.inst.maxMana;
		manaSlider.value = ManaSystem.inst.currentMana;
		if (uiAnim != null) uiAnim();
	}

	public void CheckWinLose()
	{
		if (ManaSystem.inst.gameState == GameStates.win && !winObj.gameObject.activeInHierarchy)
		{
			lerpTime = new float[6];
			winObj.gameObject.SetActive(true);
			endScreenIsPlaying = true;
			uiAnim += DisplayWin;
		}
		else if (ManaSystem.inst.gameState == GameStates.lose && !loseObj.gameObject.activeInHierarchy)
		{
			lerpTime = new float[4];
			loseObj.gameObject.SetActive(true);
			endScreenIsPlaying = true;
			uiAnim += DisplayLose;
		}
		else if (ManaSystem.inst.gameState == GameStates.gameComplete && !winObj.gameObject.activeInHierarchy)
		{
			lerpTime = new float[6];
			winObj.gameObject.SetActive(true);
			continueButton.interactable = false;
			endScreenIsPlaying = true;
			uiAnim += DisplayWin;
		}
	}

	void SetWinLoseObj()
	{
		winObj.color = Color.clear;
		loseObj.color = Color.clear;

		win.rectTransform.localScale = new Vector3(5, 5, 5);
		win.color = Color.white;
		lose.color = Color.clear;

		foreach (RectTransform button in winButtons)
		{
			button.anchoredPosition = new Vector2(button.anchoredPosition.x, -300);
		}
		foreach (RectTransform button in loseButtons)
		{
			button.anchoredPosition = new Vector2(button.anchoredPosition.x, -300);
		}
	}

	public void UpdateWave(int currentWave, int totalWave)
	{
		waveNumber.text = "Wave " + (currentWave + 1) + "/" + totalWave; //+1 since current wave is an array index that starts from 0
	}

	/// <summary>
	/// Set GUI to Wave Start Appearance
	/// </summary>
	public void StartWaveAppearance()
	{
		phaseImage.sprite = phasesSprites[1];
		pressSpaceToStart.gameObject.SetActive(false);
		uiAnim -= FadeInAndOut;
		prepPhaseLerpTime = 0;
	}

	/// <summary>
	/// Set GUI to Prep Phase Appearance
	/// </summary>
	public void EndWaveAppearance()
	{
		phaseImage.sprite = phasesSprites[0];
		pressSpaceToStart.gameObject.SetActive(true);
		uiAnim += FadeInAndOut;
	}

	public void CheckEventNotifications()
	{
		if (ManaSystem.inst.eventsManager.eventLineUp.Count != 0) StartCoroutine(NotificationChecks());
	}

	public GameObject AddEventChat(string intendedText)
	{
		Text chat = Instantiate(chatText, chatLayout.transform);
		chat.text = "Wave " + (ManaSystem.inst.waveSystem.currentWave + 1) + ": " + intendedText;

		return chat.gameObject;
	}

	private IEnumerator NotificationChecks()
	{
		eventText.text = ManaSystem.inst.eventsManager.eventLineUp[0];

		uiAnim += DisplayNotification;

		yield return new WaitUntil( () => notificationShown == true);

		yield return new WaitForSeconds(3);

		uiAnim += HideNotification;

		yield return new WaitUntil(() => notificationShown == false);

		ManaSystem.inst.eventsManager.eventLineUp.RemoveAt(0);

		yield return new WaitForSeconds(0.25f);

		CheckEventNotifications();
	}

	//GUI Animations
	private void FadeInAndOut()
	{
		if (pressSpaceToStart.gameObject.activeInHierarchy && ManaSystem.gameStateS == GameStates.afterWin || ManaSystem.gameStateS == GameStates.started)
		{
			prepPhaseLerpTime += Time.deltaTime;

			float alpha = MathFunctions.SmoothPingPong(prepPhaseLerpTime, 1, prepPhaseLerpSpeed);
			pressSpaceToStart.color = new Color(pressSpaceToStart.color.r, pressSpaceToStart.color.g, pressSpaceToStart.color.b, alpha);
		}
	}

	private void DisplayNotification()
	{
		eventNotifLerpTime = Mathf.Min(MathFunctions.SinerpValue(eventNotifLerpTime + eventNotificationSpeed * Time.deltaTime, 1), 1);

		//95 x is displayed, -110 x is not hidden
		float xPos = Mathf.Lerp(-125, 122.5f, eventNotifLerpTime);

		notification.rectTransform.anchoredPosition = new Vector2(xPos, notification.rectTransform.anchoredPosition.y);

		if (eventNotifLerpTime >= 0.98f)
		{
			notificationShown = true;
			eventNotifLerpTime = 0;
			uiAnim -= DisplayNotification;
		}
	}

	private void HideNotification()
	{
		eventNotifLerpTime = Mathf.Min(MathFunctions.SinerpValue(eventNotifLerpTime + eventNotificationSpeed * Time.deltaTime, 1), 1);

		//95 x is displayed, -110 x is not hidden
		float xPos = Mathf.Lerp(122.5f, -125, eventNotifLerpTime);

		notification.rectTransform.anchoredPosition = new Vector2(xPos, notification.rectTransform.anchoredPosition.y);

		if (eventNotifLerpTime >= 0.98f)
		{
			notificationShown = false;
			eventNotifLerpTime = 0;
			uiAnim -= HideNotification;
		}
	}

	void ShowHideChatBox()
	{
		if (show)
		{
			currentChatStep = Mathf.Min(MathFunctions.SinerpValue(currentChatStep + chatSpeed * Time.deltaTime, 1), 1);
		}
		else
		{
			//When it goes to 2, it curves down
			currentChatStep = Mathf.Max(MathFunctions.SinerpValue(currentChatStep + chatSpeed * Time.deltaTime, 2), 0);
		}

		float xPos = Mathf.Lerp(520, 290, currentChatStep);

		notificationChatBox.anchoredPosition = new Vector2(xPos, notificationChatBox.anchoredPosition.y);

		if (show)
		{
			if (currentChatStep > 0.98f)
			{
				notificationChatBox.anchoredPosition = new Vector2(290, notificationChatBox.anchoredPosition.y);
				uiAnim -= ShowHideChatBox;
			}
		}
		else
		{
			if (currentChatStep < 0.02f)
			{
				notificationChatBox.anchoredPosition = new Vector2(520, notificationChatBox.anchoredPosition.y);
				uiAnim -= ShowHideChatBox;
			}
		}
	}

	public void DisplayWin()
	{
		lerpTime[0] = Mathf.Min(lerpTime[0] + 1.5f * Time.fixedDeltaTime, 1);
		lerpTime[1] = Mathf.Min(lerpTime[1] + 0.75f * Time.fixedDeltaTime, 1);

		float imageLerpTime = MathFunctions.SinerpValue(lerpTime[0], 1);
		float textLerpTime = MathFunctions.SinerpValue(lerpTime[1], 1);

		winObj.color = Color.Lerp(winObj.color, new Color(0, 0, 0, 0.5f), imageLerpTime);
		win.rectTransform.localScale = Vector3.Lerp(win.rectTransform.localScale, new Vector3(1, 1, 1), textLerpTime);

		if (win.rectTransform.localScale.x < 1.05f)
		{
			win.rectTransform.localScale = new Vector3(1, 1, 1);
			uiAnim += DisplayWinButtons;
			uiAnim -= DisplayWin;
		}
	}

	public void DisplayWinButtons()
	{
		float firstButtonTime = 0;
		float secondButtonTime = 0;
		float thirdButtonTime = 0;
		float fourthButtonTime = 0;

		lerpTime[2] = Mathf.Min(lerpTime[2] + 1.25f * Time.fixedDeltaTime, 1);
		firstButtonTime = MathFunctions.SinerpValue(lerpTime[2], 1);

		if (firstButtonTime > 0.8f)
		{
			lerpTime[3] = Mathf.Min(lerpTime[3] + 1.25f * Time.fixedDeltaTime, 1);
			secondButtonTime = MathFunctions.SinerpValue(lerpTime[3], 1);
		}

		if (secondButtonTime > 0.8f)
		{
			lerpTime[4] = Mathf.Min(lerpTime[4] + 1.25f * Time.fixedDeltaTime, 1);
			thirdButtonTime = MathFunctions.SinerpValue(lerpTime[4], 1);
		}

		if (thirdButtonTime > 0.8f)
		{
			lerpTime[5] = Mathf.Min(lerpTime[5] + 1.25f * Time.fixedDeltaTime, 1);
			fourthButtonTime = MathFunctions.SinerpValue(lerpTime[5], 1);
		}

		winButtons[0].anchoredPosition = Vector2.Lerp(winButtons[0].anchoredPosition, new Vector3(winButtons[0].anchoredPosition.x, -125), firstButtonTime);
		winButtons[1].anchoredPosition = Vector2.Lerp(winButtons[1].anchoredPosition, new Vector3(winButtons[1].anchoredPosition.x, -125), secondButtonTime);
		winButtons[2].anchoredPosition = Vector2.Lerp(winButtons[2].anchoredPosition, new Vector3(winButtons[2].anchoredPosition.x, -125), thirdButtonTime);
		winButtons[3].anchoredPosition = Vector2.Lerp(winButtons[3].anchoredPosition, new Vector3(winButtons[3].anchoredPosition.x, -125), fourthButtonTime);

		if (fourthButtonTime > 0.98f)
		{
			winButtons[0].anchoredPosition = new Vector2(winButtons[0].anchoredPosition.x, -125);
			winButtons[1].anchoredPosition = new Vector2(winButtons[1].anchoredPosition.x, -125);
			winButtons[2].anchoredPosition = new Vector2(winButtons[2].anchoredPosition.x, -125);
			winButtons[3].anchoredPosition = new Vector2(winButtons[3].anchoredPosition.x, -125);
			uiAnim -= DisplayWinButtons;

			endScreenIsPlaying = false;

			lerpTime = null;
		}
	}

	public void DisplayLose()
	{
		lerpTime[0] = Mathf.Min(lerpTime[0] + 1.5f * Time.fixedDeltaTime, 1);
		lerpTime[1] = Mathf.Min(lerpTime[1] + 0.1f * Time.fixedDeltaTime, 1);

		float imageLerpTime = MathFunctions.SinerpValue(lerpTime[0], 1);
		float textLerpTime = MathFunctions.SinerpValue(lerpTime[1], 1);

		loseObj.color = Color.Lerp(winObj.color, new Color(0, 0, 0, 0.5f), imageLerpTime);
		lose.color = Color.Lerp(lose.color, Color.white, textLerpTime);

		if (lose.color.a > 0.95f)
		{
			lose.color = Color.white;
			uiAnim += DisplayLoseButtons;
			uiAnim -= DisplayLose;
		}
	}

	public void DisplayLoseButtons()
	{
		float firstButtonTime = 0;
		float secondButtonTime = 0;

		lerpTime[2] = Mathf.Min(lerpTime[2] + 1.25f * Time.fixedDeltaTime, 1);
		firstButtonTime = MathFunctions.SinerpValue(lerpTime[2], 1);

		if (firstButtonTime > 0.8f)
		{
			lerpTime[3] = Mathf.Min(lerpTime[3] + 1.25f * Time.fixedDeltaTime, 1);
			secondButtonTime = MathFunctions.SinerpValue(lerpTime[3], 1);
		}

		loseButtons[0].anchoredPosition = Vector2.Lerp(loseButtons[0].anchoredPosition, new Vector3(loseButtons[0].anchoredPosition.x, -125), firstButtonTime);
		loseButtons[1].anchoredPosition = Vector2.Lerp(loseButtons[1].anchoredPosition, new Vector3(loseButtons[1].anchoredPosition.x, -125), secondButtonTime);

		if (secondButtonTime > 0.98f)
		{
			loseButtons[0].anchoredPosition = new Vector2(loseButtons[0].anchoredPosition.x, -125);
			loseButtons[1].anchoredPosition = new Vector2(loseButtons[1].anchoredPosition.x, -125);
			uiAnim -= DisplayLoseButtons;

			endScreenIsPlaying = false;

			lerpTime = null;
		}
	}

	public void HideEndScreen()
	{
		float buttonLerpTime = 0;
		float imageTextLerpTime = 0;

		lerpTime[0] = Mathf.Min(lerpTime[0] + Time.fixedDeltaTime, 1);
		imageTextLerpTime = MathFunctions.SinerpValue(lerpTime[0], 1);

		lerpTime[1] = Mathf.Min(lerpTime[0] + 2 * Time.fixedDeltaTime, 1);
		buttonLerpTime = MathFunctions.SinerpValue(lerpTime[1], 1);

		winObj.color = Color.Lerp(winObj.color, Color.clear, imageTextLerpTime);
		win.color = Color.Lerp(win.color, Color.clear, imageTextLerpTime);

		foreach (RectTransform button in winButtons)
		{
			button.anchoredPosition = Vector2.Lerp(button.anchoredPosition, new Vector2(button.anchoredPosition.x, -300), buttonLerpTime);
		}

		if (imageTextLerpTime > 0.98f)
		{
			winObj.color = Color.clear;
			win.color = Color.clear;
			foreach (RectTransform button in winButtons)
			{
				button.anchoredPosition = new Vector2(button.anchoredPosition.x, -300);
			}

			uiAnim -= HideEndScreen;
			winObj.gameObject.SetActive(false);
			SetWinLoseObj();

			endScreenIsPlaying = false;

			lerpTime = null;
			ManaSystem.gameStateS = GameStates.afterWin;
		}
	}
}
