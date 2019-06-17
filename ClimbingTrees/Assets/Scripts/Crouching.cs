using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouching : MonoBehaviour
{

	[SerializeField]
    private Transform initialTransform;

	[SerializeField]
    private Transform crouchingTransform;

	private Transform myTransform;

	private float extendSpeed = 10f;

	void Start()
    {
        myTransform = transform;
    }

    public void InputCrouch()
	{
		StopAllCoroutines();
		StartCoroutine(Crouch());
	}

	public void InputStandUp()
    {
        StopAllCoroutines();
        StartCoroutine(StandUp());
    }

	private IEnumerator Crouch()
    {
		Transform targetTransform = crouchingTransform;
        while (myTransform.position != targetTransform.position)
        {
            myTransform.position = Vector3.MoveTowards(myTransform.position, targetTransform.position, Time.fixedDeltaTime * extendSpeed);
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator StandUp()
    {
        Transform targetTransform = initialTransform;
        while (myTransform.position != targetTransform.position)
        {
            myTransform.position = Vector3.MoveTowards(myTransform.position, targetTransform.position, Time.fixedDeltaTime * extendSpeed);
            yield return new WaitForFixedUpdate();
        }
    }
}
