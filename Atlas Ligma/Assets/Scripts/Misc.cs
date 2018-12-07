using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Misc : MonoBehaviour {
	void Awake ()
	{
		Physics.IgnoreLayerCollision (11, 11);
		for (int i = 1; i < 13; i++)
		{
			Physics.IgnoreLayerCollision (i, 12);
		}
	}
}