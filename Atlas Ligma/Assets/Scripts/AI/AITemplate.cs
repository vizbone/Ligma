using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AITemplate : MonoBehaviour {

	public AttackType enemyType;
	public int hp;
	public int manaDrop = 10;
	public float defaultMoveSpeed;

	public HealthBar hpPack;
	public HealthBar hpPrefab;
	public Transform worldCanvas;

	public float timer;

	protected virtual void Start ()
	{
		timer = 0.1f;
		WaveSystem.enemyListS.Add(this);

		if (enemyType == AttackType.ground)
		{
			if (hp == 0) hp = 150;
			manaDrop = 30;
		}
		else if (enemyType == AttackType.air)
		{
			if (hp == 0) hp = 350;
			manaDrop = 70;
		}
		//Sea Enemies will be overriding the Start Function
	}

	public abstract float CheckDistance();

	private void OnDestroy()
	{
		WaveSystem.enemyListS.Remove(this);
	}

	public void Update ()
	{
		if (timer > 0) timer -= Time.deltaTime;
		HealthBar ();
	}

	public void HealthBar ()
	{
		if (timer > 0)
		{
			if (hpPack == null)
			{
				HealthBar hpBar = Instantiate (hpPrefab, worldCanvas);
				hpBar.ai = this;
				hpPack = hpBar;
			}
			hpPack.gameObject.SetActive (true);
			if (hp <= 0) Destroy (hpPack.gameObject);
		} else if (hpPack != null) hpPack.gameObject.SetActive (false);

		if (hpPack != null) hpPack.transform.position = transform.position + worldCanvas.transform.up * 0.6f + (worldCanvas.transform.forward * -1) * 5;
	}

	public void ResetTimer ()
	{
		timer = 2;
	}
}
