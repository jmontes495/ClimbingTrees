﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{

    Vector2 mouseLook;
    Vector2 smoothV;
    Vector2 scaleVector;
    float sensitivity = 1.0f;
    float smoothing = 2.0f;
    Transform myTransform;

    GameObject character;
    Quaternion originalRotation;
    bool climbing = false;
    float threshold = 0.05f;
    // Use this for initialization
    void Start()
    {
        character = this.transform.parent.gameObject;
        scaleVector = new Vector2(sensitivity * smoothing, sensitivity * smoothing);
        myTransform = transform;
        originalRotation = myTransform.localRotation;
        GraspManager.PlayerTeleported += PlayerClimbing;
    }

    // Update is called once per frame
    void Update()
    {
        if(!climbing)
        {
            var md = new Vector2(Input.GetAxis("Axis 3"), Input.GetAxis("Axis 4"));
            md = Vector2.Scale(md, scaleVector);
            smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
            smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
            mouseLook += smoothV;
            mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);
            if(!InputKeysManager.Instance.IsBalancing)
                transform.localRotation = Quaternion.AngleAxis(mouseLook.y, Vector3.right);
            Quaternion result = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
            result.z = character.transform.rotation.z;
            if (!InputKeysManager.Instance.IsBalancing)
                character.transform.localRotation = result;
            else
                character.transform.Rotate(0, Input.GetAxis("Axis 3"), 0, 0);
        }
    }

    public void PlayerClimbing()
    {
        StartCoroutine(RestoreCamera());
    }

    private IEnumerator RestoreCamera()
    {
        climbing = true;
        Quaternion finalRotation = myTransform.localRotation;
        finalRotation.x = originalRotation.x;
        while (myTransform.localRotation.x > threshold || myTransform.localRotation.x < -threshold)
        {
            myTransform.localRotation = Quaternion.Slerp(myTransform.localRotation, finalRotation, 1/5f);
            yield return new WaitForFixedUpdate();
        }
        Input.ResetInputAxes();
        climbing = false;
    }
}