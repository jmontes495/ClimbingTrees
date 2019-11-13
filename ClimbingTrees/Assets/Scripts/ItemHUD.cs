using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHUD : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Sprite itemSprite;

    private GameObject player;
    private float distanceLimit = 20f;
    private float minimumSpace = 3f;

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = itemSprite;
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        AdjustSizeAndDelta(player.transform.position);
    }

    private void AdjustSizeAndDelta(Vector3 position)
    {
        float distance = Vector3.Distance(position, spriteRenderer.transform.position);
        float proportion = 1f;

        if (distance < distanceLimit)
            proportion = distance / distanceLimit;

        if (distance < minimumSpace)
            proportion = 0;

        spriteRenderer.transform.localScale = new Vector3(proportion, proportion, proportion);
        spriteRenderer.color = new Color(1f, 1f, 1f, proportion);
    }

}
