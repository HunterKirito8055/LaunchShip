using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildFixedScale : MonoBehaviour
{
    public float FixedScale = 1f;
    SphereCollider sphereCollider;
    private void Awake()
    {
        sphereCollider = GetComponentInChildren<SphereCollider>();
    }
    //private void Update()
    //{
    //    //transform.localScale = new Vector3(FixedScale / transform.parent.localScale.x, FixedScale / transform.parent.localScale.y, FixedScale / transform.parent.localScale.z);

    //    //sphereCollider.radius = FixedScale / sphereCollider.radius;
    //}
}
