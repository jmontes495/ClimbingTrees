using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitCollector : MonoBehaviour
{
    public delegate void FruitActions();
    public static event FruitActions FruitCollected;

    private void OnTriggerEnter(Collider collision)
    {
        FruitCollected();
        Destroy(gameObject);
    }
}
