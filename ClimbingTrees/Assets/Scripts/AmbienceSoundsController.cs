using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceSoundsController : MonoBehaviour
{
    [SerializeField]
    private AudioSource day;

    [SerializeField]
    private AudioSource night;

    private float fruitsCollected = 0;

    private float totalFruits;

    private void Awake()
    {
        FruitCollector.FruitCollected += IncreaseFruitCounter;
        FruitCollector.FruitCreated += AddToTotalFruits;
    }

    private void AddToTotalFruits()
    {
        totalFruits++;
    }

    private void IncreaseFruitCounter()
    {
        fruitsCollected++;
        AdaptAmbience();
    }

    private void AdaptAmbience()
    {
        if (fruitsCollected > totalFruits)
            return;

        float target = (fruitsCollected / totalFruits);

        day.volume = 1 - target;
        night.volume = target;
    }
}
