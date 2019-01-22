using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : MonoBehaviour
{
	Light lightt;
	bool cLock;
	bool tick;

	void Start ()
	{
		lightt = GetComponent<Light> ();
		cLock = false;
		tick = false;
	}

	void Update ()
	{
		//if (!cLock) StartCoroutine ("toggle");
		transform.eulerAngles += transform.right * Time.deltaTime * 50f;
	}

	IEnumerator toggle ()
	{
		cLock = true;
		lightt.enabled = tick ? true : false;
		tick = tick ? false : true;
		yield return new WaitForSeconds (0.01f);
		cLock = false;
	}
}