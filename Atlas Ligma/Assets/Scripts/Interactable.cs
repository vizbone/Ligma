using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
	[System.Serializable]
	public class Action
	{
		public Color color; //Not Needed Actually
		public Sprite sprite;
		public string title;
	}

	public Action[] options;
	public GameObject currentTower;
	public TurretTemplate turret;
	MeshCollider sensor;
	[SerializeField] LayerMask towerLayer;
	[SerializeField] RadialMenu menuInst;

	public static Interactable inst;

	private void Start()
	{
		towerLayer.value = 1 << 10;
		currentTower = gameObject;
		sensor = GetComponent<MeshCollider>();
		turret = gameObject.GetComponent<TurretTemplate>();
	}

	void Update()
	{
		if (ManaSystem.gameStateS == GameStates.started || ManaSystem.gameStateS == GameStates.afterWin)
		{
			if (Input.GetMouseButtonDown(0))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;

				//QueryTriggerInteraction Ignore to prevent clicking on collider that makes up the turrets range.
				bool hasTower = sensor.Raycast(ray, out hit, Mathf.Infinity);

				if (hasTower)
				{
					if (hit.collider != null)
					{
						if (inst == null) inst = this;
						print(inst.name);

						if (inst == this)
						{
							if (menuInst == null)
							{
								menuInst = RadialMenuSpawner.ins.SpawnMenu(this, turret);
							}
							else
							{
								if (!menuInst.gameObject.activeInHierarchy)
								{
									menuInst.gameObject.SetActive(true);
									menuInst.CheckDisabled();
								}
							}
						}
					}
				}
				else
				{
					if (menuInst != null && menuInst.gameObject.activeInHierarchy) menuInst.gameObject.SetActive(false);
				}

				if (inst != this && menuInst != null && menuInst.gameObject.activeInHierarchy) menuInst.gameObject.SetActive(false);
			}
			else if (Input.GetMouseButtonUp(0))
			{
				inst = null;
			}
		}
	}
}