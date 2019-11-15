using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerContact : MonoBehaviour
{
    public delegate void PetalActions();
    public static event PetalActions FlowerCollided;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "Player")
            return;

        FlowerCollided();
        audioSource.Play();
    }
}
