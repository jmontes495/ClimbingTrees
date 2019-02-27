using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFall : MonoBehaviour
{
    public delegate void FallingEvent();
    public static event FallingEvent PlayerReachedGound;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float delayTurningToGround;

    [SerializeField]
    private float delayStandingUp;

    [SerializeField]
    private float fallingSpeed;

    [SerializeField]
    private float delayBetweenTurningAndFalling;

    private Transform myTransform;

    private void Start()
    {
        myTransform = transform;
        PlayerBalance.PlayerFellFromBranch += TurnAndFall;
    }

    private void TurnAndFall()
    {
        StartCoroutine(TurnToGround());
        StartCoroutine(FallIntoGround());
    }

    private IEnumerator TurnToGround()
    {
        WaitForSeconds delay = new WaitForSeconds(delayTurningToGround);
        Vector3 direction = target.position - myTransform.position;
        Quaternion finalRotation = Quaternion.LookRotation(direction);
        while (thereIsDifferenceQuaternions(myTransform.rotation, finalRotation))
        {
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, finalRotation, 1 / 5f);
            yield return delay;
        }        
    }

    private IEnumerator FallIntoGround()
    {
        yield return new WaitForSeconds(delayBetweenTurningAndFalling);
        WaitForFixedUpdate delay = new WaitForFixedUpdate();
        Vector3 finalPosition = target.position;
        finalPosition.y += 1;
        while (thereIsDifferenceVector(finalPosition, myTransform.position))
        {
            myTransform.position = Vector3.MoveTowards(myTransform.position, finalPosition, fallingSpeed * Time.deltaTime);
            yield return delay;
        }
        StartCoroutine(StandUp());
    }

    private IEnumerator StandUp()
    {
        WaitForSeconds delay = new WaitForSeconds(delayStandingUp);
        Quaternion finalRotation = myTransform.rotation;
        finalRotation.x = 0;
        finalRotation.z = 0;
        while (thereIsDifferenceQuaternions(myTransform.rotation, finalRotation))
        {
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, finalRotation, 1 / 5f);
            yield return delay;
        }
        Input.ResetInputAxes();

        PlayerReachedGound();
    }

    private bool thereIsDifferenceQuaternions(Quaternion q1, Quaternion q2)
    {
        float threshold = 0.1f;
        return Mathf.Abs(q1.x - q2.x) > threshold || Mathf.Abs(q1.y - q2.y) > threshold || Mathf.Abs(q1.z - q2.z) > threshold;
    }

    private bool thereIsDifferenceVector(Vector3 q1, Vector3 q2)
    {
        float threshold = 0.01f;
        return Mathf.Abs(q1.x - q2.x) > threshold || Mathf.Abs(q1.y - q2.y) > threshold || Mathf.Abs(q1.z - q2.z) > threshold;
    }
}
