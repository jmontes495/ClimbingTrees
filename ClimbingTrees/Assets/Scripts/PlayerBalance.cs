using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBalance : MonoBehaviour
{
    public delegate void BalanceEvent();
    public static event BalanceEvent PlayerFellFromBranch;

    [SerializeField]
    private float walkingSpeed;

    private float strength = 1f;

    private Transform myTransform;

    private float currentInclination;

    [SerializeField]
    private float acceleration;

    [SerializeField]
    private float delayInclination;

    [SerializeField]
    private float balanceLimit;

    [SerializeField]
    private float initialStrengthOfInclination;


    private void Start()
    {
        myTransform = transform;
        GraspManager.PlayerIsOnBranch += ChangeBalance;
    }

    private void ChangeBalance()
    {
        StartCoroutine(ApplyForce());
    }

    private IEnumerator ApplyForce()
    {
        currentInclination = 0;
        WaitForSeconds delay = new WaitForSeconds(delayInclination);
        while (!InputKeysManager.Instance.IsBalancing)
            yield return new WaitForEndOfFrame();

        if (myTransform.rotation.y < 0)
            currentInclination = initialStrengthOfInclination;
        else
            currentInclination = -initialStrengthOfInclination;

        while (InputKeysManager.Instance.IsBalancing && currentInclination < balanceLimit && currentInclination > -balanceLimit)
        {
            float translation = -Input.GetAxis("Vertical") * walkingSpeed * Time.deltaTime;
            float straff = Input.GetAxis("Horizontal") * -strength;

            if (myTransform.rotation.y > 0.5f || myTransform.rotation.y < -0.5f)
                straff = -straff;

            myTransform.Translate(0, 0, -translation);
            currentInclination = acceleration*currentInclination + straff;
            myTransform.Rotate(0, 0, currentInclination, 0);
            
            yield return delay;
        }
        PlayerFellFromBranch();
    }

    
}
