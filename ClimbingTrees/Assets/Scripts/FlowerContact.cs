using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerContact : MonoBehaviour
{

    private AudioSource audioSource;
    private ParticleSystem particleSystem;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "Player")
            return;

        audioSource.Play();
        particleSystem.Play();
    }
}
