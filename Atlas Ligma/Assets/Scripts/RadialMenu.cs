using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenu : MonoBehaviour
{
	public RadialButton buttonPrefab;
	public RadialButton selected;
	public TurretTemplate turret;
	public int radius;

	public void SpawnButtons(Interactable obj)
	{
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
		}
	}

	/*void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (selected)
			{
				if (turret.faction == Faction.own)
				{
					if (selected.title == "Upgrade") turret.Upgrade();
					else if (selected.title == "Destroy") Destroy(turret.gameObject);
					else print("Invalid Upgrade Title");
					gameObject.SetActive(false);
				}
				else
				{
					if (selected.title == "Investment 1") turret.Invest(1);
					else if (selected.title == "Investment 2") turret.Invest(2);
					else if (selected.title == "Investment 3") turret.Invest(3);
					else print("Invalid Upgrade Title");
				}
			}
			else print("No Button Selected");
		}
	}*/
}