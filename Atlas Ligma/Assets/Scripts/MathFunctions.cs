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
	public static float SmoothPingPong(float time, float maxValue, float speed)
	{
		return (0.5f * maxValue) * Mathf.Sin(Mathf.PI * time / (1/speed)) + (0.5f * maxValue);
	}

	public static float ResetLerpTime()
	{
		return 0;
	}

	public static void ParabolicCurve (Vector3 target, float amplitude, float currentStep, Transform yourself, float frequency, Vector3 oriPos, float currentY, float terrainOffset)
	{
		float nextStep = currentStep + 2 * Time.deltaTime;

		float x = (target.x - oriPos.x) * nextStep + oriPos.x;
		float z = (target.z - oriPos.z) * nextStep + oriPos.z;
		float temp = (target.x - oriPos.x) * nextStep > 0 ? (target.x - oriPos.x) * nextStep : -((target.x - oriPos.x) * nextStep);
		float temp1 = Mathf.Abs(Mathf.Asin (currentY / amplitude));
		float y = (amplitude * Mathf.Sin (temp * Mathf.PI * (1 / frequency) + temp1)) + terrainOffset;

		yourself.position = new Vector3 (x, y, z);
	}
}