using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBalance : MonoBehaviour
{
    private float speed = 10.0f;

    private float strength = 1f;

    private Transform myTransform;

    private bool isBalancing;

    private float currentInclination = 0.1f;

    private float acceleration = 1.025f;

    private float delayInclination = 0.05f;

    private float balanceLimit = 0.1f;

    private void Start()
    {
        myTransform = transform;
        isBalancing = false;
        GraspManager.PlayerIsOnBranch += ChangeBalance;
        GraspManager.PlayerIsOnGround += ChangeGround;
    }

    void Update()
    {
        
    }

    private void ChangeBalance()
    {
        isBalancing = true;
        StartCoroutine(ApplyForce());
    }

    private void ChangeGround()
    {
        isBalancing = false;
        StopCoroutine(ApplyForce());
    }

    private IEnumerator ApplyForce()
    {
        WaitForSeconds delay = new WaitForSeconds(delayInclination);
        while(isBalancing && myTransform.rotation.z < balanceLimit && myTransform.rotation.z > -balanceLimit)
        {
            float translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            float straffe = Input.GetAxis("Horizontal") * -strength;

            myTransform.Translate(0, 0, translation);
            currentInclination = acceleration*currentInclination + straffe;
            myTransform.Rotate(0, 0, currentInclination, 0);
            
            Debug.Log(myTransform.rotation.z);
            yield return delay;
        }        
    }
}
