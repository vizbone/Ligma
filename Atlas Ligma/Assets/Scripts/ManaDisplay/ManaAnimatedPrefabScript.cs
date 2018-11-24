using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaAnimatedPrefabScript : MonoBehaviour
{
	public Vector3 endpos;
	public float speed;

	void Update ()
	{
		transform.position = Vector3.Lerp (transform.position, endpos, speed * Time.deltaTime);
	}
}