using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBalance : MonoBehaviour
{
    private float speed = 10.0f;

    private float strength = 1f;

    private Transform myTransform;

    private bool isBalancing;

    private void Start()
    {
        myTransform = transform;
        isBalancing = true;
        GraspManager.PlayerIsOnBranch += ChangeBalance;
        GraspManager.PlayerIsOnGround += ChangeGround;
    }

    void Update()
    {
        if(isBalancing)
        {
            float translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            float straffe = Input.GetAxis("Horizontal") * -strength;

            //myTransform.Translate(0, 0, translation);
            //rb.AddForce(new Vector3(straffe, 0, 0));
            myTransform.Rotate(0, 0, straffe, 0);
        }
    }

    private void ChangeBalance()
    {
        isBalancing = true;
        //rb.AddForce(new Vector3(strength/2, 0, 0));
        //myTransform.Rotate(0, 0, 0.5f);
    }

    private void ChangeGround()
    {
        isBalancing = false;
    }
}
