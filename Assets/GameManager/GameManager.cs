using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public BuildingManager BuildingManager { get; private set; }
    public HapticFeedbackHandler HapticFeedbackHandler { get; private set; }
    public InputHandler InputHandler { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        BuildingManager = GetComponentInChildren<BuildingManager>();
        HapticFeedbackHandler = GetComponentInChildren<HapticFeedbackHandler>();
        InputHandler = GetComponentInChildren<InputHandler>();
    }

    static public float earthquakeIntensityCurve(float _x)
    {
        return -15.5f * (float)Math.Pow(_x - 0.48f, 4) + (0.3f * _x) + 0.824f;
    }
}
