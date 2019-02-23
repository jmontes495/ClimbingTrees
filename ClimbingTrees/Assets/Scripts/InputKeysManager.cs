using System.Collections;
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

    [SerializeField]

    private bool isBalancing;

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
        GraspManager.PlayerIsOnGround += ChangeGround;
        PlayerBalance.PlayerFellFromBranch += ChangeGround;
    }

    private void ChangeBalance()
    {
        isBalancing = true;
    }

    private void ChangeGround()
    {
        isBalancing = false;
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.Joystick1Button4))
        {
            leftHand.InputExtendHand();
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            rightHand.InputExtendHand();
        }

        if (Input.GetKeyUp(KeyCode.Joystick1Button4))
        {
            leftHand.InputDropHand();
        }

        if (Input.GetKeyUp(KeyCode.Joystick1Button5))
        {
            rightHand.InputDropHand();
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            GraspManager.Instance.EvaluateGrasp();
        }

        
    }
}
