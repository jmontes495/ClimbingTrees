using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private float speed;

    private Transform myTransform;

    private Rigidbody rb;

    private void Start()
    {
        Cursor.visible = false;
        myTransform = transform;
        rb = GetComponent<Rigidbody>();
        GraspManager.PlayerIsOnBranch += ChangeBalance;
        PlayerFall.PlayerReachedGound += ChangeGround;
		GraspManager.PlayerReachedGound += ChangeGround;
        StartCoroutine(Walking());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Cursor.visible = true;

        if (rb.velocity != Vector3.zero)
            rb.velocity = Vector3.zero;
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
        while (!InputKeysManager.Instance.IsBalancing && !InputKeysManager.Instance.IsFalling)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            //myTransform.Translate(movement * speed * Time.deltaTime);
            rb.MovePosition(transform.position + transform.TransformDirection(movement) * Time.deltaTime * speed);

            yield return delay;
        }
    }
}
