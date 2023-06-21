using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimelineManager : MonoBehaviour
{
    public TextAsset CSV_timeline;
    public DebrisTimeline debris_timeline;
    public List<Pair<int, float>> timeline;

    [SerializeField] private float timer = 0;
    
    public float time_since_start { get; private set; }
    public TimerDisplay timer_display; 
    public bool quake_active = false;
    
    private int debris_timeline_index = 0;
    
    void Awake()
    {
        ReadCSV();
        timeline.Sort((x, y) => x.second.CompareTo(y.second));
        debris_timeline.timeline.Sort((x, y) => x.first.CompareTo(y.first));
    }

    private void Start()
    {
        ///TEMP
        GameManager.Instance.BuildingManager.triggerGlobalShake(0.2f, 0.05f, 30);
        
        // For testing - remove later when the start of the rumble audio is triggered by the timeline manager. 
        GameManager.Instance.AudioManager.PlaySound(true, false, Vector3.zero,
            GameObject.FindGameObjectWithTag("MainCamera").transform, true, AudioManager.SoundID.SEISMIC_RUMBLE, 
            GameManager.earthquakeIntensityCurve, 30);
    }

    private void Update()
    {
        if (quake_active)
        {
            time_since_start += Time.deltaTime;
            timer_display.updateTimer(time_since_start);
        }
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        updateBuildingTimeline();
        updateDebrisTimeline();
    }

    private void updateBuildingTimeline()
    {
        if (timeline.Count == 0) return;
        
        if (timeline.First().second < timer)
        {
            //Prompts building manager!
            // Need to replace with values read in, as opposed to hard coding them.
            GameManager.Instance.BuildingManager.damageBuilding(timeline.First().first, 0.2f, 0.05f, 1, 5, 40);
            
            // IF START QUAKE - SET quake_started TO TRUE;

            timeline.RemoveAt(0);
        }
    }

    private void updateDebrisTimeline()
    {
        if (debris_timeline == null || debris_timeline_index >= debris_timeline.timeline.Count  || debris_timeline.timeline.Count < debris_timeline_index ) return;

        if (debris_timeline.timeline[debris_timeline_index].first < timer)
        {
            GameManager.Instance.DebrisHandler.triggerDebris(debris_timeline.timeline[debris_timeline_index].second);
            debris_timeline_index++;
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
