using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    [SerializeField]
    protected MeshRenderer materialRenderer;

    [SerializeField]
    protected bool requiresBothHands;

    public bool RequiresBothHands
    {
        get { return requiresBothHands; }
        private set { requiresBothHands = value; }
    }

    protected Color color;

    private Transform myTransform;

    private Rigidbody rigidbody;

    [SerializeField]
    private float throwSpeed;

    void Start()
    {
        color = materialRenderer.material.color;
        myTransform = gameObject.transform;
        rigidbody = GetComponent<Rigidbody>();
    }

    public virtual void UpdateColorUp()
    {
        if (InputKeysManager.Instance.IsFalling)
            return;

        if (materialRenderer.material.color == Color.yellow)
            materialRenderer.material.color = Color.green;
        else
            materialRenderer.material.color = Color.yellow;
    }

    public virtual void UpdateColorDown()
    {
        if (InputKeysManager.Instance.IsFalling)
            return;

        if (materialRenderer.material.color == Color.yellow)
            materialRenderer.material.color = color;
        else
            materialRenderer.material.color = Color.yellow;
    }

    public virtual bool IsStaticObject()
    {
        return false;
    }

    public virtual void ChangeParent(Transform parent)
    {
        myTransform.parent = parent;
        rigidbody.useGravity = parent == null;
        rigidbody.isKinematic = parent != null;
        if (parent != null)
            rigidbody.velocity = Vector3.zero;
    }

    public void AddForceInDirection(Vector3 direction)
    {
        rigidbody.AddForce(direction * throwSpeed);
    }
}
