using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsingLight : MonoBehaviour
{
    [SerializeField] private Light pointLight;
    [SerializeField] private float intensityMin;
    [SerializeField] private float intensityMax;
    [SerializeField] private float pulseSpeed;

    private bool isIntensityIncreasing = true;

    // Update is called once per frame
    void Update()
    {
        pointLight.intensity += (isIntensityIncreasing ? 1 : -1) * pulseSpeed * Time.deltaTime;


        if (pointLight.intensity > intensityMax && isIntensityIncreasing)
        {
            isIntensityIncreasing = false;
        }
        else if (pointLight.intensity < intensityMin && !isIntensityIncreasing)
        {
            isIntensityIncreasing = true;
        }
    }
}
