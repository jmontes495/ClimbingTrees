using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBalance : MonoBehaviour
{
    public delegate void BalanceEvent();
    public static event BalanceEvent PlayerFellFromBranch;

    [SerializeField]
    private float walkingSpeed;

    private float strength = 1f;

    private Transform myTransform;

    private float currentInclination;

    [SerializeField]
    private float acceleration;

    [SerializeField]
    private float delayInclination;

    [SerializeField]
    private float balanceLimit;

    [SerializeField]
    private float initialStrengthOfInclination;

    private Vector3 directionOfBranch;
    private void Start()
    {
        myTransform = transform;
        GraspManager.PlayerIsOnBranch += ChangeBalance;
    }

    private void ChangeBalance()
    {
        StopAllCoroutines();
        StartCoroutine(ApplyForce());
    }

    private void SetInitialPlayerRotation()
    {
        Quaternion playerRotation = myTransform.localRotation;
        playerRotation.z = 0;
        playerRotation.x = 0;
        myTransform.localRotation = playerRotation;
        Quaternion globalRotation = myTransform.rotation;
        globalRotation.y = 0;
        myTransform.rotation = globalRotation;
        Debug.LogError(InputKeysManager.Instance.currentBranchAngle);
        myTransform.Rotate(new Vector3(0, InputKeysManager.Instance.currentBranchAngle, 0), Space.Self );
        currentInclination = 0;
        directionOfBranch = myTransform.forward;
    }

    private IEnumerator ApplyForce()
    {
        SetInitialPlayerRotation();
        WaitForSeconds delay = new WaitForSeconds(delayInclination);
        while (!InputKeysManager.Instance.IsBalancing)
            yield return new WaitForEndOfFrame();

        currentInclination = initialStrengthOfInclination;

        while (InputKeysManager.Instance.IsBalancing && currentInclination < balanceLimit && currentInclination > -balanceLimit)
        {
            float translation = Input.GetAxis("Vertical") * walkingSpeed * Time.deltaTime;
            float straff = Input.GetAxis("Horizontal") * -strength;

            transform.position += directionOfBranch*translation;

            currentInclination = acceleration*currentInclination + straff;
            myTransform.Rotate(new Vector3(0, 0, currentInclination), Space.Self);

            
            yield return delay;
        }
        PlayerFellFromBranch();
    }    
}
