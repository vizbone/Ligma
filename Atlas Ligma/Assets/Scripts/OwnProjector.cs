using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OwnProjector : MonoBehaviour {

	[Header("Systems and Managers")]
	[SerializeField] GridSystem gridSys;
	[SerializeField] GraphicRaycaster worldSpaceRaycaster;

	[Header("Projector")]
	public Projector projector;
	[SerializeField] bool selected;
	[SerializeField] LayerMask towerLayer;
	[SerializeField] float radiusAspectRatio = 0.9304f; //Aspect ratio of Circle : Canvas //Aspect Ratio of Circle Texture is 0.9304

	// Use this for initialization
	void Start ()
	{
		gridSys = ManaSystem.inst.gridSystem;
		worldSpaceRaycaster = ManaSystem.inst.worldSpaceCanvas.GetComponent<GraphicRaycaster>();

		transform.rotation = Quaternion.Euler(90, 0, 0);
		projector = GetComponent<Projector>();
		radiusAspectRatio = 0.9304f;
		projector.orthographic = true; //Use orthographic view
		projector.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (ManaSystem.gameStateS == GameStates.started || ManaSystem.gameStateS == GameStates.afterWin)
		{
			if (gridSys.buildMode) return;//CastBuildRange();
			else SelectTurret(); //Calculate field of view
		}
	}

	void SelectTurret()
	{
		//When player click, check if player is clicking on a turret
		if (Input.GetMouseButtonDown (0))
		{
			//Prevent Interactable from spawning Buttons if they are already hovering over a Button (START)
			List<RaycastResult> results = new List<RaycastResult>();
			PointerEventData data = new PointerEventData(null);
			data.position = Input.mousePosition;
			worldSpaceRaycaster.Raycast(data, results);

			if (results.Count > 0) return; //Prevent Interactable from spawning Buttons if they are already hovering over a Button (END)

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			//QueryTriggerInteraction Ignore to prevent clicking on collider that makes up the turrets range.
			bool hasTower = Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, towerLayer, QueryTriggerInteraction.Ignore);
			selected = hasTower;

			if (hasTower && !gridSys.buildMode)
			{
				//print(hit.collider.name);
				//print(hit.collider.gameObject.layer);
				

				if (hit.collider != null)
				{
					Vector3 turretPos = new Vector3(hit.collider.transform.position.x, transform.position.y, hit.collider.transform.position.z);
					transform.position = turretPos;
					// * by local scale in case of any scaling errors from exported turrets
					projector.orthographicSize = CalculateProjectorRadius(hit.collider.gameObject.GetComponent<CapsuleCollider>().radius * hit.collider.transform.localScale.x);
					projector.enabled = true;
					//print (projector.orthographicSize);
				}
			}
			else
			{
				//print("No Object");
				projector.enabled = false;
			}
		}
	}

	/*void CastBuildRange()
	{
		CalculateProjectorRadius((turretValues.range / 2) / gameObject.transform.localScale.x);
	}*/

	float CalculateProjectorRadius(float turretRadius)
	{
		return (turretRadius / radiusAspectRatio);
	}
}
