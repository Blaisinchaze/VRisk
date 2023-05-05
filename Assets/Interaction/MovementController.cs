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
    
    private Vector3 prev_controller_r; 
    private Vector3 prev_controller_l;

    [SerializeField] private float sensibility = 3;
    private const float threshold = 0.10f;

    private bool left_mov = false;
    private bool right_mov = false;

    private float timer = 0.0f;

    private float speed_multiplier = 100.0f;
    public float ratio_of_motion = 0.0f;               
    
    public bool moving = false;

    void Start()
    {
        move_action = InputHandler.Instance.input_asset.InputActionMap.MoveKeyStick;
        cam = Camera.main;
        rig_rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        GestureControl();
        
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
        
        Vector3 new_pos = rig_rb.position + direction;
        rig_rb.MovePosition(new_pos);
    }

    private void GestureControl()
    {
        timer += Time.fixedDeltaTime;

        Vector3 controller_r = InputHandler.Instance.input_asset.InputActionMap.MoveRight_Hand.ReadValue<Vector3>();
        Vector3 controller_l = InputHandler.Instance.input_asset.InputActionMap.MoveLeft_Hand.ReadValue<Vector3>();

        Vector3 controller_delta_r = (controller_r - prev_controller_r) * sensibility;
        Vector3 controller_delta_l = (controller_l - prev_controller_l) * sensibility;

        left_mov = controller_delta_l.y is > threshold or < -threshold;
        right_mov = controller_delta_r.y is > threshold or < -threshold;

        //If both controllers are being moved at a certain intensity calculate we are moving
        if (right_mov && left_mov)
        {
            ratio_of_motion = ((controller_delta_r.y + controller_delta_l.y) / 2) * speed_multiplier;
            ratio_of_motion = ratio_of_motion < 0 ? ratio_of_motion * -1 : ratio_of_motion;
            timer = 0;
        }

        if (timer < 0.5f)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }


        if (moving)
        {
            movePlayer(new Vector2(0, 1));
        }

        prev_controller_r = controller_r;
        prev_controller_l = controller_l;
    }
}
