using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
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

    public void triggerHapticFeedback(HapticFeedbackControllerID _controller, float _intensity, float _duration)
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

    public void triggerSineIntensityHapticFeedback(float _intensity, float _duration)
    {
        StartCoroutine(SineHapticFeedback(_intensity, _duration, 0.03f));
    }

    public void triggerCosIntensityHapticFeedback(float _intensity, float _duration)
    {
        StartCoroutine(CosHapticFeedback(_intensity, _duration, 0.03f));
    }
    
    private IEnumerator SineHapticFeedback(float _max_intensity, float _duration, float _alter_intensity_interval)
    {
        float elapsed = 0.0f;
        float alter_intensity_timer = 0.0f;

        while (elapsed < _duration)
        {
            if (alter_intensity_timer > _alter_intensity_interval)
            {
                float progress = elapsed / _duration;
                float intensity = _max_intensity * Mathf.Sin(progress * Mathf.PI);

                left_controller.SendHapticImpulse(intensity, 1);
                right_controller.SendHapticImpulse(intensity, 1);

                alter_intensity_timer = 0.0f;
            }

            alter_intensity_timer += Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
    
    private IEnumerator CosHapticFeedback(float _max_intensity, float _duration, float _alter_intensity_interval)
    {
        float elapsed = 0.0f;
        float alter_intensity_timer = 0.0f;

        while (elapsed < _duration)
        {
            if (alter_intensity_timer > _alter_intensity_interval)
            {
                float progress = elapsed / _duration;
                float intensity = _max_intensity * Mathf.Cos(progress * Mathf.PI);

                left_controller.SendHapticImpulse(intensity, 1);
                right_controller.SendHapticImpulse(intensity, 1);

                alter_intensity_timer = 0.0f;
            }

            alter_intensity_timer += Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}