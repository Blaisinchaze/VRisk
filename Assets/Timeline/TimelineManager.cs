using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Android;

public class TimelineManager : MonoBehaviour
{
    public BuildingManager building_manager;
    
    public TextAsset CSV_timeline;
    public List<Pair<int, float>> timeline;

    [SerializeField] private float timer = 0;
    
    void Awake()
    {
        ReadCSV();
        timeline.Sort((x, y) => x.second.CompareTo(y.second));
    }

    private void Start()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
        }

        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        if (timeline.Count == 0) return;
        
        if (timeline.First().second < timer)
        {
            //Prompts building manager!
            // Need to replace with values read in, as opposed to hard coding them.
            building_manager.damageBuilding(timeline.First().first, 0.2f, 0.05f, 1, 5, 40);

            timeline.RemoveAt(0);
        }
    }

    void ReadCSV()
    {
        string[] data = CSV_timeline.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int table_size = data.Length / 2 - 1;
        timeline = new List<Pair<int, float>>(table_size);
        
        for (int i = 0; i < table_size; i++)
        {
            timeline.Add(new Pair<int, float>(int.Parse(data[2 * (i + 1)]), float.Parse(data[2 * (i + 1) + 1])));
        }
    }
}
