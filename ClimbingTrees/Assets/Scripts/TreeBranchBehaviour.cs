using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBranchBehaviour : MonoBehaviour
{

    MeshRenderer materialRenderer;
    
    private Transform teleportPosition;

    private Color color;

    private bool isCurrentBranch;

    public bool IsCurrentBranch
    {
        get { return isCurrentBranch; }
        private set { isCurrentBranch = value; }
    }
    // Use this for initialization
    void Start()
    {
        GraspManager.PlayerTeleported += ResetBranch;
        PlayerBalance.PlayerFellFromBranch += ResetBranch;
        materialRenderer = GetComponent<MeshRenderer>();
        color = materialRenderer.material.color;
        teleportPosition = transform;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (InputKeysManager.Instance.IsFalling || isCurrentBranch)
            return;

        if (materialRenderer.material.color == Color.yellow)
            materialRenderer.material.color = Color.green;
        else
            materialRenderer.material.color = Color.yellow;
    }

    private void OnTriggerExit(Collider collision)
    {
        if (InputKeysManager.Instance.IsFalling || isCurrentBranch)
            return;

        if (materialRenderer.material.color == Color.yellow)
            materialRenderer.material.color = color;
        else
            materialRenderer.material.color = Color.yellow;
    }

    private void ResetBranch()
    {
        isCurrentBranch = false;
        materialRenderer.material.color = color;
    }

    public void SetAsCurrentBranch()
    {
        isCurrentBranch = true;
    }

    public Vector3 GetTeleportPosition()
    {
        return teleportPosition.position;
    }
}
