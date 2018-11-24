﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenu : MonoBehaviour
{
	public RadialButton buttonPrefab;
	public RadialButton selected;
	public bool isSupport;
	public TurretTemplate turret;
	public SupportTurret sTurret;
	public int radius;

	public void SpawnButtons(Interactable obj)
	{
		for (int i = 0; i < obj.options.Length; i++) {
			RadialButton newButton = Instantiate(buttonPrefab) as RadialButton;
			newButton.transform.SetParent(transform, false);
			float theta = (2 * Mathf.PI / obj.options.Length) * i;
			float xPos = Mathf.Sin(theta);
			float yPos = Mathf.Cos(theta);
			newButton.transform.localPosition = new Vector3(xPos, yPos, 0f) * radius;
			newButton.circle.color = obj.options[i].color;
			newButton.icon.sprite = obj.options[i].sprite;
			newButton.title = obj.options[i].title;
			newButton.menu = this;
		}
	}

	void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			if (selected)
			{
				if (isSupport) sTurret.Upgrade(sTurret.sTurretValues.boostsStats);
				else turret.Upgrade();
			}
			Destroy(gameObject);
		}
	}
}