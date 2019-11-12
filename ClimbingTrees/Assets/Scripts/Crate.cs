using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : BreakableObject
{
    [SerializeField]
    private BoxCollider fruitCollider;

    [SerializeField]
    private GameObject finalFruit;

    [SerializeField]
    private GameObject crate;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        finalFruit.gameObject.SetActive(false);
        fruitCollider.enabled = false;
    }

    public override void DestroyBreakableObject()
    {
        finalFruit.gameObject.SetActive(true);
        crate.gameObject.SetActive(false);
        fruitCollider.enabled = true;
        audioSource.PlayOneShot(wreck);
        bodyCollider.enabled = false;
    }
}
