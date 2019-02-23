using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerFromGround : MonoBehaviour
{

    [SerializeField]
    private Transform theGround;

    [SerializeField]
    private Transform player;

    void Update()
    {
        Vector3 currentPosition = player.position;
        currentPosition.y = theGround.position.y;
        transform.position = currentPosition;
    }
}
