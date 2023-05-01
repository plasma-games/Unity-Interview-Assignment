using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsingLight : MonoBehaviour
{
    [SerializeField] private Light pointLight;
    [SerializeField] private float intensityMin;
    [SerializeField] private float intensityMax;
    [SerializeField] private float pulseSpeed;
    [SerializeField] private Color winColor;
    [SerializeField] private float colorChangeDelay;

    private bool isIntensityIncreasing = true;
    private bool stopPulsing = false;
    private bool didWin = false;
    private bool hasColorChanged = false;

    void Update()
    {
        if (stopPulsing && pointLight.intensity >= intensityMax)
        {
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

    public void ShowEndLevel(bool _didWin)
    {
        stopPulsing = true;
        didWin = _didWin;
    }

    private IEnumerator ChangeColor()
    {
        yield return new WaitForSeconds(colorChangeDelay);

        Color startColor = pointLight.color;
        float elapsedTime = 0.0f;
        float totalTime = 1.0f;

        while (elapsedTime < totalTime)
        {
            elapsedTime += Time.deltaTime;
            pointLight.color = Color.Lerp(startColor, winColor, (elapsedTime / totalTime));
            yield return null;
        }
    }
}
