using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	public AITemplate ai;
	Image hp;

	int maxHp;
	int currentHp;

	void Start ()
	{
		hp = transform.GetChild (1).GetComponent<Image> ();
		maxHp = ai.hp;
		currentHp = maxHp;
	}

	void Update ()
	{
		if (ai != null)
		{
			currentHp = ai.hp;
			hp.fillAmount = (float)currentHp / maxHp;
		}
		else Destroy(gameObject);
	}
}