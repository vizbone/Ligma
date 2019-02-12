using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Bullet") Destroy (other.gameObject);
	}
}