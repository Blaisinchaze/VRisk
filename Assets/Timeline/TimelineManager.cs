using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class TimelineManager : MonoBehaviour
{
    public string[] editorFilepath;
    public string[] androidFilepath;
    
    public DebrisTimeline debrisTimeline;
    public List<BuildingTimeslot> timeline = new List<BuildingTimeslot>();

    [SerializeField] private float timer = 0;

    private int debrisTimelineIndex = 0;
    public EarlyWarningSystem siren_system;

    void Awake()
    {
        ReadCSV();
        timeline.Sort((x, y) => x.triggerTime.CompareTo(y.triggerTime));
        debrisTimeline.timeline.Sort((x, y) => x.first.CompareTo(y.first));
    }

    private void Start()
    {
        /*siren_system.triggerWarningSiren(30);
        GameManager.Instance.BuildingManager.triggerGlobalShake(10,10,10);*/
        ///TEMP
        GameManager.Instance.BuildingManager.triggerGlobalShake(0.2f, 0.05f, 30);
        
        // For testing - remove later when the start of the rumble audio is triggered by the timeline manager. 
        GameManager.Instance.AudioManager.PlaySound(true, false, Vector3.zero,
            GameObject.FindGameObjectWithTag("MainCamera").transform, true, AudioManager.SoundID.SEISMIC_RUMBLE, 
            GameManager.earthquakeIntensityCurve, 30);
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
        
        if (timeline.First().triggerTime < timer)
        {
            var current = timeline.First();
            
            // Prompts building manager!
            // Need to replace with values read in, as opposed to hard coding them.
            GameManager.Instance.BuildingManager.damageBuilding(current.buildingId, current.intensity, current.shakingRepositionInterval,5, 40);
            // needs to trigger start of quake and start of siren.
        
            timeline.RemoveAt(0);
        }
    }

    private void updateDebrisTimeline()
    {
        if (debrisTimeline == null || debrisTimelineIndex >= debrisTimeline.timeline.Count  || debrisTimeline.timeline.Count < debrisTimelineIndex ) return;
        
        if (debrisTimeline.timeline[debrisTimelineIndex].first < timer)
        {
            GameManager.Instance.DebrisHandler.triggerDebris(debrisTimeline.timeline[debrisTimelineIndex].second);
            debrisTimelineIndex++;
        }
    }

    // Importing the CSV from the document -----------------------------------------------------
    
    void ReadCSV()
    {
        string[] entries = FileManager.parseCSVtoRows(editorFilepath, androidFilepath);

        for (int i = 1; i < entries.Length; i++)
        {
            string[] row = entries[i].Split(new string[] { "," }, StringSplitOptions.None);

            if (row.Length != 5) continue;
            
            int buildingId = int.Parse(row[0]);
            float triggerTime = float.Parse(row[1]);
            float intensity = float.Parse(row[2]);
            float shakingRepositionInterval = float.Parse(row[3]);
            float duration = float.Parse(row[4]);
            //Debug.Log($"Row: {row[0]}, {row[1]}, {row[2]}, {row[3]}, {row[4]}");

            var buildingData = new BuildingTimeslot(buildingId, triggerTime, intensity, shakingRepositionInterval, duration);
            timeline.Add(buildingData);
        }
    }
}