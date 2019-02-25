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
	public AudioSource soundA;
	public GameObject nextButton;
	public GameObject buttonActive;

	public bool prebuiltSectionCheck;
	public bool clearToProceed; //freaking hell dude
	public bool tutorialCameraLock;
	public Transform arrowButton;
	float currentTime;
	float oriY;

	public static TutorialManager inst;

	[SerializeField] LayerMask towerLayer;

	private void Start()
	{
		oriY = arrowButton.transform.position.y;
		currentTime = 0;
		inst = this;
		pressSpaceToStart.SetActive(false);
		prepStartPhase.SetActive(false);
		waveNumber.SetActive(false);
		buildUI.SetActive(false);

		prebuiltSectionCheck = false;
		clearToProceed = false;
		tutorialCameraLock = true;

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
					nextButton.SetActive(true);
					buttonActive.SetActive(true);
					arrowButton.gameObject.SetActive(false);
					clearToProceed = true;
				}
			}
		}

		ArrowBounce();
	}

	public void CheckToAdvanceToPrebuiltSection()
	{
		prebuiltSectionCheck = true;
	}

	public void ClickedOnPrebuilt()
	{
		if (clearToProceed)
		{
			soundA.Play();
			Investment1Part.SetActive(false);
			Investment2Part.SetActive(true);
			clearToProceed = false;
		}
	}

	public void TutorialCameraUnlock()
	{
		tutorialCameraLock = false;
	}

	public void BuildUIIntro()
	{
		buildUI.SetActive(true);
		ManaSystem.inst.gridSystem.buildLock = false;
	}

	public void WaveNumberIntro()
	{
		waveNumber.SetActive(true);
		prepStartPhase.SetActive(true);
	}

	public void PressSpaceToStartIntro()
	{
		pressSpaceToStart.SetActive(true);
		clearToProceed = false;
		prebuiltSectionCheck = false;
		ManaSystem.inst.inTutorial = false;
		ManaSystem.inst.gui.EndWaveAppearance();
	}

	public void ArrowBounce()
	{
		currentTime += Time.deltaTime;
		float lerpMovement = MathFunctions.SmoothPingPong(currentTime, 10.0f, 2.0f);

		arrowButton.transform.position = new Vector3(arrowButton.transform.position.x, oriY + lerpMovement, arrowButton.transform.position.z);
	}
}
