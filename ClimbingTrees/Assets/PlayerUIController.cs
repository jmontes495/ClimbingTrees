using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI fruitCounter;

    private int fruitsCollected = 0;

    void Start()
    {
        FruitCollector.FruitCollected += IncreaseFruitCounter;
    }

    private void IncreaseFruitCounter()
    {
        fruitsCollected++;
        fruitCounter.text = "x" + fruitsCollected;
    }
}
