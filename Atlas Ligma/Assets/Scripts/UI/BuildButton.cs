using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] TurretInfo buttonTurretInfo;
	[SerializeField] float timer;
	[SerializeField] bool onHover;
	[SerializeField] string turretType;

	void Start()
	{
		timer = 0.3f;
		buttonTurretInfo.UpdateTurretInfo(turretType);
	}

	void Update()
	{
		if (onHover)
		{
			timer = Mathf.Max(timer - Time.deltaTime, 0);
			if (timer <= 0 && !buttonTurretInfo.gameObject.activeInHierarchy) buttonTurretInfo.gameObject.SetActive(true);
		}
		else timer = 0.3f;
	}

	public void OnPointerEnter(PointerEventData data)
	{
		onHover = true;
	}

	public void OnPointerExit(PointerEventData data)
	{
		onHover = false;
		buttonTurretInfo.gameObject.SetActive(false);
	}
}
