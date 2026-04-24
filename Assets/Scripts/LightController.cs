using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {

    // Array of lights to control
    public Light[] lights;

    // Boolean to control whether the light intensity should be constant or gradually increase
    public bool constantIntensity = false;

    // Start intensity level
    private float startIntensity = 0f;

    // End intensity level
    private float endIntensity = 0.6f;

    // Time in seconds for the intensity to reach from startIntensity to endIntensity
    public float duration = 5 * 60; // 20 minutes

    void Start()
    {
        // Set the initial intensity of the lights
        foreach (Light light in lights)
        {
            light.intensity = constantIntensity ? endIntensity : startIntensity;
        }

        // If the intensity is not constant, start the coroutine to gradually increase the intensity
        if (!constantIntensity)
        {
            StartCoroutine(IncreaseIntensityOverTime());
        }
    }

    IEnumerator IncreaseIntensityOverTime()
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            foreach (Light light in lights)
            {
                light.intensity = Mathf.Lerp(startIntensity, endIntensity, elapsedTime / duration);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure that the intensity is set to the endIntensity after the loop
        foreach (Light light in lights)
        {
            light.intensity = endIntensity;
        }
    }
}
