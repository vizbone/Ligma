using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ManaGlowEffect : MonoBehaviour
{
	public Image manaBlueGlowBG;
	public Image manaRedGlowBG;

	public GameObject blueGlow;
	public GameObject redGlow;

	public float currentBlueTime;
	public float currentRedTime;

	AudioSource siren;

	private void Start()
	{
		blueGlow.SetActive(false);
		redGlow.SetActive(false);
		siren = GetComponent<AudioSource>();
	}

	void Update()
    {
		if (ManaSystem.inst.currentMana >= ManaSystem.inst.maxMana - 200 && ManaSystem.inst.currentMana < ManaSystem.inst.maxMana)
		{
			blueGlow.SetActive(true);
			GlowBluePingPong();
		} else blueGlow.SetActive(false);
		

		if (ManaSystem.inst.currentMana <= 200 && ManaSystem.inst.currentMana >= 0)
		{
			redGlow.SetActive(true);
			GlowRedPingPong();
			ManaSystem.inst.audioLibrary.PlayAudio(ManaSystem.inst.audioLibrary.siren, siren, false, false);
		} else redGlow.SetActive(false);
	}

	void GlowBluePingPong()
	{
		currentBlueTime += Time.deltaTime;
		float alpha = MathFunctions.SmoothPingPong(currentBlueTime, 1.0f, 1.0f);

		manaBlueGlowBG.color = new Color (manaBlueGlowBG.color.r, manaBlueGlowBG.color.g, manaBlueGlowBG.color.b, alpha);
	}

	void GlowRedPingPong()
	{
		currentRedTime += Time.deltaTime;
		float alpha = MathFunctions.SmoothPingPong(currentRedTime, 1.0f, 1.0f);

		manaRedGlowBG.color = new Color (manaRedGlowBG.color.r, manaRedGlowBG.color.g, manaRedGlowBG.color.b, alpha);
	}
}
