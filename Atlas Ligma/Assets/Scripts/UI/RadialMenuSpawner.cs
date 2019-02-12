using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenuSpawner : MonoBehaviour {

	public static RadialMenuSpawner ins;

	[Header("Prefabs For Spawning")]
	public RadialMenu menuPrefab;
	public TurretInfo turretInfoPrefab;

	void Awake()
	{
		ins = this;
	}
	
	public RadialMenu SpawnMenu (Interactable obj, TurretTemplate turret)
	{
		RadialMenu newMenu = Instantiate(menuPrefab, turret.transform.position + Vector3.right * 0.5f + Vector3.down * 0.3f, this.gameObject.transform.rotation, this.gameObject.transform);
		newMenu.transform.localPosition += (transform.forward * -1) * 15;
		newMenu.turret = turret;
		newMenu.SpawnButtons(obj, turret.isPrebuilt);
		return newMenu;
	}
}