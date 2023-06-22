using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject
{
    public float record_position_interval = 0;
    public float grid_cell_size_x = 0;
    public float grid_cell_size_y = 0;
    
    public int NextScene = 2;
}

public enum GameDataVariable
{
    RECORD_POSITION_INTERVAL,
    GRID_CELL_SIZE_X,
    GRID_CELL_SIZE_Y
}
