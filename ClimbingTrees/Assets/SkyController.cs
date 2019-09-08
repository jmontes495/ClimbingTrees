using System.Collections;
using UnityEngine;

public class SkyController : MonoBehaviour
{
    [SerializeField]
    private Light sunlight;

    private float fruitsCollected = 0;

    private float totalFruits;

    private float skyboxBlendSpeed = 0.05f;

    private float skyboxBlendFactor = 0;

    [SerializeField]
    private Color sunriseColor;

    [SerializeField]
    private Color morningColor;

    [SerializeField]
    private Color afternoonColor;

    [SerializeField]
    private Color nightColor;

    private void Awake()
    {
        FruitCollector.FruitCollected += IncreaseFruitCounter;
        FruitCollector.FruitCreated += AddToTotalFruits;
    }

    private void Start()
    {
        AdaptSky();
    }

    private void AddToTotalFruits()
    {
        totalFruits++;
    }

    private void IncreaseFruitCounter()
    {
        fruitsCollected++;
        AdaptSky();
    }

    private void AdaptSky()
    {
        if (fruitsCollected >= totalFruits)
            return;

        StartCoroutine(BlendSky());
        BlendSun();
    }

    private IEnumerator BlendSky()
    {
        float target = (fruitsCollected / totalFruits);

        while (skyboxBlendFactor < target)
        {
            skyboxBlendFactor += skyboxBlendSpeed * Time.deltaTime;
            RenderSettings.skybox.SetFloat("_Blend", skyboxBlendFactor);
            yield return new WaitForEndOfFrame();
        }
    }

    private void BlendSun()
    {
        Color currentColor = sunlight.color;
        Color targetColor = currentColor;

        float target = (fruitsCollected / totalFruits);

        if (target <= 0.33f)
            targetColor = calculateAverageColor(currentColor, morningColor);
        else if (target <= 0.66f)
            targetColor = calculateAverageColor(currentColor, afternoonColor);
        else
            targetColor = calculateAverageColor(currentColor, nightColor);

        sunlight.color = targetColor;
    }

    private Color calculateAverageColor(Color color1, Color color2)
    {
        float r = (color1.r + color2.r) / 2;
        float g = (color1.g + color2.g) / 2;
        float b = (color1.b + color2.b) / 2;
        float a = (color1.a + color2.a) / 2;

        return new Color(r, g, b, a);
    }

    private void OnDestroy()
    {
        RenderSettings.skybox.SetFloat("_Blend", 0);
    }
}
