using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenuSpawner : MonoBehaviour {
	public static RadialMenuSpawner ins;
	public RadialMenu menuPrefab;

	void Awake() {
		ins = this;
	}
	
	public void SpawnMenu(Interactable obj) {
		RadialMenu newMenu = Instantiate(menuPrefab) as RadialMenu;
		newMenu.transform.SetParent(transform, false);
		newMenu.transform.position = Camera.main.WorldToScreenPoint(obj.currentTower.transform.position);
		newMenu.SpawnButtons(obj);
	}
}