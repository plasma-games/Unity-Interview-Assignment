using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

// This script controls the point lights in the background of the game scene.
// They pulse until the player submits their responses. If they got all the
// questions right, the lights turn green, otherwise they stay red.
public class PulsingLight : MonoBehaviour
{
    [SerializeField] private Light pointLight;
    [SerializeField] private float intensityMin;
    [SerializeField] private float intensityMax;
    [SerializeField] private float pulseSpeed;
    [SerializeField] private Color winColor;
    [SerializeField] private float colorChangeDelay;
    [SerializeField] private float colorChangeDuration;

    private bool isIntensityIncreasing = true;
    private bool stopPulsing = false;
    private bool didWin = false;
    private bool hasColorChanged = false;

    public void ShowEndLevel(bool _didWin)
    {
        stopPulsing = true;
        didWin = _didWin;
    }

    private void Update()
    {
        // Wait until the light has reached max intensity to stop pulsing
        if (stopPulsing && pointLight.intensity >= intensityMax)
        {
            // Change to green if the player got all of the questions correct
            if (didWin && !hasColorChanged)
            {
                hasColorChanged = true;
                StartCoroutine(ChangeColor());
            }

            return;
        }

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

    private IEnumerator ChangeColor()
    {
        yield return new WaitForSeconds(colorChangeDelay);

        Color startColor = pointLight.color;
        float elapsedTime = 0.0f;

        // Use Color.Lerp to gradually transition the color from red to green
        while (elapsedTime < colorChangeDuration)
        {
            elapsedTime += Time.deltaTime;
            pointLight.color = Color.Lerp(startColor, winColor, (elapsedTime / colorChangeDuration));
            yield return null;
        }
    }
}
