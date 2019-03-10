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

    [SerializeField]

    private bool isBalancing;

    private bool isFalling;

    private static InputKeysManager instance;

    [SerializeField]
    private HandController leftHand;

    [SerializeField]
    private HandController rightHand;

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
        PlayerBalance.PlayerFellFromBranch += ChangeFalling;
        PlayerFall.PlayerReachedGound += ChangeGround;
    }

    private void ChangeBalance()
    {
        isBalancing = true;
        isFalling = false;
    }

    private void ChangeGround()
    {
        isBalancing = false;
        isFalling = false;
    }

    private void ChangeFalling()
    {
        isBalancing = false;
        isFalling = true;
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
        }

        if (Input.GetMouseButtonUp(1))
        {
            rightHand.InputDropHand();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GraspManager.Instance.EvaluateGrasp();
        }

        
    }
}
