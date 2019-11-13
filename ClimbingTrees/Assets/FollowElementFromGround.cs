using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowElementFromGround : MonoBehaviour
{
    [SerializeField]
    private Transform theGround;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private bool includeHeight;

    void Update()
    {
        Vector3 currentPosition = player.position;
        if(!includeHeight)
            currentPosition.y = theGround.position.y;
        transform.position = currentPosition;
    }
}
