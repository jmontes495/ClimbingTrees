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
    private float delayTurningToGround;

    [SerializeField]
    private float balanceLimit;

    [SerializeField]
    private float initialStrengthOfInclination;

    [SerializeField]
    private Transform target;

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
        StartCoroutine(TurnToGround());
    }

    private IEnumerator TurnToGround()
    {
        WaitForSeconds delay = new WaitForSeconds(delayTurningToGround);
        Vector3 direction = target.position - myTransform.position;
        Quaternion finalRotation = Quaternion.LookRotation(direction);
        while (thereIsDifference(myTransform.rotation, finalRotation))
        {
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, finalRotation, 1 / 5f);
            yield return delay;
        }
    }

    private bool thereIsDifference(Quaternion q1, Quaternion q2)
    {
        float threshold = 0.001f;
        return Mathf.Abs(q1.x - q2.x) > threshold || Mathf.Abs(q1.y - q2.y) > threshold || Mathf.Abs(q1.z - q2.z) > threshold;
    }
}
