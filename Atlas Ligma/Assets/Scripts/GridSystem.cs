using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour {

	public LayerMask gridLayer;
	public Material[] buildingMaterials;
	public Tower[] towers;
	public float gridSize;

	GameObject currentBuild;
	Camera cam;
	ManaSystem manaSys;
	CastSpell spellCaster;
	int buildIndex;
	public bool buildMode;

	void Start () 
	{
		cam = GetComponent<Camera> ();
		manaSys = GetComponent<ManaSystem> ();
		spellCaster = GetComponent<CastSpell>();
		buildMode = false;
		buildIndex = 0;
	}

	void Update ()
	{
		BuildFunction ();
	}

	void BuildFunction()
	{
		if (!spellCaster.isCasting)
		{
			BuildSwitchAndPreview();
			Cast();
		}
	}

	//handles the building previews and switching the mode on and off
	void BuildSwitchAndPreview ()
	{
		if (Input.GetKeyDown (key: KeyCode.R)) 
		{
			buildMode = buildMode ? false : true;
			if (buildMode) 
			{
				GameObject build = Instantiate (towers[buildIndex].buildModel, Vector3.zero, Quaternion.identity);
				currentBuild = build;
			} else {
				Destroy (currentBuild);
			}
		}
		if (buildMode)
		{
			if (Input.GetKeyDown (key: KeyCode.E) && buildIndex < towers.Length - 1)
			{
				buildIndex++;
				Destroy (currentBuild);
				GameObject build = Instantiate (towers[buildIndex].buildModel, Vector3.zero, Quaternion.identity);
				currentBuild = build;
			}
			if (Input.GetKeyDown (key: KeyCode.Q) && buildIndex > 0)
			{
				buildIndex--;
				Destroy (currentBuild);
				GameObject build = Instantiate (towers[buildIndex].buildModel, Vector3.zero, Quaternion.identity);
				currentBuild = build;
			}
		}
	}

	//casts a ray that teleports the attached obj onto the grid
	void Cast ()
	{
		if (buildMode)
		{
			RaycastHit hit;
			Physics.Raycast (cam.ScreenToWorldPoint (Input.mousePosition), transform.forward, out hit, Mathf.Infinity, gridLayer);
			Vector3 buildPos;
			if (hit.collider != null)
			{
				buildPos = new Vector3 (hit.point.x - Mathf.Repeat (hit.point.x, gridSize) + gridSize * 0.5f, 0.5f, hit.point.z - Mathf.Repeat (hit.point.z, gridSize) + gridSize * 0.5f);
				currentBuild.transform.position = buildPos;
				bool canPlace = /*isObjectHere (buildPos) &&*/ towers[buildIndex].cost <= manaSys.currentMana ? true : false;
				Material (canPlace);
				if (Input.GetMouseButtonDown (0) && canPlace)
				{
					Build (buildPos);
				}
			}
		}
	}

	//check if object is present
	bool isObjectHere (Vector3 position)
	{
		Collider[] intersecting = Physics.OverlapSphere (position, 0.01f);
		return intersecting.Length == 0 ? true : false;
	}

	//handles changing of material (if it can place or not)
	void Material (bool placeability) 
	{
		currentBuild.GetComponent<Renderer> ().material = placeability ? buildingMaterials[0] : buildingMaterials[1];
	}

	//handles placing of building
	void Build (Vector3 buildPos)
	{
		Instantiate (towers[buildIndex].actualTower, buildPos, Quaternion.identity);
		manaSys.ManaMinus (towers[buildIndex].cost);
	}
}