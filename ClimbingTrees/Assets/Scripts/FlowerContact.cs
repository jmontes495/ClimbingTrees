using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerContact : MonoBehaviour
{

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "Player")
            return;

        audioSource.Play();
    }
}
