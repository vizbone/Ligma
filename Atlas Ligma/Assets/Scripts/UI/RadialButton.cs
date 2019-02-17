﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RadialButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {

	public Image icon;
	public string title;
	public Text cost;
	public RadialMenu menu;
	public bool disabled;

	Color defaultColor;

	void Start()
	{
		defaultColor = Color.white;
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
		if (ManaSystem.gameStateS == GameStates.started || ManaSystem.gameStateS == GameStates.afterWin)
		{
			menu.selected = this;

			if (menu.turret.faction == Faction.own)
			{
				if (menu.selected.title == "Upgrade" && !disabled) menu.turret.Upgrade ();
				else if (menu.selected.title == "Destroy")
				{
					AudioSource audio = Instantiate (ManaSystem.inst.audio, menu.turret.transform.position, Quaternion.identity);
					ManaSystem.inst.audioLibrary.PlayAudio (ManaSystem.inst.audioLibrary.destroy, audio);
					Destroy (menu.gameObject);
					Destroy (menu.turret.gameObject);
				} 
				else print ("Invalid Upgrade Title");
			}
			else
			{
				if (menu.selected.title == "Investment 1" && !disabled)
				{
					menu.turret.Invest(1);
					CheckCost();
				}
				else if (menu.selected.title == "Investment 2" && !disabled)
				{
					menu.turret.Invest(2);
					CheckCost();
				}
				else if (menu.selected.title == "Investment 3" && !disabled)
				{
					menu.turret.Invest(3);
					CheckCost();
				}
				else print("Invalid Upgrade Title");
			}
		}
	}

	public void CheckCost()
	{
		if (menu.turret.faction == Faction.own)
		{
			if (title == "Upgrade" && menu.turret.level < 3) cost.text = menu.turret.turretValues.upgradeOrInvestCost[0].ToString();
			else cost.text = "";
		}
		else
		{
			if (title == "Investment 1")
			{
				if (menu.turret.investmentLevel == 0) cost.text = menu.turret.turretValues.upgradeOrInvestCost[0].ToString();
				else cost.text = "0";
			}
			else if (title == "Investment 2")
			{
				if (menu.turret.investmentLevel != 0)
				{
					cost.text = (Mathf.Max(menu.turret.turretValues.upgradeOrInvestCost[1] - menu.turret.turretValues.upgradeOrInvestCost[menu.turret.investmentLevel - 1], 0)).ToString();
				}
				else cost.text = menu.turret.turretValues.upgradeOrInvestCost[1].ToString();
			}
			else if (title == "Investment 3")
			{
				if (menu.turret.investmentLevel != 0)
				{
					cost.text = (menu.turret.turretValues.upgradeOrInvestCost[2] - menu.turret.turretValues.upgradeOrInvestCost[menu.turret.investmentLevel - 1]).ToString();
				}
				else cost.text = menu.turret.turretValues.upgradeOrInvestCost[2].ToString();
			} 
			else print("Invalid Button");
		}
	}

	public void CheckDisabled()
	{
		if (!menu.turret.isActiveAndEnabled)
		{
			disabled = true;
			icon.color = disabled ? Color.gray : Color.white;
			return;
		}

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
					disabled = ManaSystem.inst.currentMana <= menu.turret.turretValues.upgradeOrInvestCost[0] || menu.turret.investmentLevel != 0 ? true : false;
				}
				else if (title == "Investment 2")
				{
					disabled = ManaSystem.inst.currentMana <= menu.turret.turretValues.upgradeOrInvestCost[1] || menu.turret.investmentLevel >= 2 ? true : false;
				}
				else if (title == "Investment 3")
				{
					disabled = ManaSystem.inst.currentMana <= menu.turret.turretValues.upgradeOrInvestCost[2] || menu.turret.investmentLevel >= 3 ? true : false;
				}
			}
		}

		icon.color = disabled ? Color.gray : Color.white;
		//circle.color = disabled ? Color.gray : Color.white;
	}
}