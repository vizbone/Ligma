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

		for (int i = 13; i <= 16; i++)
		{
			for (int i2 = i; i2 <= 16; i2++)
			{
				if (i2 != i) Physics.IgnoreLayerCollision(i2, i);
			}
		}
		for (int i = 1; i < 20; i++)
		{
			if ((i < 13 || i > 16) && i != 11)
			{
				Physics.IgnoreLayerCollision (i, 19);
			}
		}
	}
}