using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraspManager : MonoBehaviour {

    public delegate void GraspActions();
    public static event GraspActions PlayerTeleported;
    public static event GraspActions PlayerIsOnBranch;
    public static event GraspActions PlayerIsOnGround;

    public static GraspManager Instance
    {
        get { return instance; }
        set { }
    }

    [SerializeField]
    private GameObject objectLeftHand;

    [SerializeField]
    private GameObject objectRightHand;

    private static GraspManager instance;

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log(":v");
            
        }
    }

    public void EvaluateGrasp()
    {
        if (objectLeftHand != null && objectRightHand != null)
        {
            if (ReferenceEquals(objectLeftHand, objectRightHand))
                Teleport();
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
        PlayerTeleported();
        PlayerIsOnBranch();
    }

    public void PlayerIsOnTheGround()
    {
        PlayerIsOnGround();
    }
}
