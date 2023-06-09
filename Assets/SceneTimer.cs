using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SceneTimer : MonoBehaviour
{
    public TransitionManager transition_manager;
    public GameData data;
    private float timer = 0;

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        if (timer > 3.0f)
        {
            transition_manager.GoToSceneAsync(data.NextScene);
            Debug.Log(data.NextScene);
            timer = 0;
        }
    }
}
