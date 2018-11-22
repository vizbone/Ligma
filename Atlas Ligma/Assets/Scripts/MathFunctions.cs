using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LerpValues
{
	public float currentLerpTime;
	public float totalLerpTime;
}

public class MathFunctions : MonoBehaviour
{
	//For Turret Upgrading Etc
	public static float ReturnNewIncrement (float affectedValue, float oldIncrement, float newIncrement)
	{
		return (affectedValue - oldIncrement + newIncrement);
	}

	//Function to return t where t is the value for Lerp Interpolation
	public static float SinerpValue(float currentLerpTime, float totalLerpTime)
	{
		float v = currentLerpTime / totalLerpTime;
		return Mathf.Sin(v * Mathf.PI * 0.5f);
	}

	//Similar to Sinerp, except that it is an ease in effect rather than an ease in effect
	public static float CoserpValue(float currentLerpTime, float totalLerpTime)
	{
		float v = currentLerpTime / totalLerpTime;
		return v = 1 - Mathf.Cos(v * Mathf.PI * 0.5f);
	}

	//Stores the result of the Math equation for smooth interpolation of the Ping Pong Function
	public static float SmoothPingPong(float v, float cap)
	{
		return (0.5f * cap) * Mathf.Sin(Mathf.PI * v / cap) + (0.5f * cap);
	}

	public static float ResetLerpTime()
	{
		return 0;
	}

}
