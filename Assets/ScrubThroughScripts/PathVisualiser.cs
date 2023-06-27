using System;
using System.Collections.Generic;
using DataVisualiser;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PathVisualiser : MonoBehaviour
{
    public PlaythroughDataScript data;
    public List<Pair<float, GameObject>> locations;
    
    public float height_offset = 0;
    public Transform grid_origin;
    public Vector2 grid_cell_size;
    public GameObject location_marker_prefab;

    public Slider timeline_slider;
    public TMP_Text time_display;

    private LineRenderer line_renderer;
    [SerializeField] private float total_line_segments;
    [SerializeField] private float current_line_segment;
    [SerializeField] private int last_whole_segment;

    private void Start()
    {
        grid_cell_size = new Vector2(GameManager.Instance.Data.grid_cell_size_x,
            GameManager.Instance.Data.grid_cell_size_y);
    }

    public void setData(PlaythroughDataScript _data)
    {
        clearLocations();
        
        if (data != null)
        {
            data.display_data_button.interactable = true;
        }
        
        data = _data;
        data.display_data_button.interactable = false;
        
        foreach (var element in data.timeline)
        {
            var location_marker = Instantiate(location_marker_prefab);

            var tile_location = element.grid_cell;
            var local_location = new Vector3(tile_location.x * grid_cell_size.x, height_offset, tile_location.y * grid_cell_size.y);


            var world_position = grid_origin.TransformPoint(local_location);
            location_marker.transform.position = world_position;
            locations.Add(new Pair<float, GameObject>(element.time, location_marker));
        }
        
        updateSliderText(0.0f, data.completion_time);
    }

    private void Update()
    {
        if (data != null)
        {
            float current_time = timeline_slider.value * data.completion_time;
            
            updatePathVisuals(current_time);
            updateSliderText(current_time, data.completion_time);
        }
    }

    public void updateSliderText(float _current_seconds_from_start, float _total_time_from_start)
    {
        int total_minutes = (int) Math.Round(_total_time_from_start / 60);
        int total_seconds = (int)Math.Round(_total_time_from_start % 60);
        int total_milliseconds = (int)Math.Round(_total_time_from_start * 1000) % 1000;
        string total_time = total_minutes.ToString("D2") + "m : " + total_seconds.ToString("D2") + "s : " + total_milliseconds.ToString("D3") + "ms";

        int current_minutes = (int) Math.Round(_current_seconds_from_start / 60);;
        int current_seconds = (int)Math.Round(_current_seconds_from_start % 60);;
        int current_milliseconds = (int)Math.Round(_current_seconds_from_start * 1000) % 1000;
        string current_time = current_minutes.ToString("D2") + "m : " + current_seconds.ToString("D2") + "s : " + current_milliseconds.ToString("D3") + "ms";;

        time_display.text = current_time + " / " + total_time;
    }

    public void updatePathVisuals(float _time)
    {
        
    }

    public void clearLocations()
    {
        for (int i = locations.Count-1; i > -1; --i)
        {
            Destroy(locations[i].second);
        }
        
        locations.Clear();
    }
}
