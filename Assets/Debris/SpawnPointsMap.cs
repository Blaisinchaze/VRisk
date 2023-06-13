using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObjects/DebrisMeshSpawnPointMap")]
[System.Serializable]
public class SpawnPointsMap : ScriptableObject
{
    public Dictionary<BuildingManager.BuildingType, Dictionary<BuildingManager.BuildingState, List<SpawnPointData>>> map;
}

[System.Serializable]
public class SpawnPointData
{
    public SpawnPointData(Vector3 _spawn_point, Vector3 _direction, Vector3 _normal)
    {
        spawn_point = _spawn_point;
        direction = _direction;
        normal = _normal;
    }
    
    public Vector3 spawn_point;
    public Vector3 direction;
    public Vector3 normal;
}
