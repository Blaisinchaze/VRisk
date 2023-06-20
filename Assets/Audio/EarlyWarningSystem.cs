using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.InputSystem;

public class EarlyWarningSystem : MonoBehaviour
{
    public Sound sound;
    public List<AudioSource> sources;

    private void Awake()
    {
        sources.AddRange(transform.GetComponentsInChildren<AudioSource>());
    }

    // start to be removed.
    private void Start()
    {
        GameManager.Instance.InputHandler.input_asset.InputActionMap.Debug.started += test;
    }

    private void test(InputAction.CallbackContext _context)
    {
        triggerWarningSiren(30);
    }
    // end stuff to be removed.

    public void triggerWarningSiren(float _duration)
    {
        foreach (var source in sources)
        {
            GameManager.Instance.AudioManager.PlaySound(source, true, false, source.transform.position, AudioManager.SoundID.WARNING_SIREN, _duration);
        }
    }
}
