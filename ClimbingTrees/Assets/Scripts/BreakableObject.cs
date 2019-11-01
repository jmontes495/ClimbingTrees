using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField]
    protected int requiredHits;

    protected BoxCollider collider;
    protected int numberOfHits;

    void Start()
    {
        collider = GetComponent<BoxCollider>();   
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Hammer"))
            return;

        numberOfHits++;
        if (requiredHits <= numberOfHits)
            DestroyBreakableObject();
    }

    public virtual void DestroyBreakableObject()
    {
        Destroy(gameObject);
    }
}
