using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GridSystem : MonoBehaviour {

	public LayerMask gridLayer;
	public Material[] buildingMaterials;
	public Tower[] towers;
	public LayerMask ignoreLayersForBuilding;
	public float gridSize;
	public Vector3 offset;

	public GameObject currentBuild;
	Camera cam;
	ManaSystem manaSys;
	public int buildIndex;
	public bool buildMode;
	public bool canBuild;

	public static bool isBuilding;

	public GraphicRaycaster myRay;

	UIButtonFunctions uiButton;
	TutorialManager tut;

	bool rocketLock;
	public bool buildLock;

	void Start () 
	{
		cam = GetComponent<Camera> ();
		manaSys = GetComponent<ManaSystem> ();
		myRay = ManaSystem.inst.gui.GetComponent<GraphicRaycaster>();
		uiButton = manaSys.buttonFunctionUI;
		if (SceneManager.GetActiveScene ().name == "Level 1") tut = manaSys.tutorial;

		rocketLock = SceneManager.GetActiveScene ().name == "Level 1" ? true : false;
		buildLock = SceneManager.GetActiveScene ().name == "Level 1" ? true : false;

		buildMode = false;
		buildIndex = 0;
	}

	void Update ()
	{
		if (ManaSystem.gameStateS == GameStates.started || ManaSystem.gameStateS == GameStates.afterWin) BuildFunction ();
	}

	void BuildFunction()
	{
		//BuildSwitchAndPreview();
		Cast();
		Shortcut ();
	}

	void Shortcut ()
	{
		if (!buildLock) {
			if (Input.GetKeyDown (key: KeyCode.Alpha1)) uiButton.CrossbowBuild ();
			else if (Input.GetKeyDown (key: KeyCode.Alpha2)) uiButton.CannonBuild ();
			else if (Input.GetKeyDown (key: KeyCode.Alpha3)) uiButton.CatapultBuild ();
			else if (Input.GetKeyDown (key: KeyCode.Alpha4) && !rocketLock) uiButton.RocketsBuild ();
		}
	}

	//handles the building previews and switching the mode on and off
	/*void BuildSwitchAndPreview ()
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
	}*/

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
				buildPos = new Vector3 (hit.point.x - Mathf.Repeat (hit.point.x, gridSize) + gridSize * 0.5f, hit.point.y, hit.point.z - Mathf.Repeat (hit.point.z, gridSize) + gridSize * 0.5f) + offset;
				if (currentBuild != null) currentBuild.transform.position = buildPos;
				else return;

				//Check for any UI Elements hovered over
				List<RaycastResult> results = new List<RaycastResult>();
				PointerEventData data = new PointerEventData(null);
				data.position = Input.mousePosition;
				myRay.Raycast(data, results);

				bool canPlace = false;

				canPlace = !isObjectHere(buildPos) && towers[buildIndex].cost < manaSys.currentMana ? true : false;
				if (results.Count > 0) canPlace = false;

				Material(canPlace);
				
				if (Input.GetMouseButtonDown (0) && canPlace && EventSystem.current.IsPointerOverGameObject())
				{
					Build (buildPos);
				}
			}
		}
	}

	//check if object is present
	bool isObjectHere (Vector3 position)
	{
		bool isObjHere = false;

		Collider[] intersectingColliders = Physics.OverlapSphere(position /*+ new Vector3(0, 0.5f, 0)*/, 0.25f);

		if (intersectingColliders.Length == 0)
		{
			print("nothing here");
			isObjHere = false;
		}
		else
		{
			foreach (Collider collider in intersectingColliders)
			{
				if (!collider.isTrigger)
				{
					if (1 << collider.gameObject.layer != (ignoreLayersForBuilding.value & 1 << collider.gameObject.layer))
					{
						isObjHere = true;
						break;
					}
					else isObjHere = false;
				}
			}
		}

		return isObjHere;
	}

	//handles changing of material (if it can place or not)
	void Material (bool placeability) 
	{
		Renderer r = currentBuild.GetComponent<Renderer>();

		if (r.materials.Length > 1)
		{
			Material[] mats = new Material[2];
			mats[0] = placeability ? buildingMaterials[0] : buildingMaterials[1];
			mats[1] = placeability ? buildingMaterials[0] : buildingMaterials[1];

			r.materials = mats;
		}
		else r.material = placeability ? buildingMaterials[0] : buildingMaterials[1];
	}

	//handles placing of building
	void Build (Vector3 buildPos)
	{
		Instantiate(towers[buildIndex].actualTower, buildPos, towers[buildIndex].actualTower.transform.rotation);
		manaSys.ManaMinus(towers[buildIndex].cost, buildPos, 2);
	}

	public void Uninteractable()
	{
		canBuild = false;
	}
}