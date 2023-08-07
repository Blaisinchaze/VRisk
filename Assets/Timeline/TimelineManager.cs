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
    public List<float> debrisAccumulationStages;
    public EarlyWarningSystem sirenSystem;

    [SerializeField] private float timer = 0;

    // Global data available for the earthquake
    [SerializeField] private float sirenStartTime;
    [SerializeField] private float quakeStartTime;
    [SerializeField] private float maxGlobalIntensity;
    [SerializeField] private float shakingRepositionInterval;
    [SerializeField] private float globalDuration;
    [SerializeField] private bool sirenEnabled = false;

    private bool sirenTriggered = false;
    private bool quakeTriggered = false;
    private int debrisTimelineIndex = 0;

    void Awake()
    {
        ReadCSV();
        timeline.Sort((x, y) => x.triggerTime.CompareTo(y.triggerTime));
        debrisAccumulationStages.Sort();
        debrisTimeline.timeline.Sort((x, y) => x.first.CompareTo(y.first));
    }

    private void Start()
    {
        // /*siren_system.triggerWarningSiren(30);
        // GameManager.Instance.BuildingManager.triggerGlobalShake(10,10,10);*/
        // ///TEMP
        // GameManager.Instance.BuildingManager.triggerGlobalShake(0.2f, 0.05f, 30);
        //
        // // For testing - remove later when the start of the rumble audio is triggered by the timeline manager. 
        // GameManager.Instance.AudioManager.PlaySound(true, false, Vector3.zero,
        //     GameObject.FindGameObjectWithTag("MainCamera").transform, true, AudioManager.SoundID.SEISMIC_RUMBLE, 
        //     GameManager.earthquakeIntensityCurve, 30);
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        if (sirenStartTime < timer && !sirenTriggered)
        {
            sirenSystem.triggerWarningSiren(globalDuration + Mathf.Abs(quakeStartTime - sirenStartTime));
            sirenTriggered = true;
        }

        if (quakeStartTime < timer && !quakeTriggered)
        {
            GameManager.Instance.BuildingManager.triggerGlobalShake(maxGlobalIntensity, shakingRepositionInterval, globalDuration);
            GameManager.Instance.AudioManager.PlaySound(true, false, Vector3.zero,
                GameObject.FindGameObjectWithTag("MainCamera").transform, true, AudioManager.SoundID.SEISMIC_RUMBLE, 
                GameManager.earthquakeIntensityCurve, globalDuration);
            quakeTriggered = true;
        }

        UpdateDebrisFloorTimeline();
        updateBuildingTimeline();
        //updateDebrisTimeline();
    }

    private void updateBuildingTimeline()
    {
        if (timeline.Count == 0) return;
        
        if (timeline.First().triggerTime + quakeStartTime < timer)
        {
            var current = timeline.First();
            
            // Prompts building manager!
            // Need to replace with values read in, as opposed to hard coding them.
            //Debug.Log(current.buildingId);
            GameManager.Instance.BuildingManager.damageBuilding(current.buildingId, current.intensity, current.shakingRepositionInterval,5, 40);
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

    //Atm unused
    private void UpdateDebrisFloorTimeline()
    {
        if (debrisAccumulationStages.Count == 0) return;      
        
        if (debrisAccumulationStages.First() + quakeStartTime <= timer)
        {
            // stuff
            Debug.Log(debrisAccumulationStages.First());
            debrisAccumulationStages.RemoveAt(0);
        }
    }

    // Importing the CSV from the document -----------------------------------------------------
    
    void ReadCSV()
    {
        string[] entries = FileManager.parseCSVtoRows(editorFilepath, androidFilepath);

        //Gathers the global variables
        string[] globalRow = entries[1].Split(new string[] { "," }, StringSplitOptions.None);
        
        //Splits by comma and saves
        sirenStartTime = float.Parse(globalRow[0]);
        quakeStartTime = float.Parse(globalRow[1]);
        maxGlobalIntensity = float.Parse(globalRow[2]);
        shakingRepositionInterval = float.Parse(globalRow[3]);
        globalDuration = float.Parse(globalRow[4]);
        sirenEnabled = float.Parse(globalRow[5]) != 0;

        //Gathers the accumulation stages in time of the debris
        string[] stagesRow = entries[4].Split(new string[] { "," }, StringSplitOptions.None);
        //Adds it to a list
        foreach (var timeframe in stagesRow)
        {
            debrisAccumulationStages.Add(float.Parse(timeframe));
        }

        //starts at 7 to gather the timeline data
        for (int i = 7; i < entries.Length; i++)
        {
            string[] row = entries[i].Split(new string[] { "," }, StringSplitOptions.None);
            
            if (row.Length != 6) continue;
            
            int buildingId = int.Parse(row[0]);
            float triggerTime = float.Parse(row[1]);
            float intensity = float.Parse(row[2]);
            float shakingRepositionInterval = float.Parse(row[3]);
            //Debug.Log($"Row: {row[0]}, {row[1]}, {row[2]}, {row[3]}, {row[4]}");

            var buildingData = new BuildingTimeslot(buildingId, triggerTime, intensity, shakingRepositionInterval);
            timeline.Add(buildingData);
        }
    }
}