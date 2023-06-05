using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObjects/DebrisTimeline")]
[System.Serializable]
public class DebrisTimeline : ScriptableObject
{
    public List<Pair<float, TimelineDebrisTrigger>> timeline;
}

[System.Serializable]
public struct TimelineDebrisTrigger
{
    public TimelineDebrisTrigger(Vector3 _spawn_point, Vector3 _direction, DebrisHandler.DebrisType _type)
    {
        spawn_point = _spawn_point;
        direction = _direction;
        type = _type;
    }
    
    public Vector3 spawn_point;
    public Vector3 direction;
    public DebrisHandler.DebrisType type;
}
