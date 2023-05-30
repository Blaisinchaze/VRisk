using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class MenuVisualsHandler : MonoBehaviour
{
    public bool inside_menu = false;

    public XRBaseController left_controller;
    public XRBaseController right_controller;

    private XRInteractorLineVisual left_cast;
    private XRInteractorLineVisual right_cast;

    private InputAction trigger_left_pressed;
    private InputAction trigger_left_touched;
    
    private InputAction trigger_right_pressed;
    private InputAction trigger_right_touched;
    
    private enum CastSide
    {
        Left,
        Right,
        None
    }

    private CastSide current = CastSide.None;
    private CastSide previous = CastSide.None;
    
    void Start()
    {
        left_cast = left_controller.GetComponent<XRInteractorLineVisual>();
        right_cast = right_controller.GetComponent<XRInteractorLineVisual>();
        
        trigger_left_pressed = GameManager.Instance.InputHandler.input_asset.InputActionMap.InteractLeft_Hand;
        trigger_left_touched = GameManager.Instance.InputHandler.input_asset.InputActionMap.TouchLeft_Hand;
        trigger_right_pressed = GameManager.Instance.InputHandler.input_asset.InputActionMap.InteractRight_Hand;
        trigger_right_touched = GameManager.Instance.InputHandler.input_asset.InputActionMap.TouchRight_Hand;
    }
    
    void FixedUpdate()
    {
        if (inside_menu)
        {
            //Uses touch and press to cast a line if the player is using the left controller to point
            if (current != CastSide.Left)
            {
                if (trigger_left_touched.triggered || trigger_left_pressed.triggered)
                {
                    current = CastSide.Left;
                }
            }
            
            //Same but with the right controller instead
            if (current != CastSide.Right)
            {
                if (trigger_right_touched.triggered || trigger_right_pressed.triggered)
                {
                    current = CastSide.Right;
                }
            }

            //Sets the correct line to be casted in a menu
            if (current != previous)
            {
                switch (current)
                {
                    case CastSide.Left:
                        left_cast.enabled = true;
                        right_cast.enabled = false;
                        break;

                    case CastSide.Right:
                        left_cast.enabled = false;
                        right_cast.enabled = true;
                        break;
                    
                    case CastSide.None:
                        left_cast.enabled = false;
                        right_cast.enabled = false;
                        break;
                }
                
                previous = current;
            }
        }
    }
}
