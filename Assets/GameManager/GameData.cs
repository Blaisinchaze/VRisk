using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject
{
    public enum SceneIndex : int
    {
        LOADING_SCENE = 0,
        MIAN_MENU = 1,
        SIMULATION = 2,
        END_MENU = 3
    }

    public enum MovementType
    {
        JOYSTICK,
        GESTURE
    }

    // Transition Data
    public int totalTime = 0;
    public int NextScene = 1;

    // Scene data
    public float record_position_interval = 0;
    public float grid_cell_size_x = 0;
    public float grid_cell_size_y = 0;
    public string user_name;
    
    // Settings Data
    public int volume = 50;
    public int turnSpeed = 5;
    public int heightOffset = 0;
    public MovementType movementType = MovementType.GESTURE;
    
    private void Awake()
    {
        NextScene = 1;
    }
}

public enum GameDataVariable
{
    RECORD_POSITION_INTERVAL,
    GRID_CELL_SIZE_X,
    GRID_CELL_SIZE_Y
}
