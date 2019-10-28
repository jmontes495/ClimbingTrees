using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour {

    public enum TypeOfHand { Left, Right };

    [SerializeField]
    private TypeOfHand typeOfHand;

    [SerializeField]
    private Transform targetTransform;

    [SerializeField]
    private Transform initialTransform;

	[SerializeField]
    private Transform crouchingTransform;

    private Transform myTransform;

    private bool isExtended;

    private bool isExtending;

    private float extendSpeed = 3f;

    private Transform originalParent;

    [SerializeField]
    private Transform otherParent;

    [SerializeField]
    private GraspManager graspManager;

    private bool lockedToObject;

    void Start () {
        GraspManager.PlayerTeleported += PopHandsToOGPosition;
        myTransform = transform;
        isExtended = false;
        lockedToObject = false;
        originalParent = myTransform.parent;
	}
	
	public void InputExtendHand()
    {
        StopAllCoroutines();
        StartCoroutine(ExtendHand());
    }

    public void InputDropHand()
    {
        ReturnHandsToOGPosition();
    }  

    public void CheckCrouching()
	{
		if (isExtended)
			InputExtendHand();
	}

    private void ReturnHandsToOGPosition()
    {
        StopAllCoroutines();
        lockedToObject = false;
        isExtending = false;
        isExtended = false;
        graspManager.ChangeGraspObject(null, typeOfHand);
        myTransform.parent = originalParent;
        StartCoroutine(ReturnHand());
    }

    private void PopHandsToOGPosition()
    {
        StopAllCoroutines();
        lockedToObject = false;
        isExtending = false;
        isExtended = false;
        graspManager.ChangeGraspObject(null, typeOfHand);
        myTransform.parent = originalParent;
        myTransform.position = initialTransform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (InputKeysManager.Instance.IsFalling || !isExtending)
            return;

        GrabbableObject contactObject = other.GetComponent<GrabbableObject>();
        if (contactObject == null)
            return;

        lockedToObject = true;
        isExtending = false;
        isExtended = true;
        StopAllCoroutines();

        if (!contactObject.IsStaticObject())
            contactObject.transform.parent = myTransform;
        else
            myTransform.parent = otherParent;
        
        graspManager.ChangeGraspObject(other.gameObject, typeOfHand);
    }

    private IEnumerator ExtendHand()
    {
		isExtending = true;
		Transform objectiveTransform = InputKeysManager.Instance.IsCrouching? crouchingTransform : targetTransform;
		while(myTransform.position != objectiveTransform.position)
        {
			myTransform.position = Vector3.MoveTowards(myTransform.position, objectiveTransform.position, Time.fixedDeltaTime * extendSpeed);
            yield return new WaitForFixedUpdate();
        }
        isExtending = false;
        isExtended = true;
    }

    private IEnumerator ReturnHand()
    {
        isExtending = true;
        while (myTransform.position != initialTransform.position)
        {
            myTransform.position = Vector3.MoveTowards(myTransform.position, initialTransform.position, Time.fixedDeltaTime * extendSpeed);
            yield return new WaitForFixedUpdate();
        }
        isExtending = false;
        isExtended = false;
    }


}
