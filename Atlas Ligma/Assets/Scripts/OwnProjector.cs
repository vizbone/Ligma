using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnProjector : MonoBehaviour {

	public Projector projector;
	[SerializeField] bool selected;
	[SerializeField] LayerMask towerLayer;
	[SerializeField] float radiusAspectRatio = 0.9304f; //Aspect ratio of Circle : Canvas //Aspect Ratio of Circle Texture is 0.9304

	// Use this for initialization
	void Start ()
	{
		projector = GetComponent<Projector>();
		radiusAspectRatio = 0.9304f;
		projector.orthographic = true; //Use orthographic view
		projector.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Calculate field of view
		SelectTurret();
	}

	void SelectTurret()
	{
		//When player click, check if player is clicking on a turret
		if (Input.GetMouseButton(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			//QueryTriggerInteraction Ignore to prevent clicking on collider that makes up the turrets range.
			bool hasTower = Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, towerLayer, QueryTriggerInteraction.Ignore);

			if (hasTower)
			{
				if (hit.collider != null)
				{
					Vector3 turretPos = new Vector3(hit.collider.transform.position.x, transform.position.y, hit.collider.transform.position.z);
					transform.position = turretPos;
					projector.orthographicSize = CalculateProjectorRadius(hit.collider.gameObject.GetComponent<CapsuleCollider>().radius * hit.collider.transform.localScale.x);
					projector.enabled = true;
				}
			}
			else projector.enabled = false;
		}
		else
		{
			projector.enabled = false;
		}
	}

	float CalculateProjectorRadius(float turretRadius)
	{
		return (turretRadius / radiusAspectRatio);
	}
}
