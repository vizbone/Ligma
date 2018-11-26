    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaAnimatedPrefabScript : MonoBehaviour
{
	public Vector3 endPos;
	public float speed;

	void Update ()
	{
		transform.position = Vector3.Lerp (transform.position, endPos, speed * Time.deltaTime);
	}

	void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.name == "Townhall") Destroy (gameObject);
	}
}