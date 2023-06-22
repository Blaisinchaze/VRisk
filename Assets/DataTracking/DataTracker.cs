using System;
using System.Collections.Generic;
using UnityEngine;

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

    private GameData data;

    public List<string> editor_folder_path;
    public List<string> android_folder_path;
    private string[] editor_file_path;
    private string[] android_file_path;

    private void Start()
    {
        _recorded_locations = new List<List<string>>();

        editor_file_path = new string[editor_folder_path.Count];

        editor_file_path = new string[editor_folder_path.Count + 1];
        for (int i = 0; i < editor_folder_path.Count; i++)
        {
            editor_file_path[i] = editor_folder_path[i];
        }
        editor_folder_path.Clear();
        
        android_file_path = new string[android_folder_path.Count + 1];
        for (int i = 0; i < android_folder_path.Count; i++)
        {
            android_file_path[i] = android_folder_path[i];
        }
        android_folder_path.Clear();

        data = GameManager.Instance.Data;
        record_position_interval = data.record_position_interval;
        grid_cell_size = new Vector2(data.grid_cell_size_x, data.grid_cell_size_y);
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

    public void recordTime(bool _survived)
    {
        active = false;

        string survived = _survived ? "Survived" : "Died";
        _recorded_locations.Add(new List<string> {timer.ToString(), getGridLocation().ToString(), survived});

        DateTime date_time = DateTime.Now;
        
        string file_friendly_date_time = date_time.ToString("yyyy-MM-dd  (HH-mm-ss)");
        string file_name = file_friendly_date_time + "  -  " + data.user_name + ".csv";

        editor_file_path[editor_file_path.Length - 1] = file_name;
        android_file_path[android_file_path.Length - 1] = file_name;
        
        FileManager.saveToCSV(editor_file_path, android_file_path ,_recorded_locations);
    }
}
