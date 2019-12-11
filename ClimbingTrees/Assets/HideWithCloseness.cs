using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideWithCloseness : MonoBehaviour
{
    [SerializeField]
    private Material opaque;
    [SerializeField]
    private Material transparent;

    private MeshRenderer meshRenderer;

    private GameObject player;
    private float distanceLimit = 10f;
    private float minimumSpace = 5f;
    private float minimumDelta = 0.1f;
    private float green;
    private float red;
    private float blue;
    private bool isTransparent;

    void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        player = GameObject.Find("Player");
        green = meshRenderer.material.color.g;
        blue = meshRenderer.material.color.b;
        red = meshRenderer.material.color.r;
    }

    private void Update()
    {
        AdjustDelta(player.transform.position);
    }

    private void AdjustDelta(Vector3 position)
    {
        float distance = Vector3.Distance(position, meshRenderer.transform.position);
        float proportion = 1f;

        if (distance < distanceLimit)
            proportion = distance / distanceLimit;

        if (distance < minimumSpace || proportion < minimumDelta)
            proportion = minimumDelta;

        if (proportion >= 1f && isTransparent)
        {
            meshRenderer.material = opaque;
            isTransparent = false;
        }
        else if (proportion < 1f && !isTransparent)
        {
            meshRenderer.material = transparent;
            isTransparent = true;
        }

        Debug.LogError(proportion);

        meshRenderer.material.color = new Color(red, green, blue, proportion);
    }
}
