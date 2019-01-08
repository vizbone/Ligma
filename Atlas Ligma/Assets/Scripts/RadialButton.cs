using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RadialButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {

	public Image circle;
	public Image icon;
	public string title;
	public RadialMenu menu;

	Color defaultColor;

	public void OnPointerEnter(PointerEventData eventData)
	{
		menu.selected = this;
		defaultColor = circle.color;
		circle.color = Color.white;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		menu.selected = null;
		circle.color = Color.white;
	}

	public void OnPointerDown (PointerEventData eventData)
	{
		if (menu.selected)
		{
			if (menu.turret.faction == Faction.own)
			{
				if (menu.selected.title == "Upgrade")
					menu.turret.Upgrade ();
				else if (menu.selected.title == "Destroy")
					Destroy (menu.turret.gameObject);
				else
					print ("Invalid Upgrade Title");
				gameObject.SetActive (false);
			} else
			{
				if (menu.selected.title == "Investment 1")
					menu.turret.Invest (1);
				else if (menu.selected.title == "Investment 2")
					menu.turret.Invest (2);
				else if (menu.selected.title == "Investment 3")
					menu.turret.Invest (3);
				else
					print ("Invalid Upgrade Title");
			}
		}
	}
}