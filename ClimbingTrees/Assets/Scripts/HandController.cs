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

    private Transform myTransform;

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
        GraspManager.PlayerTeleported += ReturnHandsToOGPosition;
        myTransform = transform;
        isExtended = false;
        lockedToBranch = false;
        originalParent = myTransform.parent;
	}
	
	public void InputExtendHand()
    {
        StopAllCoroutines();
        StartCoroutine(ExtendHand());
    }

    public void InputDropHand()
    {
        PopHandsToOGPosition();
    }    

    private void ReturnHandsToOGPosition()
    {
        StopAllCoroutines();
        lockedToBranch = false;
        graspManager.ChangeGraspObject(null, typeOfHand);
        myTransform.parent = originalParent;
        StartCoroutine(ReturnHand());
    }

    private void PopHandsToOGPosition()
    {
        StopAllCoroutines();
        lockedToBranch = false;
        graspManager.ChangeGraspObject(null, typeOfHand);
        myTransform.parent = originalParent;
        myTransform.position = initialTransform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (InputKeysManager.Instance.IsBalancing || InputKeysManager.Instance.IsFalling)
            return;

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


}
