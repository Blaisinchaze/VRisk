using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject
{
    public int NextScene;

    private void Awake()
    {
        NextScene = 2;
    }
}
