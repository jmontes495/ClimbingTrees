using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    private Rigidbody rigidbody;

    [SerializeField]
    private BoxCollider fruitCollider;

    [SerializeField]
    private GameObject finalFruit;

    [SerializeField]
    private GameObject bucket;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        finalFruit.gameObject.SetActive(false);
        fruitCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("GrabbableObject"))
        {
            rigidbody.useGravity = true;
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;
            finalFruit.gameObject.SetActive(true);
            bucket.gameObject.SetActive(false);
            fruitCollider.enabled = true;
        }
    }
}
