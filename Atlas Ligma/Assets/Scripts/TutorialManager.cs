using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
	[Header("Main Section")]
	public GameObject pressSpaceToStart;
	public GameObject prepStartPhase;
	public GameObject waveNumber;
	public GameObject buildUI;

	[Header("Prebuilt Tutorial Section")]
	public GameObject Investment1Part;
	public GameObject Investment2Part;

	public bool prebuiltSectionCheck;
	public bool clearToProceed; //freaking hell dude

	[SerializeField] LayerMask towerLayer;

	private void Start()
	{
		pressSpaceToStart.SetActive(false);
		prepStartPhase.SetActive(false);
		waveNumber.SetActive(false);
		buildUI.SetActive(false);

		prebuiltSectionCheck = false;
		clearToProceed = false;

		ManaSystem.inst.inTutorial = true;
	}

	public void Update()
	{
		if (Input.GetMouseButtonDown(0) && prebuiltSectionCheck)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			bool isPrebuilt = Physics.Raycast(ray, out hit, Mathf.Infinity, towerLayer, QueryTriggerInteraction.Ignore);

			if (isPrebuilt)
			{
				if (hit.collider != null)
				{
					print(hit.collider.gameObject.tag);
					clearToProceed = true;
				}
			}
		}
	}

	public void CheckToAdvanceToPrebuiltSection()
	{
		prebuiltSectionCheck = true;
	}

	public void ClickedOnPrebuilt()
	{
		if (clearToProceed)
		{
			Investment1Part.SetActive(false);
			Investment2Part.SetActive(true);
			clearToProceed = false;
		}
	}

	public void BuildUIIntro()
	{
		buildUI.SetActive(true);
	}

	public void WaveNumberIntro()
	{
		waveNumber.SetActive(true);
		prepStartPhase.SetActive(true);
	}

	public void PressSpaceToStartIntro()
	{
		pressSpaceToStart.SetActive(true);
		ManaSystem.inst.inTutorial = false;
		ManaSystem.inst.gui.EndWaveAppearance();
	}
}
