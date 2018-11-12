using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastSpell : MonoBehaviour
{
	//Spell Casting
	public bool isCasting;
	[SerializeField] float castTime; //Time which the player has to hold for Spell Circle to appear
	[SerializeField] float castSpeed; //How fast the spell circle grows
	[SerializeField] float spellRadius; //Local Scale of the Spell Circle
	//[SerializeField] bool spellCancelled; //spell will cancel if player move the mouse beyond the buffer zone

	//Spell Position
	[SerializeField] Vector3 mouseStartPos; //Records where Player starts clicking
	[SerializeField] float effectiveOffset; //Buffer for player movement of mouse
	[SerializeField] LayerMask hitLayers; //Store layers for Screentopoint raycast

	//For Spell Circle Instantiation
	[SerializeField] GameObject spellCirclePrefab;
	[SerializeField] GameObject spellCircle;

	[SerializeField] GridSystem grid;
	[SerializeField] ManaSystem manaSys;

	[SerializeField] float halfSec; 

	// Use this for initialization
	void Start()
	{
		grid = GetComponent<GridSystem>();
		manaSys = GetComponent<ManaSystem>();
		effectiveOffset = Screen.height * 0.03f; //Buffer zone is 3% of screen width
		castTime = 0.5f;
		castSpeed = 1f;
	}

	// Update is called once per frame
	void Update()
	{
		if (!grid.buildMode) Casting();
		else
		{
			if (spellCircle != null)
			{
				GameObject spellCircleInstance = spellCircle;
				spellCircle = null;
				Destroy(spellCircleInstance);
			}
		}
	}

	void Casting()
	{
		if (Input.GetMouseButtonDown(0))
		{
			//Get Mouse Starting Position
			mouseStartPos = Input.mousePosition;

			//Set Values Back to Default
			castTime = 0.5f;
			spellRadius = 0;
			halfSec = 0;
		}
		else if (Input.GetMouseButton(0))
		{
			//Check current mouse position and compare the distance from start pos
			float xDiff = Input.mousePosition.x - mouseStartPos.x;
			float yDiff = Input.mousePosition.y - mouseStartPos.y;

			//Check if current mouse position is within buffer zone
			if (Mathf.Abs(xDiff) < effectiveOffset && Mathf.Abs(yDiff) < effectiveOffset)
			{
				castTime = Mathf.Max(castTime -= Time.deltaTime, 0);

				//If Player has held down left click for > 0.5s, start casting the spell
				if (castTime <= 0)
				{
					isCasting = true;

					if (spellCircle == null) //If spell circle has not been created, create one
					{
						//Set Position of the Spell Circle
						Vector3 spellPos = Vector3.zero;
						Ray mouseRay = Camera.main.ScreenPointToRay(mouseStartPos);
						RaycastHit[] hits = Physics.RaycastAll(mouseRay, Mathf.Infinity, hitLayers);
						foreach (RaycastHit hit in hits)
						{
							if (hit.collider.tag == "Grid") spellPos = new Vector3 (hit.point.x, hit.point.y + 0.01f, hit.point.z);
						}
						//Create Spell Circle Instance
						spellCircle = Instantiate(spellCirclePrefab, spellPos, Quaternion.identity);
					}
					else //Increase the local scale of the spell circle for the increasing radius effect.
					{
						spellRadius = Mathf.Min(spellRadius += castSpeed * Time.deltaTime, 1);
						spellCircle.transform.localScale = new Vector3(spellRadius, spellRadius, spellRadius);
						print("Casting");
					}

					if (halfSec >= 0.5f)
					{
						if (spellCircle.transform.localScale.x < 1) manaSys.ManaMinus(5);
						else manaSys.ManaMinus(3);

						halfSec = 0;
					}
					else halfSec += Time.deltaTime;
				}
			}
			else  //Cancel the spell if the Player moves the mouse beyond the buffer
			{
				isCasting = false;

				if (spellCircle != null)
				{
					GameObject spellCircleInstance = spellCircle;
					spellCircle = null;
					Destroy(spellCircleInstance);
				}
			}
		}
		else if (Input.GetMouseButtonUp(0))
		{
			Cast(isCasting);
			isCasting = false;
		}
	}

	void Cast(bool isCasting)
	{
		if (isCasting)
		{
			GameObject spellCircleInstance = spellCircle;
			spellCircle = null;
			if (manaSys.currentMana >= 50)
			{
				manaSys.ManaMinus(50);
				spellCircleInstance.GetComponent<SpellCircle>().castSpell = true;
			}
			Destroy(spellCircleInstance, 0.1f);
			print("Spell is casted over " + spellRadius + " units");
		} 
		else print("Spell Was Cancelled");
	}
}
