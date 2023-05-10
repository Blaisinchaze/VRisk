using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimelineManager : MonoBehaviour
{
    public TextAsset CSV_timeline;
    public List<Pair<int, float>> timeline;

    [SerializeField] private float timer = 0;
    
    void Awake()
    {
        ReadCSV();
        timeline.Sort((x, y) => x.second.CompareTo(y.second));
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        if (timeline.Count == 0) return;
        
        if (timeline.First().second < timer)
        {
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