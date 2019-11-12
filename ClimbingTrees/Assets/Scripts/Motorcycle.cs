using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motorcycle : BreakableObject
{
    private Rigidbody[] parts;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        parts = GetComponentsInChildren<Rigidbody>();
    }

    public override void DestroyBreakableObject()
    {
        audioSource.PlayOneShot(wreck);
        bodyCollider.enabled = false;
        foreach(Rigidbody part in parts)
        {
            part.isKinematic = false;
            part.useGravity = true;
        }
    }
}
