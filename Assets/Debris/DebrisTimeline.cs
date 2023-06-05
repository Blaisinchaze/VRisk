using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObjects/DebrisTimeline")]
public class DebrisTimeline : ScriptableObject
{
    public Transform spawn_point;
    public Vector3 direction;
    public DebrisHandler.DebrisType type;
    public float trigger_time;
}
