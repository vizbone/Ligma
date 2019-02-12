using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AITemplate : MonoBehaviour {

	public AttackType enemyType;
	public int maxHp;
	public int hp;
	public int manaDrop = 10;

	public AudioSource audioSource;

	public HealthBar hpPack;
	public HealthBar hpPrefab;
	public Transform worldCanvas;

	public float timer;

	protected virtual void Start ()
	{
		//timer = 0.1f;
		WaveSystem.enemyListS.Add(this);
		
		if (enemyType == AttackType.ground)
		{
			if (hp == 0) hp = 150;
			manaDrop = 20;
		}
		else if (enemyType == AttackType.air)
		{
			if (hp == 0) hp = 350;
			manaDrop = 70;
		}
		//Sea Enemies will be overriding the Start Function
		maxHp = hp;
	}

	public abstract float CheckDistance();

	private void OnDestroy()
	{
		WaveSystem.enemyListS.Remove(this);
		//hpPack.ai = null;

		switch (enemyType)
		{
			case AttackType.ground:
				ManaSystem.inst.gui.enemiesLeft[0]--;
				break;
			case AttackType.air:
				ManaSystem.inst.gui.enemiesLeft[1]--;
				break;
			case AttackType.sea:
				ManaSystem.inst.gui.enemiesLeft[2]--;
				break;
		}

		ManaSystem.inst.gui.enemiesLeft[3]--;
	}

	protected virtual void Update ()
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

		float vertOffset = enemyType == AttackType.ground ? 0.6f : enemyType == AttackType.air ? 2f : 0.6f;
		if (hpPack != null) hpPack.transform.position = transform.position + worldCanvas.transform.up * vertOffset + (worldCanvas.transform.forward * -1) * 10;
	}

	public void ResetTimer ()
	{
		timer = 2;
	}
}
