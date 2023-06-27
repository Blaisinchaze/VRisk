using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject
{
    public int NextScene;
    public float record_position_interval = 0;
    public float grid_cell_size_x = 0;
    public float grid_cell_size_y = 0;
    public string user_name;
    
    public enum SceneIndex : int
    {
        LOADING_SCENE = 0,
        MIAN_MENU = 1,
        SIMULATION = 2
    }
    
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
