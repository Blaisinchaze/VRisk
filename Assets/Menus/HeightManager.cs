using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class HeightManager : MonoBehaviour
{
    public GameData data;
    public int heightOffset = 0;

    private void Start()
    {
        data.heightOffset = heightOffset;
    }

    public void IncrementValue(int amount)
    {
        heightOffset += amount;
        data.heightOffset = heightOffset;
    }

    public void DecrementValue(int amount)
    {
        heightOffset -= amount;
        data.heightOffset = heightOffset;
    }
}
