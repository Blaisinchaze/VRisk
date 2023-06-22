using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject
{
    public float record_position_interval = 0;
    public int NextScene = 2;
}

public enum GameDataVariable
{
    RECORD_POSITION_INTERVAL
}
