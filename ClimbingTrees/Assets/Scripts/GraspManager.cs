using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraspManager : MonoBehaviour {

    public delegate void GraspActions();
    public static event GraspActions PlayerTeleported;
    public static event GraspActions PlayerIsOnBranch;

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

    private float telespeed = 10f;

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
        StartCoroutine(MoveToBranch());
    }

    private IEnumerator MoveToBranch()
    {
        float x = LeftHand.position.x + RightHand.position.x / 2;
        float z = LeftHand.position.z + RightHand.position.z / 2;
        Vector3 finalPosition = 0.5f*(LeftHand.position + RightHand.position);
        finalPosition.y += 1;

        Transform player = gameObject.transform;
        while(player.position != finalPosition)
        {
            player.position = Vector3.MoveTowards(player.position, finalPosition, Time.fixedDeltaTime * telespeed);
            yield return new WaitForFixedUpdate();
        }

        PlayerTeleported();
        PlayerIsOnBranch();
    }
}
