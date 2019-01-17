using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLose : MonoBehaviour
{
	public Image pener;
	public float currentPenerSize;
	public float fraction;

	public GameObject sex;
	public float currentSexSize;
	public float fraction1;

	public GameObject[] ass;
	public float currentAssSize;
	public float fraction2;

	public float stepSize;

	void Start ()
	{
		pener.color = new Color32 (0, 0, 0, 0);
		sex.transform.localScale = 0.001f * Vector3.one;
		for (int i = 0; i < ass.Length; i++) ass[i].transform.localScale = 0.001f * Vector3.one;
	}

	void Update ()
	{
		if (currentSexSize < 1) currentSexSize += 1 * stepSize;
		if (currentAssSize < 1) currentAssSize += 1 * stepSize;
		if (currentPenerSize < 1) currentPenerSize += 1 * stepSize;
		sex.transform.localScale = new Vector3 (MathFunctions.SinerpValue (currentSexSize, 1), MathFunctions.SinerpValue (currentSexSize, 1), MathFunctions.SinerpValue (currentSexSize, 1)) * fraction1;
		for (int i = 0; i < ass.Length; i++) ass[i].transform.localScale = new Vector3 (MathFunctions.SinerpValue (currentAssSize, 1), MathFunctions.SinerpValue (currentAssSize, 1), MathFunctions.SinerpValue (currentAssSize, 1)) * fraction2;
		pener.color = new Color32 (0, 0, 0, (byte) (MathFunctions.SinerpValue (currentPenerSize, 1) * 255 * fraction));
	}
}