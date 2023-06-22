using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DataTracker : MonoBehaviour
{
    [SerializeField] private float timer;
    public TimerDisplay timer_display;
    
    public float record_position_interval = 0;
    public float record_position_timer;
    public GameObject map_origin;
    public Vector2 grid_cell_size;

    private bool active = true;
    private List<List<string>> _recorded_locations;

    private void Start()
    {
        _recorded_locations = new List<List<string>>();

        GameData data = GameManager.Instance.Data;
        record_position_interval = data.record_position_interval;
        grid_cell_size = new Vector2(data.grid_cell_size_x, data.grid_cell_size_y);
        
        GameManager.Instance.InputHandler.input_asset.InputActionMap.Debug.started += test;
    }

    private void Update()
    {
        if (active)
        {
            timer += Time.deltaTime;
            timer_display.updateTimer(timer);
            
            record_position_timer += Time.deltaTime;
            if (record_position_timer > record_position_interval)
            {
                recordDataPoint();
                record_position_timer = 0f;
            }
        }
    }

    private void recordDataPoint()
    {
        Vector2 grid_location = getGridLocation();
        _recorded_locations.Add(new List<string> {timer.ToString(), grid_location.ToString()});
    }

    private Vector2 getGridLocation()
    {
        Vector3 local_position = map_origin.transform.InverseTransformPoint(GameManager.Instance.Player.transform.position);
        Vector2 local_position_2D = new Vector2(local_position.x, local_position.z);
        Vector2 grid_location = new Vector2(local_position_2D.x / grid_cell_size.x, local_position_2D.y / grid_cell_size.y);
        return grid_location;
    }

    private void test(InputAction.CallbackContext _context)
    {
        recordTime(true);
    }

    public void recordTime(bool _survived)
    {
        active = false;

        string survived = _survived ? "Survived" : "Died";
        _recorded_locations.Add(new List<string> {timer.ToString(), getGridLocation().ToString(), survived});
        
        FileManager.saveToCSV(new []{"GameManager", "record.csv"}, new []{"test.csv"} ,_recorded_locations);
    }
}
