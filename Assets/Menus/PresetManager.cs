using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PresetManager : MonoBehaviour
{
    public GameData data;
    public Button joystickBT;
    public Button gestureBT;

    public bool useGestures = true;

    private void Start()
    {
        if (useGestures)
        {
            GestureSelected();
        }
        else
        {
            JoystickSelected();
        }
    }

    public void JoystickSelected()
    {
        useGestures = false;
        joystickBT.interactable = false;
        gestureBT.interactable = true;
        data.movementType = GameData.MovementType.JOYSTICK;
    }

    public void GestureSelected()
    {
        useGestures = true;
        joystickBT.interactable = true;
        gestureBT.interactable = false;
        data.movementType = GameData.MovementType.GESTURE;
    }
}
