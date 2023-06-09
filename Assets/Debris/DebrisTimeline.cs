using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObjects/DebrisTimeline")]
[System.Serializable]
public class DebrisTimeline : ScriptableObject
{
    public List<Pair<float, DebrisTimelineElement>> timeline;
}

[System.Serializable]
public class DebrisTimelineElement
{
    public DebrisTimelineElement(Vector3 _spawn_point, Vector3 _direction, float _force, DebrisHandler.DebrisType _type)
    {
        spawn_point = _spawn_point;
        direction = _direction;
        type = _type;
        force = _force;
    }
    
    
    
    public Vector3 spawn_point;
    public Vector3 direction;
    public float force;
    public DebrisHandler.DebrisType type;
}


/*
 * Add editor warning if size of normals differs from size of debris.
 * Add direction from normal to timeline data.
 * 
*/