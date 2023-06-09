using System;
using System.Collections.Generic;
using UnityEngine;

public class DebrisHandler : MonoBehaviour
{
    public enum DebrisType
    {
        BRICK,
        COUNT
    }

    public List<Triple<DebrisType, GameObject, int>> debris_prefabs;

    private void Awake()
    {
        foreach (var triple in debris_prefabs)
        {
            //triple.
        }
    }

    public void triggerDebris(DebrisTimelineElement _debris_data)
    {
        GameObject prefab = null;
        foreach (var element in debris_prefabs)
        {
            if (element.first == _debris_data.type)
            {
                prefab = element.second;
                break;
            }
        }

        if (prefab == null) return;

        GameObject debris_object = Instantiate(prefab);
        debris_object.transform.position = _debris_data.spawn_point;
        debris_object.GetComponent<Rigidbody>().AddForce(_debris_data.direction * _debris_data.force, ForceMode.Impulse);
    }
}
