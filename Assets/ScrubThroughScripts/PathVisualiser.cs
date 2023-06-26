using System;
using System.Collections.Generic;
using DataVisualiser;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PathVisualiser : MonoBehaviour
{
    public PlaythroughDataScript data;
    public List<Pair<float, GameObject>> locations;
    
    public float height_offset = 0;
    public Transform grid_origin;
    public Vector2 grid_cell_size;
    public GameObject test;

    private void Start()
    {
        grid_cell_size = new Vector2(GameManager.Instance.Data.grid_cell_size_x,
            GameManager.Instance.Data.grid_cell_size_y);
    }

    public void setData(PlaythroughDataScript _data)
    {
        clearLocations();
        data = _data;
        
        foreach (var element in data.timeline)
        {
            //var location_marker = new GameObject(element.time.ToString());
            var location_marker = Instantiate(test);

            var tile_location = element.grid_cell;
            var local_location = new Vector3(tile_location.x * grid_cell_size.x, height_offset, tile_location.y * grid_cell_size.y);


            var world_position = grid_origin.TransformPoint(local_location);
            location_marker.transform.position = world_position;
            locations.Add(new Pair<float, GameObject>(element.time, location_marker));
        }
    }

    private void clearLocations()
    {
        for (int i = locations.Count-1; i > -1; --i)
        {
            Destroy(locations[i].second);
        }
        
        locations.Clear();
    }
}
