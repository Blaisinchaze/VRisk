using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SpatialTracking;

public class InputHandler : MonoBehaviour
{
    private static InputHandler instance;
    
    public static InputHandler Instance
    { 
        get { return instance;}
    }

    public Input input_asset { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
        
        input_asset = new Input();
    }

    private void OnEnable()
    {
        input_asset.Enable();
    }

    private void OnDisable()
    {
        input_asset.Disable();
    }
}
