using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {

    Vector2 mouseLook;
    Vector2 smoothV;
    Vector2 scaleVector;
    float sensitivity = 5.0f;
    float smoothing = 2.0f;
    Transform myTransform;
    float rotationLimit = 0.5f;

    GameObject character;
	// Use this for initialization
	void Start () {
        character = this.transform.parent.gameObject;
        scaleVector = new Vector2(sensitivity * smoothing, sensitivity * smoothing);
        myTransform = transform;
    }
	
	// Update is called once per frame
	void Update () {
        var md = new Vector2(Input.GetAxisRaw("RightStickHorizontal"), Input.GetAxisRaw("RightStickVertical"));
        md = Vector2.Scale(md, scaleVector);
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;

        Quaternion resultCamera = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        if (resultCamera.x > -rotationLimit && resultCamera.x < rotationLimit)
            myTransform.localRotation = resultCamera;
        
        Quaternion result = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
        result.z = character.transform.rotation.z;
        character.transform.localRotation = result;
    }
}
