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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            IncreaseFruitCounter();
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
        if (target < 0.5f)
        {
            float colorPosition = fruitsCollected / (totalFruits / 2);
            targetColor = calculateAverageColor(colorPosition, morningColor, afternoonColor);
        }
        else
        {
            float colorPosition = (fruitsCollected - (totalFruits / 2)) / (totalFruits / 2);
            targetColor = calculateAverageColor(colorPosition, afternoonColor, nightColor);
        }
        sunlight.color = targetColor;
    }

    private Color calculateAverageColor(float colorPosition, Color color1, Color color2)
    {
        float r = (color1.r * (1 - colorPosition) + color2.r * colorPosition);
        float g = (color1.g * (1 - colorPosition) + color2.g * colorPosition);
        float b = (color1.b * (1 - colorPosition) + color2.b * colorPosition);

        return new Color(r, g, b, 1);
    }

    private void OnDestroy()
    {
        RenderSettings.skybox.SetFloat("_Blend", 0);
    }
}
