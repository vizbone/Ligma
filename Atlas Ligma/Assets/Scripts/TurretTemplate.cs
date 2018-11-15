using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTemplate : MonoBehaviour {

	public float fireRate;
	public float bulletSpeed;
	public float range;
	public GameObject bullet;

	protected virtual void SetValues()
	{
		fireRate = 1;
		bulletSpeed = 5;
	}
}
