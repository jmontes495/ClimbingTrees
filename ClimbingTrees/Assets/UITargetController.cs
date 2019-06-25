using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITargetController : MonoBehaviour
{
    [SerializeField]
	private GameObject targetStanding;

	[SerializeField]
    private GameObject targetCrouching;

	private bool isCrouching;

	private void Update()
	{
		if(InputKeysManager.Instance.IsCrouching && !isCrouching)
		{
			isCrouching = true;
			targetStanding.SetActive(false);
			targetCrouching.SetActive(true);
		}
		else if (!InputKeysManager.Instance.IsCrouching && isCrouching)
        {
			isCrouching = false;
            targetStanding.SetActive(true);
            targetCrouching.SetActive(false);
        }
	}
}
