using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyController : MonoBehaviour
{
    private float fruitsCollected = 0;

    private float totalFruits;

    private float skyboxBlendSpeed = 0.05f;

    private float skyboxBlendFactor = 0;

    private void Awake()
    {
        FruitCollector.FruitCollected += IncreaseFruitCounter;
        FruitCollector.FruitCreated += AddToTotalFruits;
        RenderSettings.skybox.SetFloat("_Blend", 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
            IncreaseFruitCounter();
    }

    private void AddToTotalFruits()
    {
        totalFruits++;
        AdaptSky();
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
}
