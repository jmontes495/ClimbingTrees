using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    [SerializeField]
    protected MeshRenderer materialRenderer;

    protected Color color;

    void Start()
    {
        color = materialRenderer.material.color;
    }

    protected virtual void OnTriggerEnter(Collider collision)
    {
        if (InputKeysManager.Instance.IsFalling)
            return;

        if (materialRenderer.material.color == Color.yellow)
            materialRenderer.material.color = Color.green;
        else
            materialRenderer.material.color = Color.yellow;
    }

    protected virtual void OnTriggerExit(Collider collision)
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
}
