﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour, IListener {

    public enum TypeOfHand { Left, Right };

    [SerializeField]
    private TypeOfHand typeOfHand;

    [SerializeField]
    private Transform targetTransform;

    [SerializeField]
    private Transform initialTransform;

    private Transform myTransform;

    private KeyCode myKeyCode;

    private bool isExtended;

    private bool isExtending;

    private float extendSpeed = 10f;

    private Transform originalParent;

    [SerializeField]
    private Transform otherParent;

    [SerializeField]
    private GraspManager graspManager;

    private bool lockedToBranch;

    void Start () {

        EventManager.Instance.AddListener(EVENT_TYPE.ON_CLIMBED_BRANCH, this);

        myTransform = transform;
        isExtended = false;
        lockedToBranch = false;
        originalParent = myTransform.parent;

        if (typeOfHand == TypeOfHand.Left)
            myKeyCode = KeyCode.V;
        else
            myKeyCode = KeyCode.N;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(myKeyCode))
        {
            StopAllCoroutines();
            StartCoroutine(ExtendHand());
        }


        if (Input.GetKeyUp(myKeyCode))
        {
            StopAllCoroutines();
            lockedToBranch = false;
            graspManager.ChangeGraspObject(null, typeOfHand);
            myTransform.parent = originalParent;
            StartCoroutine(ReturnHand());
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        lockedToBranch = true;
        isExtending = false;
        isExtended = true;
        StopAllCoroutines();
        myTransform.parent = otherParent;
        graspManager.ChangeGraspObject(other.gameObject, typeOfHand);
    }

    private IEnumerator ExtendHand()
    {
        isExtending = true;
        while(myTransform.position != targetTransform.position)
        {
            myTransform.position = Vector3.MoveTowards(myTransform.position, targetTransform.position, Time.fixedDeltaTime * extendSpeed);
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

    public void OnEvent(EVENT_TYPE Event_Type, Object Param = null)
    {
        if(Event_Type == EVENT_TYPE.ON_CLIMBED_BRANCH)
        {
            StopAllCoroutines();
            lockedToBranch = false;
            graspManager.ChangeGraspObject(null, typeOfHand);
            myTransform.parent = originalParent;
            StartCoroutine(ReturnHand());
        }
    }
}