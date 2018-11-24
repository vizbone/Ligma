using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
	[System.Serializable]
	public class Action {
		public Color color;
		public Sprite sprite;
		public string title;
	}

	public Action[] options;
	public GameObject currentTower;
	public TurretTemplate turret;
	public SupportTurret sTurret;
	MeshCollider sensor;

	private void Start()
	{
		currentTower = gameObject;
		sensor = GetComponent<MeshCollider>();
		turret = gameObject.GetComponent<TurretTemplate>() ? gameObject.GetComponent<TurretTemplate>() : null;
		sTurret = gameObject.GetComponent<SupportTurret>() ? gameObject.GetComponent<SupportTurret>() : null;
	}

	void OnMouseDown()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (sensor.Raycast(ray, out hit, 100.0f))
		{
			if (turret != null) RadialMenuSpawner.ins.SpawnMenu(this, turret);
			else RadialMenuSpawner.ins.SpawnMenu(this, sTurret);
		}
	}
}