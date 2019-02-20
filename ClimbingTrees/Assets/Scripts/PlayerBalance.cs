using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBalance : MonoBehaviour
{
    [SerializeField]
    private float walkingSpped;

    private float strength = 1f;

    private Transform myTransform;

    private float currentInclination;

    [SerializeField]
    private float acceleration;

    [SerializeField]
    private float delayInclination;

    [SerializeField]
    private float balanceLimit;

    private void Start()
    {
        myTransform = transform;
        GraspManager.PlayerIsOnBranch += ChangeBalance;
        GraspManager.PlayerIsOnGround += ChangeGround;
    }

    private void ChangeBalance()
    {
        StartCoroutine(ApplyForce());
    }

    private void ChangeGround()
    {
        StopCoroutine(ApplyForce());
    }

    private IEnumerator ApplyForce()
    {
        WaitForSeconds delay = new WaitForSeconds(delayInclination);
        while (!InputKeysManager.Instance.IsBalancing)
            yield return new WaitForEndOfFrame();
        
        while (InputKeysManager.Instance.IsBalancing && myTransform.rotation.z < balanceLimit && myTransform.rotation.z > -balanceLimit)
        {
            float translation = -Input.GetAxis("Axis 2") * walkingSpped * Time.deltaTime;
            float straffe = Input.GetAxis("Axis 1") * -strength;

            myTransform.Translate(0, 0, translation);
            currentInclination = acceleration*currentInclination + straffe;
            myTransform.Rotate(0, 0, currentInclination, 0);
            
            yield return delay;
        }        
    }
}
