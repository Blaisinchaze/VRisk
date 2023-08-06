using UnityEngine;

public class BuildingData : MonoBehaviour
{
    public int id;
    public MeshBuildingStateMap building_map;

    public BuildingManager.BuildingType type = BuildingManager.BuildingType.BASIC_SET;
    public BuildingManager.BuildingState state = BuildingManager.BuildingState.NO_DAMAGE;
    public Vector3 original_position;

    public MeshCollider building_collider;
    public MeshFilter mesh_filter;
    public MeshRenderer mesh_renderer;

    private void Awake()
    {
        //building_collider = GetComponent<MeshCollider>();
        mesh_renderer = GetComponent<MeshRenderer>();
        //mesh_filter = GetComponent<MeshFilter>();

        original_position = transform.position;
    }
}
