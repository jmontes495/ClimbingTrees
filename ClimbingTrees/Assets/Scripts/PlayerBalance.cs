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
        myTransform.localRotation = InputKeysManager.Instance.currentBranchAngle;
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
