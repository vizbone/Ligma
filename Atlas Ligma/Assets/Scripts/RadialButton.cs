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
	public bool disabled;

	Color defaultColor;

	void Start()
	{
		defaultColor = circle.color;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		menu.selected = this;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		menu.selected = null;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		menu.selected = this;

		if (menu.turret.faction == Faction.own)
		{
			if (menu.selected.title == "Upgrade") menu.turret.Upgrade();
			else if (menu.selected.title == "Destroy") Destroy(menu.turret.gameObject);
			else print("Invalid Upgrade Title");
			menu.gameObject.SetActive(false);
		}
		else
		{
			if (menu.selected.title == "Investment 1") menu.turret.Invest(1);
			else if (menu.selected.title == "Investment 2") menu.turret.Invest(2);
			else if (menu.selected.title == "Investment 3") menu.turret.Invest(3);
			else print("Invalid Upgrade Title");
		}
	}

	public void CheckDisabled()
	{
		if (menu.turret.investOrUpgradeDisabled)
		{
			if (title != "Destroy") disabled = true;
		}
		else
		{
			if (menu.turret.faction == Faction.own)
			{
				if (title == "Upgrade")
				{
					disabled = ManaSystem.inst.currentMana <= menu.turret.turretValues.upgradeOrInvestCost[0] ? true : false;
				}
			}
			else
			{
				if (title == "Investment 1")
				{
					disabled = ManaSystem.inst.currentMana <= menu.turret.turretValues.upgradeOrInvestCost[0] ? true : false;
				}
				else if (title == "Investment 2")
				{
					disabled = ManaSystem.inst.currentMana <= menu.turret.turretValues.upgradeOrInvestCost[1] ? true : false;
				}
				else if (title == "Investment 3")
				{
					disabled = ManaSystem.inst.currentMana <= menu.turret.turretValues.upgradeOrInvestCost[2] ? true : false;
				}
			}
		}

		icon.color = disabled ? Color.gray : Color.white;
		circle.color = disabled ? Color.gray : Color.white;
	}
}