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
	[SerializeField] RadialMenu menuInst;

	private void Start()
	{
		towerLayer.value = 1 << 10;
		currentTower = gameObject;
		sensor = GetComponent<MeshCollider>();
		turret = gameObject.GetComponent<TurretTemplate>() ? gameObject.GetComponent<TurretTemplate>() : null;
		sTurret = gameObject.GetComponent<SupportTurret>() ? gameObject.GetComponent<SupportTurret>() : null;
	}


	void Update()
	{
		if (Input.GetMouseButtonDown (0))
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			//QueryTriggerInteraction Ignore to prevent clicking on collider that makes up the turrets range.
			bool hasTower = sensor.Raycast (ray, out hit, Mathf.Infinity);

			if (hasTower)
			{
				if (hit.collider != null)
				{
					if (menuInst == null)
					{
						if (turret != null)
							menuInst = RadialMenuSpawner.ins.SpawnMenu(this, turret);
						else
							menuInst = RadialMenuSpawner.ins.SpawnMenu(this, sTurret);
					}
					else
					{
						if (!menuInst.gameObject.activeInHierarchy) menuInst.gameObject.SetActive(true);
					}
				}
			}
			else
			{
				if (menuInst != null && menuInst.gameObject.activeInHierarchy) menuInst.gameObject.SetActive(false);
			}
		}
	}
}