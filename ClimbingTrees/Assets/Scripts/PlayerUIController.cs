using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI fruitCounter;

    private int fruitsCollected = 0;

    private int totalFruits;

    private void Awake()
    {
        FruitCollector.FruitCollected += IncreaseFruitCounter;
        FruitCollector.FruitCreated += AddToTotalFruits;
    }

    private void AddToTotalFruits()
    {
        totalFruits++;
        fruitCounter.text = fruitsCollected + " / " + totalFruits;
    }

    private void IncreaseFruitCounter()
    {
        fruitsCollected++;
        fruitCounter.text = fruitsCollected + " / " + totalFruits;
    }
}
