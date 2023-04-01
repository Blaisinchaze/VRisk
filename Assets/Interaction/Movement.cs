using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    private InputAction move_action;
    private Camera cam;
    private Rigidbody rig_rb;
    public float move_speed = 0.5f;
    
    void Start()
    {
        move_action = InputHandler.Instance.input.InputActionMap.MoveKeyStick;
        cam = Camera.main;
        rig_rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector2 input_vect = move_action.ReadValue<Vector2>();

        if (input_vect != Vector2.zero)
        {
            Vector3 world_move = new Vector3(input_vect.x, 0, input_vect.y);
            Vector3 rotatedVector = cam.transform.TransformDirection(world_move);

            rotatedVector *= move_speed;
            
            rig_rb.MovePosition(new Vector3(rig_rb.position.x + rotatedVector.x, rig_rb.position.y, rig_rb.position.z + rotatedVector.z));
        }
    }
}
