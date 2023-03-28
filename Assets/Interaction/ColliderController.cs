using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ColliderController : MonoBehaviour
{
    public Transform head;
    private Transform collider_transform;

    private void Awake()
    {
        collider_transform = this.GetComponent<Collider>().transform;
    }

    private void FixedUpdate()
    {
        collider_transform.position = new Vector3(head.transform.position.x, 
                                    collider_transform.transform.position.y,
                                    head.position.z);
    }
}
