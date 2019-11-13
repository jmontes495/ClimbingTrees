using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField]
    protected int requiredHits;
    [SerializeField]
    protected AudioClip hit;
    [SerializeField]
    protected AudioClip wreck;
    [SerializeField]
    protected BoxCollider bodyCollider;

    protected int numberOfHits;
    protected AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (requiredHits <= numberOfHits)
            return;
        
        if (other.gameObject.layer != LayerMask.NameToLayer("Hammer"))
            return;

        if (other.GetComponent<Hammer>() != null)
            other.GetComponent<Hammer>().ShowHammerHit();

        numberOfHits++;
        if (requiredHits <= numberOfHits)
            DestroyBreakableObject();
        else
            PlayHitSound();
    }

    public virtual void DestroyBreakableObject()
    {
        audioSource.PlayOneShot(wreck);
        bodyCollider.enabled = false;
        Destroy(gameObject);
    }

    public virtual void PlayHitSound()
    {
        audioSource.PlayOneShot(hit);
    }
}
