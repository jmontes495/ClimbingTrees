﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    float sensitivity = 0.7f;
    Transform myTransform;
    GameObject character;

    void Start()
    {
        character = this.transform.parent.gameObject;
        myTransform = transform;
        CursorControl.SetLocalCursorPos(new Vector2(Screen.width / 2, Screen.height / 2));
    }

    void Update()
    {
        if(!InputKeysManager.Instance.IsFalling && !InputKeysManager.Instance.IsStandingUp)
        {
            Vector2 mousePosition = (Vector2)Input.mousePosition;
            float x = mousePosition.x - Screen.width / 2;
            float y = mousePosition.y - Screen.height / 2;
            x *= sensitivity;
            y *= sensitivity;

            y = Mathf.Clamp(y, -90, 90);

            character.transform.eulerAngles = new Vector3(0, x, 0);
            transform.eulerAngles = new Vector3(-y,x,0);
        }
    }
}