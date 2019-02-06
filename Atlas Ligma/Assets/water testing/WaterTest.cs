using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTest : MonoBehaviour
{
	Material mat;

	void Start ()
	{
		mat = GetComponent<SkinnedMeshRenderer> ().material;
	}

	void Update ()
	{
		mat.mainTextureOffset += Vector2.one * Time.deltaTime * -0.05f;
	}
}