using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObjects/DebrisEditorData")]
public class DebrisEditorData : ScriptableObject
{
    public string safe_risk_tag = "Black";
    public string low_risk_tag = "Beige";
    public string mid_risk_tag = "Yellow";
    public string high_risk_tag = "Red";

    public int safe_risk_debris_max;
    public int low_risk_debris_max;
    public int mid_risk_debris_max;
    public int high_risk_debris_max;

    public int safe_risk_debris_min;
    public int low_risk_debris_min;
    public int mid_risk_debris_min;
    public int high_risk_debris_min;
    
    public float direction_range_around_normal;
    
    public float max_force;
    public float min_force;

    public float debris_timeline_start;
    public float debris_timeline_end;

    public DebrisTimeline timeline;
    public bool generate_debug_game_objects = false;
    public List<Vector3> debris_normals;
    [FormerlySerializedAs("object_direction_map")] public List<GameObject> debug_objects;

    public GameObject debug_prefab;
    public GameObject debug_parent_prefab;
}
