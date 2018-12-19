using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraspManager : MonoBehaviour {

    [SerializeField]
    private GameObject objectLeftHand;

    [SerializeField]
    private GameObject objectRightHand;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if(objectLeftHand != null && objectRightHand != null)
            {
                if (ReferenceEquals(objectLeftHand, objectRightHand))
                    Teleport();
            }
        }
    }

    public void ChangeGraspObject(GameObject newGameObject, HandController.TypeOfHand handType)
    {
        if (handType == HandController.TypeOfHand.Left)
            objectLeftHand = newGameObject;
        else
            objectRightHand = newGameObject;
    }

    private void Teleport()
    {
        gameObject.transform.position = objectLeftHand.GetComponent<TreeBranchBehaviour>().GetTeleportPosition();
        EventManager.Instance.Notify(EVENT_TYPE.ON_CLIMBED_BRANCH);
    }
}
