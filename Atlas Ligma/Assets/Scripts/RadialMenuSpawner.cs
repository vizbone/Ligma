using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenuSpawner : MonoBehaviour {
	public static RadialMenuSpawner ins;
	public RadialMenu menuPrefab;

	void Awake() {
		ins = this;
	}
	
	public void SpawnMenu(Interactable obj, TurretTemplate turret)
	{
		print("Spawning");
		RadialMenu newMenu = Instantiate(menuPrefab) as RadialMenu;
		newMenu.turret = turret;
		newMenu.sTurret = null;
		newMenu.isSupport = false;
		newMenu.transform.SetParent(transform, false);
		newMenu.transform.position = Camera.main.WorldToScreenPoint(obj.currentTower.transform.position);
		newMenu.SpawnButtons(obj);
	}

	public void SpawnMenu(Interactable obj, SupportTurret sTurret)
	{
		print("Spawning");
		RadialMenu newMenu = Instantiate(menuPrefab) as RadialMenu;
		newMenu.sTurret = sTurret;
		newMenu.turret = null;
		newMenu.isSupport = true;
		newMenu.transform.SetParent(transform, false);
		newMenu.transform.position = Camera.main.WorldToScreenPoint(obj.currentTower.transform.position);
		newMenu.SpawnButtons(obj);
	}
}