using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class BuildingData : MonoBehaviour
{ 
    public int id; 
    public BuildingManager.BuildingState state = BuildingManager.BuildingState.NO_DAMAGE;
    public MeshBuildingStateMap building_map;
    public bool transitioning = false;
    public float transition_duration = 4;
    public float transition_timer = 0;
    public Vector3 position;
}
