using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBranchBehaviour : MonoBehaviour
{

    MeshRenderer materialRenderer;
    
    private Transform teleportPosition;

    private Color color;
    // Use this for initialization
    void Start()
    {
        materialRenderer = GetComponent<MeshRenderer>();
        color = materialRenderer.material.color;
        teleportPosition = transform;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (InputKeysManager.Instance.IsFalling)
            return;

        if (materialRenderer.material.color == Color.yellow)
            materialRenderer.material.color = Color.green;
        else
            materialRenderer.material.color = Color.yellow;
    }

    private void OnTriggerExit(Collider collision)
    {
        if (InputKeysManager.Instance.IsFalling)
            return;

        if (materialRenderer.material.color == Color.yellow)
            materialRenderer.material.color = color;
        else
            materialRenderer.material.color = Color.yellow;
    }

    public Vector3 GetTeleportPosition()
    {
        return teleportPosition.position;
    }
}
