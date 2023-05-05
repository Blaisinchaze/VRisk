using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public enum HapticFeedbackControllerID
{
    LEFT, 
    RIGHT
}
public class HapticFeedbackHandler : MonoBehaviour
{
    public XRBaseController left_controller;
    public XRBaseController right_controller;

    public void TriggerHapticFeedback(HapticFeedbackControllerID _controller, float _intensity, float _duration)
    {
        switch (_controller)
        {
            case HapticFeedbackControllerID.LEFT:
            {
                left_controller.SendHapticImpulse(_intensity, _duration);
                break;
            }
            case HapticFeedbackControllerID.RIGHT:
            {
                right_controller.SendHapticImpulse(_intensity, _duration);
                break;
            }

            default:
            {
                throw new ArgumentOutOfRangeException(nameof(_controller), _controller, null);
            }
        }
    }
}
