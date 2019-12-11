using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBranchBehaviour : GrabbableObject
{
    private Transform teleportPosition;

    private bool isCurrentBranch;

	[SerializeField]
	private bool isTreeBase;

    [SerializeField]
    private Collider endCollider;

    [SerializeField]
    private Collider beginningCollider;

    [SerializeField]
    private Material opaqueMat;

    [SerializeField]
    private Material transparentMat;

    public bool IsCurrentBranch
    {
        get { return isCurrentBranch; }
        private set { isCurrentBranch = value; }
    }

	public bool IsTreeBase
    {
		get { return isTreeBase; }
		private set { isTreeBase = value; }
    }
    // Use this for initialization
    void Start()
    {
        GraspManager.PlayerTeleported += ResetBranch;
		GraspManager.PlayerReachedGound += ResetBranch;
        PlayerBalance.PlayerFellFromBranch += ResetBranch;
        color = materialRenderer.material.color;
        teleportPosition = transform;

        if(!isTreeBase)
            transform.localPosition = new Vector3(transform.localPosition.x, TreeSpawningManager.Instance.AdjustHeight(transform.localPosition.y), transform.localPosition.z);
    }

    public override void UpdateColorUp()
    {
        if (isCurrentBranch)
            return;

        base.UpdateColorUp();
    }

    public override void UpdateColorDown()
    {
        if (isCurrentBranch)
            return;

        base.UpdateColorDown();
    }

    public override bool IsStaticObject()
    {
        return true;
    }

    private void ResetBranch()
    {
        isCurrentBranch = false;
        materialRenderer.material = opaqueMat;
        materialRenderer.material.color = color;
        if (!isTreeBase)
        {
            beginningCollider.gameObject.SetActive(false);
            endCollider.gameObject.SetActive(false); 
        }        
    }

    public void SetAsCurrentBranch()
    {
        isCurrentBranch = true;
		Color transluscent = color;
		transluscent.a = 0.4f;
        materialRenderer.material = transparentMat;

		materialRenderer.material.color = transluscent;
        if (!isTreeBase)
        {
            beginningCollider.gameObject.SetActive(true);
            endCollider.gameObject.SetActive(true);
        }
    }

    public Vector3 GetTeleportPosition()
    {
        return teleportPosition.position;
    }
}
