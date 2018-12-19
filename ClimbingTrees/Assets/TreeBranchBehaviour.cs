using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBranchBehaviour : MonoBehaviour
{

    MeshRenderer materialRenderer;

    [SerializeField]
    private Transform teleportPosition;
    // Use this for initialization
    void Start()
    {
        materialRenderer = GetComponent<MeshRenderer>();
        materialRenderer.material.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider collision)
    {
        materialRenderer.material.color = Color.yellow;
    }

    private void OnTriggerExit(Collider collision)
    {
        materialRenderer.material.color = Color.white;
    }

    public Vector3 GetTeleportPosition()
    {
        return teleportPosition.position;
    }
}
