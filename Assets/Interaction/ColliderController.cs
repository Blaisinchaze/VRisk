using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider))]
public class ColliderController : MonoBehaviour
{
    private Collider collider;
    private Transform collider_transform;
    private const float scale_interval = 0.8f;
    private float scale_timer = 0;
    public Transform head;

    private void Start()
    {
        collider = GetComponent<Collider>();
        collider_transform = collider.transform;
    }

    private void FixedUpdate()
    {
        scale_timer += Time.deltaTime;
        
        collider_transform.position = new Vector3(head.position.x, 
                                    collider_transform.position.y,
                                    head.position.z);
    }
}
