using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : GrabbableObject
{
    public override void ChangeParent(Transform parent)
    {
        base.ChangeParent(parent);

        if (parent == null)
            return;
        
        transform.rotation = new Quaternion(0, 0, 0, 0);
        transform.Rotate(new Vector3(0, 0, 90));
        transform.localPosition = new Vector3(0,0,0.5f);
    }
}
