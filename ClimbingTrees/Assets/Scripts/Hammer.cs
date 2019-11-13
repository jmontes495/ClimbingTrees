using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : GrabbableObject
{
    [SerializeField]
    private SpriteRenderer hit;

    private bool animating;

    void Start()
    {
        color = materialRenderer.material.color;
        myTransform = gameObject.transform;
        rigidbody = GetComponent<Rigidbody>();
        hit = GetComponentInChildren<SpriteRenderer>();
        hit.enabled = false;
    }

    public override void ChangeParent(Transform parent)
    {
        base.ChangeParent(parent);

        if (parent == null)
            return;
        
        transform.rotation = new Quaternion(0, 0, 0, 0);
        transform.Rotate(new Vector3(0, 0, 90));
        transform.localPosition = new Vector3(0,0,0.5f);
    }

    public void ShowHammerHit()
    {
        if (animating)
            return;

        animating = true;
        StartCoroutine(AnimateHit());
    }

    private IEnumerator AnimateHit()
    {
        hit.enabled = true;
        yield return new WaitForSeconds(0.15f);
        hit.enabled = false;
        animating = false;
    }
}
