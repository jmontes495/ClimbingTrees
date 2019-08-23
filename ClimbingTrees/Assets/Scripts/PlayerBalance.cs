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
		GraspManager.PlayerReachedGound += ChangeGround;
        PlayerFall.PlayerReachedGound += ChangeGround;
    }

    private void ChangeBalance()
    {
        StopAllCoroutines();
        StartCoroutine(ApplyForce());
    }

	private void ChangeGround()
    {
        StopAllCoroutines();
    }

    private void SetInitialPlayerRotation()
    {
        myTransform.rotation = InputKeysManager.Instance.currentBranchAngle;
        currentInclination = 0;
        directionOfBranch = myTransform.forward;
        if (transform.localRotation.y - directionOfBranch.y > 0.5f || transform.localRotation.y - directionOfBranch.y < -0.5f)
            directionOfBranch = -myTransform.forward;
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
            float directionOfWalking = 1;

            if (transform.localRotation.y - directionOfBranch.y > 0.5f || transform.localRotation.y - directionOfBranch.y < -0.5f)
                directionOfWalking = -1;

            transform.position += directionOfBranch*translation*directionOfWalking;

            currentInclination = acceleration*currentInclination + straff;
            myTransform.Rotate(new Vector3(0, 0, currentInclination), Space.Self);

            
            yield return delay;
        }
        PlayerFellFromBranch();
    }    
}
