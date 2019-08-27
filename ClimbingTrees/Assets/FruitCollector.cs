using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitCollector : MonoBehaviour
{
    public delegate void FruitActions();
    public static event FruitActions FruitCollected;
    public static event FruitActions FruitCreated;

    private void Start()
    {
        FruitCreated();
    }

    private void OnTriggerEnter(Collider collision)
    {
        FruitCollected();
        Destroy(gameObject);
    }
}
