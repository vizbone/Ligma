using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIOverlay : MonoBehaviour
{
	[Header("Screen GUIs")]
	public Image winObj;
	public Image win;
	public RectTransform[] winButtons;
	public Button continueButton;

	public Image loseObj;
	public Image lose;
	public RectTransform[] loseButtons;

	public float[] lerpTime; //0 is for Image, 1 is for Text, subsequent is for Buttons

	public System.Action uiAnim;

	// Start is called before the first frame update
	void Start()
    {
		SetWinLoseObj();

		winObj.gameObject.SetActive(false);
		loseObj.gameObject.SetActive(false);
	}

    // Update is called once per frame
    void Update()
    {
		CheckWinLose();
		if (uiAnim != null) uiAnim();
	}

	public void CheckWinLose()
	{
		if (ManaSystem.inst.gameState == GameStates.win && !winObj.gameObject.activeInHierarchy)
		{
			lerpTime = new float[6];
			winObj.gameObject.SetActive(true);
			uiAnim += DisplayWin;
		}
		else if (ManaSystem.inst.gameState == GameStates.lose && !loseObj.gameObject.activeInHierarchy)
		{
			lerpTime = new float[4];
			loseObj.gameObject.SetActive(true);
			uiAnim += DisplayLose;
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

	//GUI Animations
	public void DisplayWin()
	{
		lerpTime[0] = Mathf.Min(lerpTime[0] + 1.5f * Time.deltaTime, 1);
		lerpTime[1] = Mathf.Min(lerpTime[1] + 0.75f * Time.deltaTime, 1);

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

		lerpTime[2] = Mathf.Min(lerpTime[2] + 1.25f * Time.deltaTime, 1);
		firstButtonTime = MathFunctions.SinerpValue(lerpTime[2], 1);

		if (firstButtonTime > 0.8f)
		{
			lerpTime[3] = Mathf.Min(lerpTime[3] + 1.25f * Time.deltaTime, 1);
			secondButtonTime = MathFunctions.SinerpValue(lerpTime[3], 1);
		}

		if (secondButtonTime > 0.8f)
		{
			lerpTime[4] = Mathf.Min(lerpTime[4] + 1.25f * Time.deltaTime, 1);
			thirdButtonTime = MathFunctions.SinerpValue(lerpTime[4], 1);
		}

		if (thirdButtonTime > 0.8f)
		{
			lerpTime[5] = Mathf.Min(lerpTime[5] + 1.25f * Time.deltaTime, 1);
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

			lerpTime = null;
		}
	}

	public void DisplayLose()
	{
		lerpTime[0] = Mathf.Min(lerpTime[0] + 1.5f * Time.deltaTime, 1);
		lerpTime[1] = Mathf.Min(lerpTime[1] + 0.1f * Time.deltaTime, 1);

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

		lerpTime[2] = Mathf.Min(lerpTime[2] + 1.25f * Time.deltaTime, 1);
		firstButtonTime = MathFunctions.SinerpValue(lerpTime[2], 1);

		if (firstButtonTime > 0.8f)
		{
			lerpTime[3] = Mathf.Min(lerpTime[3] + 1.25f * Time.deltaTime, 1);
			secondButtonTime = MathFunctions.SinerpValue(lerpTime[3], 1);
		}

		loseButtons[0].anchoredPosition = Vector2.Lerp(loseButtons[0].anchoredPosition, new Vector3(loseButtons[0].anchoredPosition.x, -125), firstButtonTime);
		loseButtons[1].anchoredPosition = Vector2.Lerp(loseButtons[1].anchoredPosition, new Vector3(loseButtons[1].anchoredPosition.x, -125), secondButtonTime);

		if (secondButtonTime > 0.98f)
		{
			loseButtons[0].anchoredPosition = new Vector2(loseButtons[0].anchoredPosition.x, -125);
			loseButtons[1].anchoredPosition = new Vector2(loseButtons[1].anchoredPosition.x, -125);
			uiAnim -= DisplayLoseButtons;

			lerpTime = null;
		}
	}

	public void HideEndScreen()
	{
		float buttonLerpTime = 0;
		float imageTextLerpTime = 0;

		lerpTime[0] = Mathf.Min(lerpTime[0] + Time.deltaTime, 1);
		imageTextLerpTime = MathFunctions.SinerpValue(lerpTime[0], 1);

		lerpTime[1] = Mathf.Min(lerpTime[0] + 2 * Time.deltaTime, 1);
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

			lerpTime = null;
			ManaSystem.gameStateS = GameStates.afterWin;
		}
	}
}
