using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public float direction_range_around_normal_y;
    public float direction_range_around_normal_x;

    public DebrisTimeline timeline;
    public bool generate_debug_game_objects = false;
    public List<Vector3> debris_normals;
    public List<Pair<GameObject, Vector3>> object_direction_map;

    public GameObject debug_prefab;
    public GameObject debug_parent_prefab;
}
