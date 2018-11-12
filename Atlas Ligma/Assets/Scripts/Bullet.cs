using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float destroyTime;
	void Start () { Destroy (gameObject, destroyTime); }
	void OnTriggerEnter (Collider other) { if (other.tag == "Enemy") { Destroy (gameObject); } }
}