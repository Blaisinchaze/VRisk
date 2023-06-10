using System;
using System.Collections.Generic;
using UnityEngine;

public class DebrisHandler : MonoBehaviour
{
    public enum DebrisType
    {
        BRICK,
        CONCRETE_SLAB,
        CONCRETE_CHUNK,
        BRICKWORK_CHUNK,
        CINDER_BLOCK,
        
        COUNT
    }

    public List<Triple<DebrisType, GameObject, int>> debris_prefabs;
    private Dictionary<DebrisType, List<Pair<GameObject, DebrisScript>>> debris_pool;

    public float debris_lifetime = 0;
    public Vector3 debris_min_scale;

    private void Awake()
    {
        debris_pool = new Dictionary<DebrisType, List<Pair<GameObject, DebrisScript>>>();

        foreach (var triple in debris_prefabs)
        {
            for (int i = 0; i < triple.third; i++)
            {
                var debris_object = Instantiate(triple.second, transform, true);
                var debris_script = debris_object.GetComponent<DebrisScript>();

                if (!debris_pool.ContainsKey(triple.first))
                {
                    debris_pool.Add(triple.first, new List<Pair<GameObject, DebrisScript>>());
                }

                debris_pool[triple.first].Add(new Pair<GameObject, DebrisScript>(debris_object, debris_script));
            }
        }
    }

    public void triggerDebris(DebrisTimelineElement _debris_data)
    {
        if (!debris_pool.ContainsKey(_debris_data.type))
        {
            Debug.Log("Debris pool dictionary does not contain key " + _debris_data.type);
            return;
        }
        
        foreach (var debris in debris_pool[_debris_data.type])
        {
            if (!debris.first.activeSelf)
            {
                debris.first.SetActive(true);
                debris.first.transform.position = _debris_data.spawn_point;
                debris.first.GetComponent<Rigidbody>().AddForce(_debris_data.direction * _debris_data.force, ForceMode.Impulse);
                return;
            }
        }
        
        Debug.Log("No available " + _debris_data.type + " debris in the pool - consider increasing the pool");
    }
}
