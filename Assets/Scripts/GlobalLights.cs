using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GlobalLights : MonoBehaviour
{
    public float currentIntensity = 1.0f;
    public Color currentColor = Color.white;

    public float targetIntensity = 1.0f;

    public Color targetColor = Color.white;

    public float changeRate = 0.1f;

    public static GlobalLights instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // Slowly change the intensity of the light
        if (currentIntensity < targetIntensity) {
            currentIntensity += changeRate * Time.deltaTime;
        } else if (currentIntensity > targetIntensity) {
            currentIntensity -= changeRate * Time.deltaTime;
        }
        // Slowly change the color of the light
        if (currentColor.r < targetColor.r) {
            currentColor.r += changeRate * Time.deltaTime;
        } else if (currentColor.r > targetColor.r) {
            currentColor.r -= changeRate * Time.deltaTime;
        }
        if (currentColor.g < targetColor.g) {
            currentColor.g += changeRate * Time.deltaTime;
        } else if (currentColor.g > targetColor.g) {
            currentColor.g -= changeRate * Time.deltaTime;
        }
        if (currentColor.b < targetColor.b) {
            currentColor.b += changeRate * Time.deltaTime;
        } else if (currentColor.b > targetColor.b) {
            currentColor.b -= changeRate * Time.deltaTime;
        }

        GetComponent<Light2D>().intensity = currentIntensity;
        GetComponent<Light2D>().color = currentColor;
    }

    public void SetIntensity(float intensity) {
        targetIntensity = intensity;
    }

    public void SetColor(Color color) {
        targetColor = color;
    }
}
