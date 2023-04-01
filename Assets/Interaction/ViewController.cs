using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ViewController : MonoBehaviour
{
    private InputAction view_action;
    public float rotate_speed = 2f;
    
    private void Start()
    {
        view_action = InputHandler.Instance.input_asset.InputActionMap.RotateView;
    }

    private void FixedUpdate()
    {
        Vector2 input_vect = view_action.ReadValue<Vector2>();

        if (input_vect.y != 0)
        {
            transform.Rotate(input_vect.y * rotate_speed * -1, 0f, 0f, Space.Self);
        }

        if (input_vect.x != 0)
        {
            transform.Rotate(0f, input_vect.x * rotate_speed, 0f, Space.World);
        }
    }
}
