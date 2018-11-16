using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType {ground, air, both};

[RequireComponent(typeof(CapsuleCollider))]
public abstract class TurretTemplate : MonoBehaviour {

	//Values to be set
	public float fireRate; //To be set
	public float bulletSpeed; //To be set
	public float range; //To be set
	public AttackType attackType; //To be set

	public float coolDown;
	
	[SerializeField] private CapsuleCollider collider;
	[SerializeField] private GameObject bullet; //To be set in Inspector

	[SerializeField] List<AI> enemies;

	protected virtual void Start()
	{
		collider = GetComponent<CapsuleCollider>();
		collider.isTrigger = true;
		enemies = new List<AI>();
		SetValues();

		//Set range of turret depending on type
		collider.radius = range/2;
		//Set cooldown
		coolDown = fireRate;
	}

	protected virtual void Update()
	{
		coolDown = Mathf.Max(coolDown -= Time.deltaTime, 0);
		if (coolDown <= 0) Shoot();
	}

	protected abstract void SetValues();

	protected virtual void Shoot()
	{
		//Remove any "Enemy" from list if the Enemy Reference is not present
		if (enemies.Contains(null)) enemies.RemoveAll(AI => AI == null);

		if (enemies.Count > 0)
		{
			float shortestDist = Mathf.Infinity;
			int index = 0;
			//print ("==========================================================================");
			for (int i = 0; i < enemies.Count; i++)
			{
				//For attacking enemies closest to Townhall
				float enemyDistance = enemies[i].CheckDistance();
				//print ("Index " + i + ": " + enemyDistance);
				if (enemyDistance < shortestDist)
				{
					shortestDist = enemyDistance;
					index = i;
					//print ("i updated");
				}

				//For attacking enemies closest to Turret
				/*if ((transform.position - enemies[i].transform.position).magnitude < distance)
				{
					distance = (transform.position - enemies[i].transform.position).magnitude;
					//index = i;
				}*/
			}

			Vector3 direction = -(transform.position - enemies[index].transform.position).normalized;
			GameObject currentBullet = Instantiate(bullet, transform.position + direction * 0.5f, Quaternion.identity);
			currentBullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
			coolDown = fireRate;
			//print ("Shortest: " + shortestDist);
		}
		else return;
	}

	void OnTriggerStay (Collider other)
	{
		if (other.tag == "AI" && !enemies.Contains(other.GetComponent<AI>())) enemies.Add(other.GetComponent<AI>());
		//print("Working");
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "AI") { enemies.Remove(other.GetComponent<AI>()); }
	}
}
