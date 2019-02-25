using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiAirTutorial : MonoBehaviour
{
	public GameObject pressSpaceToStart;
	public GameObject prepStartPhase;

	public GameObject nextButton;

	void Start()
    {
		pressSpaceToStart.SetActive(false);
		prepStartPhase.SetActive(false);

		ManaSystem.inst.inTutorial = true;
	}

	public void PressSpaceToStartIntro()
	{
		nextButton.SetActive(false);

		pressSpaceToStart.SetActive(true);
		prepStartPhase.SetActive(true);
		ManaSystem.inst.inTutorial = false;
		ManaSystem.inst.gui.EndWaveAppearance();
	}
}
