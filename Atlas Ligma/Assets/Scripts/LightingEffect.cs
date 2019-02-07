using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingEffect : MonoBehaviour
{
	float intensity;
	[SerializeField] float maxIntensity; //Set intensity that is wanted
	[SerializeField] float minIntensity;
	[SerializeField] float glowSpeed;
	Light light;

    // Start is called before the first frame update
    void Start()
    {
		light = GetComponent<Light>();
		if (glowSpeed == 0) glowSpeed = 3;
    }

    // Update is called once per frame
    void Update()
    {
		intensity = MathFunctions.SmoothPingPong(Time.time, (maxIntensity - minIntensity), glowSpeed);
		light.intensity = minIntensity + intensity;
    }
}
