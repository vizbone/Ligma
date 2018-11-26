using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour {

	public LayerMask gridLayer;
	public Material[] buildingMaterials;
	public Tower[] towers;
	public LayerMask[] ignoreLayersForBuilding;
	public float gridSize;

	GameObject currentBuild;
	Camera cam;
	ManaSystem manaSys;
	int buildIndex;
	public bool buildMode;

	void Start () 
	{
		cam = GetComponent<Camera> ();
		manaSys = GetComponent<ManaSystem> ();
		buildMode = false;
		buildIndex = 0;
	}

	void Update ()
	{
		BuildFunction ();
	}

	void BuildFunction()
	{
		BuildSwitchAndPreview();
		Cast();
	}

	//handles the building previews and switching the mode on and off
	void BuildSwitchAndPreview ()
	{
		if (Input.GetKeyDown (key: KeyCode.R)) 
		{
			buildMode = buildMode ? false : true;
			if (buildMode) 
			{
				GameObject build = Instantiate (towers[buildIndex].buildModel, Vector3.zero, towers[buildIndex].buildModel.transform.rotation);
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
				GameObject build = Instantiate (towers[buildIndex].buildModel, Vector3.zero, towers[buildIndex].buildModel.transform.rotation);
				currentBuild = build;
			}
			if (Input.GetKeyDown (key: KeyCode.Q) && buildIndex > 0)
			{
				buildIndex--;
				Destroy (currentBuild);
				GameObject build = Instantiate (towers[buildIndex].buildModel, Vector3.zero, towers[buildIndex].buildModel.transform.rotation);
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
				bool canPlace = !isObjectHere (buildPos) && towers[buildIndex].cost <= manaSys.currentMana ? true : false;
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
		bool isIt = false;
		Collider[] intersecting = Physics.OverlapSphere (position, 0.01f);
		if (intersecting.Length == 0)
		{
			return false;
		} else {
			for (int i = 0; i < intersecting.Length; i++)
			{
				for (int index = 0; index < ignoreLayersForBuilding.Length; index++)
				{
					if (intersecting[i].gameObject.layer == ignoreLayersForBuilding[index])
					{
						isIt = true;
					}
				}
			}
			return isIt;
		}
	}

	//handles changing of material (if it can place or not)
	void Material (bool placeability) 
	{
		currentBuild.GetComponent<Renderer> ().material = placeability ? buildingMaterials[0] : buildingMaterials[1];
	}

	//handles placing of building
	void Build (Vector3 buildPos)
	{
		Instantiate (towers[buildIndex].actualTower, buildPos, towers[buildIndex].actualTower.transform.rotation);
		manaSys.ManaMinus (towers[buildIndex].cost);
	}
}