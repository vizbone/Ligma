using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenu : MonoBehaviour
{
	public RadialButton buttonPrefab;
	public RadialButton selected;
	public List<RadialButton> buttons;
	public TurretTemplate turret;
	public float radius;

	Camera cam;

	void Start ()
	{
		cam = FindObjectOfType<Camera> ();
	}

	/// <summary>
	/// Spawns Each Button According to what is stored in the Interactable Script.
	/// Checks if the Button should be enabled or disabled when first spawned.
	/// Checks the cost for each Button Action when it first spawns.
	/// </summary>
	/// <param name="interactable"></param>
	/// <param name="isPrebuilt"></param>
	public void SpawnButtons(Interactable obj, bool isPrebuilt)
	{
		turret = obj.turret;

		buttons = new List<RadialButton>();

		for (int i = 0; i < obj.options.Length; i++)
		{
			RadialButton newButton = Instantiate(buttonPrefab) as RadialButton;
			newButton.transform.SetParent(transform, false);
			float theta = obj.options.Length == 3 ? (3 * Mathf.PI / 12) * i : (3 * Mathf.PI / 10) * i;
			float xPos = Mathf.Sin(theta) * 1.75f;
			float yPos = Mathf.Cos(theta) * 1.75f;
			newButton.transform.localPosition = new Vector3(xPos, yPos, 0f) * radius;

			newButton.icon.sprite = obj.options[i].sprite; //Set Icon of Turret
			newButton.title = obj.options[i].title; //Set Title of Button
			newButton.menu = this; 

			newButton.CheckDisabled();
			newButton.CheckCost();

			buttons.Add(newButton);
		}
	}

	void Update ()
	{
		radius = cam.orthographicSize * 0.05f;
		transform.localScale = Vector3.one * cam.orthographicSize * 0.07f;

		CheckCost();
		CheckDisabled();
	}

	public void CheckDisabled()
	{
		for (int i = 0; i < buttons.Count; i++)
		{
			buttons[i].CheckDisabled();
		}
	}

	public void CheckCost()
	{
		for (int i = 0; i < buttons.Count; i++)
		{
			buttons[i].CheckCost();
			//print("Cost Checked");
		}
	}
}