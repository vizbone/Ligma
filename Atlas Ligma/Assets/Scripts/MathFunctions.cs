using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathFunctions : MonoBehaviour
{
	public static float ReturnNewIncrement (float affectedValue, float oldIncrement, float newIncrement)
	{
		return (affectedValue - oldIncrement + newIncrement);
	}

	//Add Sinerp and Coserp Scripts here

}
