using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenuSpawner : MonoBehaviour {
	public static RadialMenuSpawner ins;
	public RadialMenu menuPrefab;

	void Awake() {
		ins = this;
	}
	
	public RadialMenu SpawnMenu(Interactable obj, TurretTemplate turret)
	{
		RadialMenu newMenu = Instantiate(menuPrefab, obj.currentTower.transform.position, this.gameObject.transform.rotation, this.gameObject.transform) as RadialMenu;
		newMenu.transform.localPosition += (transform.forward * -1) * 5;
		newMenu.sTurret = null;
		newMenu.turret = turret;
		newMenu.isSupport = true;
		newMenu.SpawnButtons(obj);
		return newMenu;
	}

	public RadialMenu SpawnMenu(Interactable obj, SupportTurret sTurret)
	{
		RadialMenu newMenu = Instantiate(menuPrefab, obj.currentTower.transform.position, this.gameObject.transform.rotation, this.gameObject.transform) as RadialMenu;
		newMenu.transform.localPosition += (transform.forward * -1) * 5;
		newMenu.sTurret = sTurret;
		newMenu.turret = null;
		newMenu.isSupport = true;
		newMenu.SpawnButtons(obj);
		return newMenu;
	}
}