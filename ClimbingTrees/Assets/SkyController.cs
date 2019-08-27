using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyController : MonoBehaviour
{
    private Camera skyCamera;

    private float fruitsCollected = 0;

    private float totalFruits;

    private void Awake()
    {
        FruitCollector.FruitCollected += IncreaseFruitCounter;
        FruitCollector.FruitCreated += AddToTotalFruits;
        skyCamera = GetComponent<Camera>();

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

        skyCamera.backgroundColor = new Color( 0.8f - (fruitsCollected / totalFruits), 1 - (fruitsCollected / totalFruits), 1 - (fruitsCollected / totalFruits)*0.7f, 0);
    }
}
