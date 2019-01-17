using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenu : MonoBehaviour
{
	public RadialButton buttonPrefab;
	public RadialButton selected;
	public List<RadialButton> buttons;
	public TurretTemplate turret;
	public int radius;

	public void SpawnButtons(Interactable2 obj, bool isPrebuilt)
	{
		buttons = new List<RadialButton>();

		if (isPrebuilt)
		{
			for (int i = 0; i < obj.prebuiltOptions.Length; i++)
			{
				RadialButton newButton = Instantiate (buttonPrefab) as RadialButton;
				newButton.transform.SetParent (transform, false);
				float theta = obj.prebuiltOptions.Length == 3 ? (3 * Mathf.PI / 12) * i : (3 * Mathf.PI / 10) * i;
				float xPos = Mathf.Sin (theta) * 1.75f;
				float yPos = Mathf.Cos (theta) * 1.75f;
				newButton.transform.localPosition = new Vector3 (xPos, yPos, 0f) * radius;
				//newButton.circle.color = obj.options[i].color;
				newButton.icon.sprite = obj.prebuiltOptions[i].sprite;
				newButton.title = obj.prebuiltOptions[i].title;
				newButton.menu = this;
				newButton.CheckDisabled ();

				buttons.Add (newButton);
			}
		} else
		{
			for (int i = 0; i < obj.ownOptions.Length; i++)
			{
				RadialButton newButton = Instantiate (buttonPrefab) as RadialButton;
				newButton.transform.SetParent (transform, false);
				float theta = obj.ownOptions.Length == 3 ? (3 * Mathf.PI / 12) * i : (3 * Mathf.PI / 10) * i;
				float xPos = Mathf.Sin (theta) * 1.75f;
				float yPos = Mathf.Cos (theta) * 1.75f;
				newButton.transform.localPosition = new Vector3 (xPos, yPos, 0f) * radius;
				//newButton.circle.color = obj.options[i].color;
				newButton.icon.sprite = obj.ownOptions[i].sprite;
				newButton.title = obj.ownOptions[i].title;
				newButton.menu = this;
				newButton.CheckDisabled ();

				buttons.Add (newButton);
			}
		}
	}

	public void CheckDisabled()
	{
		for (int i = 0; i < buttons.Count; i++)
		{
			buttons[i].CheckDisabled();
		}
	}
}