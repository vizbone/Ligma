using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct STurretValues
{
	public bool boostsStats;
	public float range; //Stores range that it affects. Does not change when upgraded
	public float buff; //Stores multiplier that affects Turret Stats
	public float investmentValue; //Stores the increment of investment
}

[RequireComponent (typeof (CapsuleCollider))]
public class SupportTurret : MonoBehaviour 
{
	public int level;
	public STurretValues sTurretValues;
	[SerializeField] CapsuleCollider collider;
	[SerializeField] List<TurretTemplate> affectedTurrets;

    private void Start()
    {
		level = 1;
		collider = GetComponent<CapsuleCollider> ();
		affectedTurrets = new List<TurretTemplate>();

		SetValues (sTurretValues.boostsStats);

		//Set Colliders
		collider.height = 5;
		collider.center = new Vector3(0, -0.5f, 0);
		collider.radius = sTurretValues.range / 2;
    }

	public void SetValues (bool boostsStats)
	{
		sTurretValues.range = 5;
		sTurretValues.buff = 0.1f; //Set any values
		sTurretValues.investmentValue = boostsStats ? 0.05f : 0.1f;
	}

	public void Upgrade (bool boostsStats)
	{
		level = Mathf.Min (++level, 3);

		float oldInvestmentValue = sTurretValues.investmentValue;
		float oldBuff = sTurretValues.buff;

		switch (level)
		{
			case 1:
				sTurretValues.buff = 0.1f;
				sTurretValues.investmentValue = boostsStats ? 0.05f : 0.1f;
				break;
			case 2:
				sTurretValues.buff = 0.2f;
				sTurretValues.investmentValue = boostsStats ? 0.1f : 0.15f;
				break;
			case 3:
				sTurretValues.buff = 0.3f;
				sTurretValues.investmentValue = boostsStats ? 0.15f : 0.25f;
				break;
			default:
				sTurretValues.buff = 0.1f;
				sTurretValues.investmentValue = boostsStats ? 0.05f : 0.1f;
				break;
		}

		foreach (TurretTemplate turret in affectedTurrets)
		{
			turret.BoostStats (turret.turretValues, sTurretValues, oldInvestmentValue, oldBuff, boostsStats);
		}
	}

	public void ApplyToAffectedTurrets (float oldInvestmentValue, float newInvestmentValue)
	{
		foreach (TurretTemplate turret in affectedTurrets)
		{
			if (turret.isPrebuilt)
			{
				turret.manaReturnPercentageS = MathFunctions.ReturnNewIncrement(turret.manaReturnPercentageS, oldInvestmentValue, newInvestmentValue);
				turret.RecalculateInvestmentValue ();
			}
		}
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Tower" && !other.isTrigger)
        {
			affectedTurrets.Add (other.GetComponent<TurretTemplate> ());
			if (affectedTurrets[affectedTurrets.Count - 1].isPrebuilt) affectedTurrets[affectedTurrets.Count - 1].manaReturnPercentageS += sTurretValues.investmentValue;
			if (sTurretValues.boostsStats) affectedTurrets[affectedTurrets.Count - 1].fireRateBuff += sTurretValues.buff;
			affectedTurrets[affectedTurrets.Count - 1].RecalculateInvestmentValue ();
			affectedTurrets[affectedTurrets.Count - 1].RecalculateFireRate ();
			print ("Buff");
		}
    }
   

}
