using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private float speed = 10.0f;

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
            float translation = -Input.GetAxis("Axis 2") * speed * Time.deltaTime;
            float straffe = Input.GetAxis("Axis 1") * speed * Time.deltaTime;

            myTransform.Translate(straffe, 0, translation);

            yield return delay;
        }
    }
}
