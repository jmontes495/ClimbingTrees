﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private float speed;

    private Transform myTransform;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        myTransform = transform;
        GraspManager.PlayerIsOnBranch += ChangeBalance;
        GraspManager.PlayerIsOnGround += ChangeGround;
        StartCoroutine(Walking());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Cursor.lockState = CursorLockMode.None;
    }

    private void ChangeBalance()
    {
        StopCoroutine(Walking());
    }

    private void ChangeGround()
    {
        StartCoroutine(Walking());
    }

    private IEnumerator Walking()
    {
        WaitForFixedUpdate delay = new WaitForFixedUpdate();
        while (InputKeysManager.Instance.IsBalancing)
            yield return new WaitForEndOfFrame();
        while (!InputKeysManager.Instance.IsBalancing)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            myTransform.Translate(movement * speed * Time.deltaTime);

            yield return delay;
        }
    }
}
