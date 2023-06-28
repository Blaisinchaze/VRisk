using System.Collections;
using System.Collections.Generic;

/**
 * General Data Container to hold 3 T values
 */
[System.Serializable]
public class Triple<T1, T2, T3>
{
    public T1 first;
    public T2 second;
    public T3 third;

    public Triple(T1 _first, T2 _second, T3 _third)
    {
        first = _first;
        second = _second;
        third = _third;
    }
}

/**
 * General Data Container to hold 2 T values
 */
[System.Serializable]
public class Pair<T1, T2>
{
    public T1 first;
    public T2 second;

    public Pair(T1 _first, T2 _second)
    {
        first = _first;
        second = _second;
    }
}

/**
 * Holds the general information for each building in that timeslot of the building
 */
[System.Serializable]
public struct BuildingTimeslot
{
    public int buildingId;
    public float intensity;
    public float shakingRepositionInterval;
    public float duration;

    public BuildingTimeslot(int _buildingId, float _intensity, float _shakingRepositionInterval, float _duration)
    {
        buildingId = _buildingId;
        intensity = _intensity;
        shakingRepositionInterval = _shakingRepositionInterval;
        duration = _duration;
    }
}