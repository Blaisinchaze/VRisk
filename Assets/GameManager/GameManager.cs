using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public int seed = 0;
    public static GameManager Instance { get; private set; }
    
    public TimelineManager TimelineManager { get; private set; }
    public BuildingManager BuildingManager { get; private set; }
    public HapticFeedbackHandler HapticFeedbackHandler { get; private set; }
    public InputHandler InputHandler { get; private set; }
    public AudioManager AudioManager { get; private set; }
    public ParticleManager ParticleManager { get; private set; }
    public DebrisHandler DebrisHandler { get; private set; }

    public GameObject Player { get; private set; }

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
        AudioManager = GetComponentInChildren<AudioManager>();
        ParticleManager = GetComponentInChildren<ParticleManager>();
        DebrisHandler = GetComponentInChildren<DebrisHandler>();
        TimelineManager = GetComponentInChildren<TimelineManager>();

        Player = GameObject.FindWithTag("Player");

        Random.InitState(seed);
    }

    static public float earthquakeIntensityCurve(float _x)
    {
        return -15.5f * (float)Math.Pow(_x - 0.48f, 4) + (0.3f * _x) + 0.824f;
    }

    static public float debrisIntensityCurve(float _x)
    {
        return -4f * (float)Math.Pow(_x - 0.48f, 4) + (0.3f * _x) + 0.8f;
    }
}
