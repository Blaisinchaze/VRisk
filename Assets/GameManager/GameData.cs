using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject
{
    public int NextScene { get; set; }

    public int Volume { get; set; }
    public int Height { get; set; }

    public float TurnSpeed { get; set; }

    private void Awake()
    {
        NextScene = 2;

        Volume = 50;
        Height = 170;
        TurnSpeed = 5.0f;
    }
}
