using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType {crossbow, rocket, catapult, cannon};

public class Bullet : MonoBehaviour
{
	[SerializeField] private BulletType bulletType; //To be set in Prefab
	public float dmg;
	public float lifetime;

	private void Start()
	{
		SetValues(bulletType);
		lifetime = 5;
		Destroy(gameObject, lifetime);
	}

	void SetValues(BulletType type)
	{
		switch (bulletType)
		{
			case BulletType.crossbow:
				//print("Crossbow Bullet Hit");
				Destroy(gameObject);
				break;
			case BulletType.cannon:
				//Get Component and Deal Damage
				//Pierce Effect so destroyed by effective distance
				break;
			case BulletType.catapult:
				//print("Catapult Bullet Hit");
				//Splash Damage. Instantiate Explosion. Explosion will deal the damage.
				Destroy(gameObject);
				break;
			case BulletType.rocket:
				//print("Rocket Bullet Hit");
				//Only attack aerial enemies
				Destroy(gameObject);
				break;
		}
	}

	void Hit(BulletType bulletType)
	{
		switch (bulletType)
		{
			case BulletType.crossbow:
				dmg = 1;
				break;
			case BulletType.cannon:
				dmg = 2;
				break;
			case BulletType.catapult:
				dmg = 1;
				break;
			case BulletType.rocket:
				dmg = 2;
				break;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "AI") Hit(bulletType);
	}
}