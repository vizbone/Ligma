using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

	public float fireRate;
	public float bulletSpeed;
	public GameObject bullet;

	List<Enemy> enemies;
	bool cLock;
	float time;
	int index;

	void Start () 
	{
		if (fireRate <= 0) { fireRate = 1; }
		time = 1 / fireRate;
		enemies = new List<Enemy> ();
		cLock = false;
	}

	void Update () 
	{
		if (!cLock) { StartCoroutine ("shoot"); }
	}

	void OnTriggerStay (Collider other) 
	{ 
		if (other.tag == "Enemy" && !enemies.Contains(other.GetComponent<Enemy> ())) { enemies.Add (other.GetComponent<Enemy> ()); }
	}

	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Enemy") { enemies.Remove (other.GetComponent<Enemy> ()); }
	}

	void Shoot () 
	{
		if (enemies.Contains(null)) { enemies.RemoveAll (Enemy => Enemy == null); }
		if (enemies.Count > 0)
		{
			float distance = 100;
			for (int i = 0; i < enemies.Count; i++)
			{
				if ((transform.position - enemies[i].transform.position).magnitude < distance)
				{
					distance = (transform.position - enemies[i].transform.position).magnitude;
					index = i;
				}
			}

			Vector3 direction = -(transform.position - enemies[index].transform.position).normalized;
			GameObject currentBullet = Instantiate (bullet, transform.position + direction * 0.5f, Quaternion.identity);
			currentBullet.GetComponent<Rigidbody> ().velocity = direction * bulletSpeed;
		}
	}

	IEnumerator shoot () 
	{
		cLock = true;
		yield return new WaitForSeconds (time);
		Shoot ();
		cLock = false;
	}
}