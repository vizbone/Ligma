using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable2 : MonoBehaviour
{
	[System.Serializable]
	public class Action
	{
		public Color color; //Not Needed Actually
		public Sprite sprite;
		public string title;
	}

	public class MenuSet
	{
		public TurretTemplate tt;
		public RadialMenu rm;
	}

	public Action[] prebuiltOptions;
	public Action[] ownOptions;

	public List<MeshCollider> meshes;
	public List<MenuSet> menuSets;

	[SerializeField] LayerMask towerLayer;
	public int Ĺ;
	WaveSystem waveSys;

	void Start ()
	{
		towerLayer.value = 1 << 10;
		waveSys = FindObjectOfType<WaveSystem> ();
		menuSets = new List<MenuSet> ();
	}

	public void AddSet (TurretTemplate ttt)
	{
	}

	void Update ()
	{
		if (ManaSystem.gameStateS == GameStates.started || ManaSystem.gameStateS == GameStates.afterWin)
		{
			if (Input.GetMouseButtonDown (0))
			{
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				RadialMenu[] rm = FindObjectsOfType<RadialMenu> ();
				int temp = -1;
				for (int i = 0; i < meshes.Count; i++) {
					//QueryTriggerInteraction Ignore to prevent clicking on collider that makes up the turrets range.
					bool hasTower = meshes[i].Raycast (ray, out hit, Mathf.Infinity);

					if (hasTower)
					{
						if (hit.collider != null)
						{
							print ("hello sir i am number" + i);
							if (!doIExist)
							{
								RadialMenuSpawner.ins.SpawnMenu (this, meshes[i].GetComponent<TurretTemplate> ());
							} /* if it does exist*/
							else
							{
								rm[correspondence].gameObject.SetActive (true);
							}
							temp = i;
							//after confirmation of a hit, ill break
							break;
						}
					}
				}

				if (temp != -1)
				{
					for (int i = 0; i < meshes.Count; i++)
					{
						if (i != temp)
					}
				}

				for (int i = 0; i < meshes.Count; i++)
				{
					if (menuInst != null && menuInst.gameObject.activeInHierarchy) menuInst.gameObject.SetActive (false);
				}
			}
		}
	}
}