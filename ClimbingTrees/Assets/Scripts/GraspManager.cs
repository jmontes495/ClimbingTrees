using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraspManager : MonoBehaviour {

    public delegate void GraspActions();
    public static event GraspActions PlayerTeleported;
    public static event GraspActions PlayerIsOnBranch;
	public static event GraspActions PlayerReachedGound;

    public static GraspManager Instance
    {
        get { return instance; }
        set { }
    }

    [SerializeField]
    private GameObject objectLeftHand;

    [SerializeField]
    private GameObject objectRightHand;

    [SerializeField]
    private Transform LeftHand;

    [SerializeField]
    private Transform RightHand;

    [SerializeField]
    private Camera playerCamera;

    private float telespeed = 3f;

    private static GraspManager instance;

    private TreeBranchBehaviour currentBranch;

    private GrabbableObject objectInHands;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            DestroyImmediate(this);
    }

    public void EvaluateGrasp()
    {
        if (objectLeftHand != null && objectRightHand != null)
        {
            if (ReferenceEquals(objectLeftHand, objectRightHand))
            {
                if (objectLeftHand.GetComponent<TreeBranchBehaviour>() != null && objectRightHand.GetComponent<TreeBranchBehaviour>() != null)
                {
                    currentBranch = objectLeftHand.GetComponent<TreeBranchBehaviour>();
                    Teleport();
                }
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

    public void ChangeObjectInHands(GrabbableObject newObject, Transform hand)
    {
        if (ReferenceEquals(objectLeftHand, objectRightHand))
        {
            objectInHands = newObject;
            objectInHands.ChangeParent(hand);
        }
    }

    public void ClearObjectInHands()
    {
        if (objectInHands == null)
            return;
        
        if(!ReferenceEquals(objectLeftHand, objectInHands) || !ReferenceEquals(objectRightHand, objectInHands))
        {
            objectInHands.ChangeParent(null);
            objectInHands.AddForceInDirection(playerCamera.transform.forward);
            objectInHands = null;
        }
    }

    private void Teleport()
    {
        StartCoroutine(MoveToBranch());
    }

    private IEnumerator MoveToBranch()
    {
        float x = LeftHand.position.x + RightHand.position.x / 2;
        float z = LeftHand.position.z + RightHand.position.z / 2;
        Vector3 finalPosition = 0.5f*(LeftHand.position + RightHand.position);
        finalPosition.y = objectLeftHand.transform.position.y + TreeSpawningManager.Instance.GetPlayerHeight();
        Transform player = gameObject.transform;

        InputKeysManager.Instance.currentBranchAngle = objectRightHand.transform.rotation;
        player.GetComponent<BoxCollider>().isTrigger = true;
        while(player.position != finalPosition)
        {
            player.position = Vector3.MoveTowards(player.position, finalPosition, Time.fixedDeltaTime * telespeed);
            yield return new WaitForFixedUpdate();
        }

        PlayerTeleported();
        if (!currentBranch.IsTreeBase)
		{
			PlayerIsOnBranch();
            currentBranch.SetAsCurrentBranch();
		}
		else
		{
			PlayerReachedGound();
		}
        player.GetComponent<BoxCollider>().isTrigger = false;

    }
}
