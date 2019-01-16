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

	public void SpawnButtons(Interactable obj)
	{
		buttons = new List<RadialButton>();

		for (int i = 0; i < obj.options.Length; i++)
		{
			RadialButton newButton = Instantiate(buttonPrefab) as RadialButton;
			newButton.transform.SetParent(transform, false);
			float theta = (2 * Mathf.PI / obj.options.Length) * i;
			float xPos = Mathf.Sin(theta) * 1.5f;
			float yPos = Mathf.Cos(theta) * 1.5f;
			newButton.transform.localPosition = new Vector3(xPos, yPos, 0f) * radius;
			//newButton.circle.color = obj.options[i].color;
			newButton.icon.sprite = obj.options[i].sprite;
			newButton.title = obj.options[i].title;
			newButton.menu = this;
			newButton.CheckDisabled();

			buttons.Add(newButton);
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