using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class BuildingData : MonoBehaviour
{ 
    
    public int id;
    public MeshBuildingStateMap building_map;

    public BuildingManager.BuildingState state = BuildingManager.BuildingState.NO_DAMAGE;
    public Vector3 original_position;

    public MeshCollider collider;
    public MeshFilter mesh_filter;

    private void Awake()
    {
        collider = GetComponent<MeshCollider>();
        mesh_filter = GetComponent<MeshFilter>();
        original_position = transform.position;
    }
}
