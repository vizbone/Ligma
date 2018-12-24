using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RadialButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

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
}