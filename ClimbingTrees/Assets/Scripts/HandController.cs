﻿using System.Collections;
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

    private bool isReturning;

    private float extendSpeed = 3f;

    private Transform originalParent;

    [SerializeField]
    private Transform otherParent;

    [SerializeField]
    private GraspManager graspManager;

    [SerializeField]
    private Animator handAnimator;

    [SerializeField]
    private ParticleSystem particleSystem;


    private bool lockedToObject;

    private Quaternion originalRotation;

    void Start () {
        GraspManager.PlayerTeleported += PopHandsToOGPosition;
        myTransform = transform;
        isExtended = false;
        lockedToObject = false;
        originalParent = myTransform.parent;
        originalRotation = myTransform.localRotation;
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
        handAnimator.SetBool("inBranch", false);
        myTransform.parent = originalParent;
        StartCoroutine(ReturnHand());
    }

    private void PopHandsToOGPosition()
    {
        StopAllCoroutines();
        lockedToObject = false;
        isExtending = false;
        isExtended = false;
        isReturning = false;
        graspManager.ChangeGraspObject(null, typeOfHand);
        handAnimator.SetBool("inBranch", false);
        myTransform.parent = originalParent;
        myTransform.position = initialTransform.position;
        myTransform.localRotation = originalRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (InputKeysManager.Instance.IsFalling || !isExtending || isReturning)
            return;

        GrabbableObject contactObject = other.GetComponent<GrabbableObject>();

        if (contactObject == null)
            return;

        if (contactObject.GetComponent<TreeBranchBehaviour>() != null && contactObject.GetComponent<TreeBranchBehaviour>().IsCurrentBranch)
            return;
        

        contactObject.UpdateColorUp();

        StopAllCoroutines();
        lockedToObject = true;
        isExtending = false;
        isExtended = true;

        graspManager.ChangeGraspObject(other.gameObject, typeOfHand);
        handAnimator.SetBool("inBranch", true);
        particleSystem.Play();


        if (!contactObject.IsStaticObject())
            graspManager.ChangeObjectInHands(contactObject, this.transform);
        else
            myTransform.parent = otherParent;
        
    }

    private void OnTriggerExit(Collider other)
    {
        GrabbableObject contactObject = other.GetComponent<GrabbableObject>();

        if (contactObject == null || !isReturning)
            return;

        contactObject.UpdateColorDown();
        graspManager.ChangeGraspObject(null, typeOfHand);
        handAnimator.SetBool("inBranch", false);

        graspManager.ClearObjectInHands();
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
        isReturning = true;
        while (myTransform.position != initialTransform.position)
        {
            myTransform.position = Vector3.MoveTowards(myTransform.position, initialTransform.position, Time.fixedDeltaTime * extendSpeed);
            yield return new WaitForFixedUpdate();
        }
        myTransform.localRotation = originalRotation;
        isReturning = false;
        isExtended = false;
    }


}
