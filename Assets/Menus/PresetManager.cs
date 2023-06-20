using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PresetManager : MonoBehaviour
{
    public Button joystickBT;
    public Button gestureBT;

    public bool useGestures = true;

    private void Start()
    {
        if (useGestures)
        {
            useGestures = true;
            joystickBT.interactable = true;
            gestureBT.interactable = false;
        }
        else
        {
            useGestures = false;
            joystickBT.interactable = false;
            gestureBT.interactable = true;
        }
    }

    public void JoystickSelected()
    {
        useGestures = false;
        joystickBT.interactable = false;
        gestureBT.interactable = true;
    }

    public void GestureSelected()
    {
        useGestures = true;
        joystickBT.interactable = true;
        gestureBT.interactable = false;
    }
}
