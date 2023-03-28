using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Serialization;

public class GestureController : MonoBehaviour
{
    private Vector3 prev_controller_r; 
    private Vector3 prev_controller_l;

    private float sensibility = 3;
    private const float threshold = 0.10f;

    private bool left_mov = false;
    private bool right_mov = false;

    private float timer = 0.0f;

    private float speed_multiplier = 100.0f;
    public float speed = 0.0f;               
    
    public bool moving = false;

    // Update is called once per frame
    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        
        Vector3 controller_r = InputHandler.Instance.input.InputActionMap.MoveRight_Hand.ReadValue<Vector3>();
        Vector3 controller_l = InputHandler.Instance.input.InputActionMap.MoveLeft_Hand.ReadValue<Vector3>();

        Vector3 controller_delta_r = (controller_r - prev_controller_r) * sensibility;
        Vector3 controller_delta_l = (controller_l - prev_controller_l) * sensibility;

        left_mov = controller_delta_l.y is > threshold or < -threshold;
        right_mov = controller_delta_r.y is > threshold or < -threshold;

        //If both controllers are being moved at a certain intensity calculate we are moving
        if (right_mov && left_mov)
        {
            speed = ((controller_delta_r.y + controller_delta_l.y) / 2) * speed_multiplier;
            speed = speed < 0 ? speed * -1 : speed;
            timer = 0;
        }

        //If running is true reset the timer
        moving = timer < 0.5f;

        prev_controller_r = controller_r;
        prev_controller_l = controller_l;
    }
}
