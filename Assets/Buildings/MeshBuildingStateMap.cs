using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObjects/BuildingStateMeshMap")]
public class MeshBuildingStateMap : ScriptableObject
{
    public List<Pair<BuildingManager.BuildingState, Mesh>> states;
}
