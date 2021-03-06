﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeysManager : MonoBehaviour {

    public static InputKeysManager Instance
    {
        get { return instance; }
        set { }
    }

    public bool IsBalancing
    {
        get { return isBalancing; }
        private set { isBalancing = value; }
    }

    public bool IsFalling
    {
        get { return isFalling; }
        private set { isFalling = value; }
    }

    public bool IsStandingUp
    {
        get { return isStandingUp; }
        private set { isStandingUp = value; }
    }

	public bool IsCrouching
    {
		get { return isCrouching; }
		private set { isCrouching = value; }
    }

    private bool isBalancing;

    private bool isFalling;

    private bool isStandingUp;

	private bool isCrouching;

    private static InputKeysManager instance;

    [SerializeField]
    private HandController leftHand;

    [SerializeField]
    private HandController rightHand;

	[SerializeField]
	private Crouching crouchingController;

    public Quaternion currentBranchAngle;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            DestroyImmediate(this);

        GraspManager.PlayerIsOnBranch += ChangeBalance;
		GraspManager.PlayerReachedGound += ChangeGround;
        PlayerBalance.PlayerFellFromBranch += ChangeFalling;
        PlayerFall.PlayerReachedGound += ChangeGround;
        PlayerFall.PlayerStandingUp += ChangeStandingUp;
    }

    private void ChangeBalance()
    {
        isBalancing = true;
        isFalling = false;
        isStandingUp = false;
		isCrouching = false;  
    }

    private void ChangeGround()
    {
        isBalancing = false;
        isFalling = false;
        isStandingUp = false;
		isCrouching = false;  
    }

    private void ChangeFalling()
    {
        isBalancing = false;
        isFalling = true;
        isStandingUp = false;
		isCrouching = false;  
    }

    private void ChangeStandingUp()
    {
        isBalancing = false;
        isFalling = false;
		isStandingUp = true;
		isCrouching = false;  
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            leftHand.InputExtendHand();
        }

        if (Input.GetMouseButtonDown(1))
        {
            rightHand.InputExtendHand();
        }

        if (Input.GetMouseButtonUp(0))
        {
            leftHand.InputDropHand();
            GraspManager.Instance.ClearObjectInHands();
        }

        if (Input.GetMouseButtonUp(1))
        {
            rightHand.InputDropHand();
            GraspManager.Instance.ClearObjectInHands();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GraspManager.Instance.EvaluateGrasp();
        }

		if (Input.GetKeyDown(KeyCode.LeftControl))
        {
			if (isFalling || isStandingUp)
				return;
			
			isCrouching = true;
			crouchingController.InputCrouch();
			rightHand.CheckCrouching();
			leftHand.CheckCrouching();
        }

		if (Input.GetKeyUp(KeyCode.LeftControl))
        {
			if (isFalling || isStandingUp)
                return;
			
            isCrouching = false;   
			crouchingController.InputStandUp();
			rightHand.CheckCrouching();
            leftHand.CheckCrouching();
        }
    }
}
