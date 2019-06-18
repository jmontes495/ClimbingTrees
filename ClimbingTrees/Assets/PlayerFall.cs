using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFall : MonoBehaviour
{
    public delegate void FallingEvent();
    public static event FallingEvent PlayerReachedGound;
    public static event FallingEvent PlayerStandingUp;

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

    [SerializeField]
    private bool isStandingUp;

    private Transform myTransform;

    private Quaternion initialRotation;

    private void Start()
    {
        myTransform = transform;
        initialRotation = myTransform.rotation;
        PlayerBalance.PlayerFellFromBranch += TurnAndFall;
    }

    private void TurnAndFall()
    {
		GetComponent<Rigidbody>().isKinematic = true;
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		GetComponent<Rigidbody>().angularVelocity = Vector3.zero; 
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
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, finalRotation, Time.deltaTime * 10f);
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
        PlayerStandingUp();
        isStandingUp = true;
        StartCoroutine(ForceStandUp());
        WaitForSeconds delay = new WaitForSeconds(delayStandingUp);
        Quaternion finalRotation = initialRotation;
        while (thereIsDifferenceQuaternions(myTransform.rotation, finalRotation))
        {
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, finalRotation, Time.deltaTime * 10f);
            yield return delay;
        }
        StopCoroutine(ForceStandUp());
		FinishUpStanding();
    }

    private IEnumerator ForceStandUp()
    {
        yield return new WaitForSeconds(4f);
        if (isStandingUp)
        {
            StopCoroutine(StandUp());
			FinishUpStanding();
        }
    }

    private void FinishUpStanding()
	{
		myTransform.localRotation = initialRotation;
        Input.ResetInputAxes();
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        GetComponent<Rigidbody>().isKinematic = false;
        isStandingUp = false;
        PlayerReachedGound();
	}

    private bool thereIsDifferenceQuaternions(Quaternion q1, Quaternion q2)
    {
        float threshold = 0.01f;
        return Quaternion.Angle(q1, q2) > threshold;
    }

    private bool thereIsDifferenceVector(Vector3 q1, Vector3 q2)
    {
        float threshold = 0.01f;
        return Mathf.Abs(q1.x - q2.x) > threshold || Mathf.Abs(q1.y - q2.y) > threshold || Mathf.Abs(q1.z - q2.z) > threshold;
    }
}
