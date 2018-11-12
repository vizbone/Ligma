using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreparationPhase : MonoBehaviour {

	public float prepTimer = 10.0f;

	public bool prepPhaseActive = false;
	public bool prepPhaseCountingDown = false;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			prepPhaseActive = true;
		}

		print(Mathf.Round(prepTimer));

		if (prepPhaseActive && !prepPhaseCountingDown)
		{
			StartCoroutine(PrepPhase());
		}
		
		if (prepPhaseCountingDown)
		{
			prepTimer -= Time.deltaTime;
		}

		prepPhaseActive = false;
	}

	private IEnumerator PrepPhase()
	{
		prepTimer = 10.0f;
		prepPhaseCountingDown = true;
		print("In Preparation Phase");
		yield return new WaitForSeconds(10.0f);
		prepPhaseCountingDown = false;
	}
}
