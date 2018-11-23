using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Misc : MonoBehaviour {
	void Start ()
	{
		Physics.IgnoreLayerCollision (11, 11);
	}
}