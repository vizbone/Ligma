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
	public static float SmoothPingPong(float time, float maxValue, float speed = 1)
	{
		return (0.5f * maxValue) * Mathf.Sin(Mathf.PI * speed * time / maxValue) + (0.5f * maxValue);
	}

	public static float ResetLerpTime()
	{
		return 0;
	}

	public static void ParabolicCurve (Vector3 target, float amplitude, float currentStep, Transform yourself, float frequency, Vector3 oriPos, float currentY)
	{
		float nextStep = Mathf.Min (currentStep + 2 * Time.deltaTime, 1);

		float x = Mathf.Lerp (oriPos.x, target.x, nextStep);
		float z = Mathf.Lerp (oriPos.z, target.z, nextStep);
		float temp = Mathf.Lerp (oriPos.x, target.x, nextStep) - oriPos.x > 0 ? Mathf.Lerp (oriPos.x, target.x, nextStep) - oriPos.x : -(Mathf.Lerp (oriPos.x, target.x, nextStep) - oriPos.x);
		float y = (amplitude * Mathf.Sin (temp * Mathf.PI * (1 / frequency))) + currentY;

		yourself.position = new Vector3 (x, y, z);
	}
}