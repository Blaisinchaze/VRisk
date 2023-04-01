using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    private InputAction move_action;
    private Camera cam;
    private Rigidbody rig_rb;
    
    public float min_run_input_mag = 0.7f;
    public float walk_speed = 0.5f;
    public float run_speed = 1.0f;

    void Start()
    {
        move_action = InputHandler.Instance.input_asset.InputActionMap.MoveKeyStick;
        cam = Camera.main;
        rig_rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (move_action.ReadValue<Vector2>() != Vector2.zero)
        {
            movePlayer(move_action.ReadValue<Vector2>());
        }
    }

    public void movePlayer(Vector2 input_vect)
    {
        Vector3 world_move = new Vector3(input_vect.x, 0, input_vect.y);
        Vector3 rotated_vector = cam.transform.TransformDirection(world_move);
        
        float temp = rotated_vector.magnitude - rotated_vector.y;
        rotated_vector.x += (rotated_vector.x / temp) * rotated_vector.y;
        rotated_vector.z += (rotated_vector.z / temp) * rotated_vector.y;
        rotated_vector.y = 0;

        float speed = input_vect.magnitude <= min_run_input_mag ? walk_speed : run_speed;
        Vector3 direction = rotated_vector.normalized * speed;
        
        Debug.Log(direction);
        Vector3 new_pos = rig_rb.position + direction;
        rig_rb.MovePosition(new_pos);
    }
}
