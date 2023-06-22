using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DataTracker : MonoBehaviour
{
    [SerializeField] private float timer;
    public TimerDisplay timer_display;
    
    public float record_position_interval = 0;
    public float record_position_timer;

    private bool active = true;

    private List<List<string>> _recorded_locations;

    private void Start()
    {
        _recorded_locations = new List<List<string>>();
        record_position_interval = GameManager.Instance.Data.record_position_interval;
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
                _recorded_locations.Add(new List<string> {timer.ToString(), Vector2.zero.ToString()});
                record_position_timer = 0f;
            }
        }
    }

    private void test(InputAction.CallbackContext _context)
    {
        recordTime(true);
    }

    public void recordTime(bool _survived)
    {
        active = false;

        string survived = _survived ? "Survived" : "Died";
        _recorded_locations.Add(new List<string> {timer.ToString(), Vector2.zero.ToString(), survived});
        
        FileManager.saveToCSV(new []{"GameManager", "record.csv"}, new []{"test.csv"} ,_recorded_locations);
    }
}
