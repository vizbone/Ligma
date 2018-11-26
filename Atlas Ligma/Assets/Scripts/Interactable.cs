using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
	[System.Serializable]
	public class Action
	{
		public Color color;
		public Sprite sprite;
		public string title;
	}

	public Action[] options;
	public GameObject currentTower;
	public TurretTemplate turret;
	public SupportTurret sTurret;
	MeshCollider sensor;
	[SerializeField] LayerMask towerLayer;

	private void Start()
	{
		towerLayer.value = 1 << 10;
		currentTower = gameObject;
		sensor = GetComponent<MeshCollider>();
		turret = gameObject.GetComponent<TurretTemplate>() ? gameObject.GetComponent<TurretTemplate>() : null;
		sTurret = gameObject.GetComponent<SupportTurret>() ? gameObject.GetComponent<SupportTurret>() : null;
	}

	private void Update()
	{
		
	}

	void OnMouseDown()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		//QueryTriggerInteraction Ignore to prevent clicking on collider that makes up the turrets range.
		bool hasTower = Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, towerLayer, QueryTriggerInteraction.Ignore);

		if (hasTower)
		{
			if (hit.collider != null)
			{
				print(hit.collider.name);
				if (turret != null) RadialMenuSpawner.ins.SpawnMenu(this, turret);
				else RadialMenuSpawner.ins.SpawnMenu(this, sTurret);
			}
		}
		else return;
	}
}