using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ColliderController : MonoBehaviour
{
    public Transform head;
    private Transform collider;

    private void Awake()
    {
        collider = this.GetComponent<Collider>().transform;
    }

    private void FixedUpdate()
    {
        collider.position = new Vector3(head.transform.position.x, 
                                                    collider.transform.position.y,
                                                        head.position.z);
    }
}
