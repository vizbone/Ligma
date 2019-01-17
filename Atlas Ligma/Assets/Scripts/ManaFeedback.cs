using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ManaFeedback : MonoBehaviour
{
	[SerializeField] RectTransform manaDropHolder;
	public Text manaDrop;
	public Image icon;
	public Vector3 targetPos;
	public Color targetColor;

	Action anim;
	[SerializeField] float lerpTime;

    // Start is called before the first frame update
    void Start()
    {
		targetPos = transform.position + transform.up; 
		//manaDropHolder Assigned via Inspector
		anim += Appear;
    }

    // Update is called once per frame
    void Update()
    {
		if (ManaSystem.gameStateS == GameStates.started || ManaSystem.gameStateS == GameStates.afterWin)
		{
			if (anim != null) anim();
		}
    }

	public void Appear()
	{
		lerpTime += Time.deltaTime * .5f;
		float time = MathFunctions.SinerpValue(lerpTime, 1);

		manaDropHolder.position = Vector3.Lerp(manaDropHolder.position, targetPos, time);
		manaDrop.color = Color.Lerp(manaDrop.color, targetColor, time);
		icon.color = Color.Lerp(icon.color, Color.white, time);

		if (Mathf.Abs(manaDropHolder.position.y - targetPos.y) < 0.05f)
		{
			manaDropHolder.transform.position = targetPos;
			manaDrop.color = targetColor;
			icon.color = Color.white;

			StartCoroutine(Freeze());
			anim -= Appear;
		}
	}

	public void Disappear()
	{
		lerpTime += Time.deltaTime * .5f;
		float time = MathFunctions.SinerpValue(lerpTime, 1);

		manaDropHolder.position = Vector3.Lerp(manaDropHolder.position, targetPos, time);
		manaDrop.color = Color.Lerp(manaDrop.color, Color.clear, time);
		icon.color = Color.Lerp(icon.color, Color.clear, time);

		if (Mathf.Abs(manaDropHolder.position.y - targetPos.y) < 0.05f)
		{
			Destroy(gameObject);
		}
	}

	IEnumerator Freeze()
	{
		yield return new WaitForSeconds(1.5f);
		lerpTime = 0;
		targetPos = transform.position + transform.up;
		anim += Disappear;
	}
}
