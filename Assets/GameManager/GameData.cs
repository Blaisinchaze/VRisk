using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject
{
    public enum SceneIndex : int
    {
        LOADING_SCENE = 0,
        MIAN_MENU = 1,
        SIMULATION = 2
    }

    public int NextScene { get; set; }

    private void Awake()
    {
        NextScene = 1;
    }
}
