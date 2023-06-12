using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public struct Triangle
{
    public Triangle(Vector3 _v1, Vector3 _v2, Vector3 _v3)
    {
        v1 = _v1;
        v2 = _v2;
        v3 = _v3;
    }
        
    public Vector3 v1;
    public Vector3 v2;
    public Vector3 v3;
}

public class DebrisEditor : EditorWindow
{
    private DebrisEditorData data;

    private static string safe_risk_tag = "Black";
    private static string low_risk_tag = "Beige";
    private static string mid_risk_tag = "Yellow";
    private static string high_risk_tag = "Red";

    private static int safe_risk_debris_max;
    private static int low_risk_debris_max;
    private static int mid_risk_debris_max;
    private static int high_risk_debris_max;

    private static int safe_risk_debris_min;
    private static int low_risk_debris_min;
    private static int mid_risk_debris_min;
    private static int high_risk_debris_min;

    private static int points_per_mesh;

    private static float direction_max_horizontal_rotation;
    private static float direction_min_horizontal_rotation;
    private static float direction_max_vertical_rotation;
    private static float direction_min_vertical_rotation;
    
    private static float max_force;
    private static float min_force;
    
    private static float debris_timeline_start;
    private static float debris_timeline_end;
    
    private static DebrisTimeline timeline;
    private static SpawnPointsMap mesh_spawn_points_map;
    private static List<MeshBuildingStateMap> building_maps;

    private static GameObject debug_prefab;
    private static GameObject debug_parent_prefab;
    private static GameObject debug_objects_parent;

    [MenuItem("Window/Debris Data Editor")]
    public static void ShowWindow()
    {
        GetWindow<DebrisEditor>("Debris Data Editor");
    }

    private void OnEnable()
    {
        string[] ids = AssetDatabase.FindAssets("t:DebrisEditorData");
        if (ids.Length > 0)
        {
            string path = AssetDatabase.GUIDToAssetPath(ids[0]);
            data = AssetDatabase.LoadAssetAtPath<DebrisEditorData>(path);
        }

        if (!data.IsUnityNull())
        {
            safe_risk_tag = data.safe_risk_tag;
            low_risk_tag = data.low_risk_tag;
            mid_risk_tag = data.mid_risk_tag;
            high_risk_tag = data.high_risk_tag;

            safe_risk_debris_max = data.safe_risk_debris_max;
            low_risk_debris_max = data.low_risk_debris_max;
            mid_risk_debris_max = data.mid_risk_debris_max;
            high_risk_debris_max = data.high_risk_debris_max;

            safe_risk_debris_min = data.safe_risk_debris_min;
            low_risk_debris_min = data.low_risk_debris_min;
            mid_risk_debris_min = data.mid_risk_debris_min;
            high_risk_debris_min = data.high_risk_debris_min;

            points_per_mesh = data.points_per_mesh;

            direction_max_horizontal_rotation = data.direction_max_horizontal_rotation;
            direction_min_horizontal_rotation = data.direction_min_horizontal_rotation;
            direction_max_vertical_rotation = data.direction_max_vertical_rotation;
            direction_min_vertical_rotation = data.direction_min_vertical_rotation;

            timeline = data.timeline;
            mesh_spawn_points_map = data.mesh_spawn_points_map;
            building_maps = data.building_maps;

            max_force = data.max_force;
            min_force = data.min_force;

            debris_timeline_start = data.debris_timeline_start;
            debris_timeline_end = data.debris_timeline_end;

            debug_prefab = data.debug_prefab;
            debug_parent_prefab = data.debug_parent_prefab;
        }
        
        debug_objects_parent = GameObject.FindGameObjectWithTag("DebrisDebugParent");

        EditorApplication.quitting += onUnityClose;
    }

    private void OnDisable()
    {
        if (!data.IsUnityNull())
        {
            data.safe_risk_tag = safe_risk_tag;
            data.low_risk_tag = low_risk_tag;
            data.mid_risk_tag = mid_risk_tag;
            data.high_risk_tag = high_risk_tag;

            data.safe_risk_debris_max = safe_risk_debris_max;
            data.low_risk_debris_max = low_risk_debris_max;
            data.mid_risk_debris_max = mid_risk_debris_max;
            data.high_risk_debris_max = high_risk_debris_max;

            data.safe_risk_debris_min = safe_risk_debris_min;
            data.low_risk_debris_min = low_risk_debris_min;
            data.mid_risk_debris_min = mid_risk_debris_min;
            data.high_risk_debris_min = high_risk_debris_min;

            data.points_per_mesh = points_per_mesh;

            data.direction_max_horizontal_rotation = direction_max_horizontal_rotation;
            data.direction_min_horizontal_rotation = direction_min_horizontal_rotation;
            data.direction_max_vertical_rotation = direction_max_vertical_rotation;
            data.direction_min_vertical_rotation = direction_min_vertical_rotation;

            data.timeline = timeline;
            data.mesh_spawn_points_map = mesh_spawn_points_map;
            data.building_maps = building_maps;

            data.max_force = max_force;
            data.min_force = min_force;

            data.debris_timeline_start = debris_timeline_start;
            data.debris_timeline_end = debris_timeline_end;

            data.debug_prefab = debug_prefab;
            data.debug_parent_prefab = debug_parent_prefab;
        }
        
        EditorApplication.quitting -= onUnityClose;
    }

    private void onUnityClose()
    {
        EditorUtility.SetDirty(data);
        AssetDatabase.SaveAssets();
        EditorApplication.quitting -= onUnityClose;
    }
    
    private void OnGUI()
    {
        GUILayout.Label("Generate Debris Spawn Points", EditorStyles.boldLabel);
            
        EditorGUILayout.Space(10);
            
        safe_risk_tag = EditorGUILayout.TextField("Safe Risk Tag: ", safe_risk_tag);
        low_risk_tag = EditorGUILayout.TextField("Low Risk Tag: ", low_risk_tag);
        mid_risk_tag = EditorGUILayout.TextField("Mid Risk Tag: ", mid_risk_tag);
        high_risk_tag = EditorGUILayout.TextField("High Risk Tag: ", high_risk_tag);
            
        EditorGUILayout.Space(10);
        
        safe_risk_debris_max = EditorGUILayout.IntField("Safe Risk Debris Max", safe_risk_debris_max);
        low_risk_debris_max = EditorGUILayout.IntField("Low Risk Debris Max", low_risk_debris_max);
        mid_risk_debris_max = EditorGUILayout.IntField("Mid Risk Debris Max", mid_risk_debris_max);
        high_risk_debris_max = EditorGUILayout.IntField("High Risk Debris Max", high_risk_debris_max);
            
        EditorGUILayout.Space(10);
        
        safe_risk_debris_min = EditorGUILayout.IntField("Safe Risk Debris Min", safe_risk_debris_min);
        low_risk_debris_min = EditorGUILayout.IntField("Low Risk Debris Min", low_risk_debris_min);
        mid_risk_debris_min = EditorGUILayout.IntField("Mid Risk Debris Min", mid_risk_debris_min);
        high_risk_debris_min = EditorGUILayout.IntField("High Risk Debris Min", high_risk_debris_min);
            
        EditorGUILayout.Space(10);
        
        points_per_mesh = EditorGUILayout.IntField("Point Options Per Mesh", points_per_mesh);
            
        EditorGUILayout.Space(10);
        
        max_force = EditorGUILayout.FloatField("Max Force", max_force);
        min_force = EditorGUILayout.FloatField("Min Force", min_force);
            
        EditorGUILayout.Space(10);

        direction_max_horizontal_rotation = EditorGUILayout.FloatField("Direction Up|Down Max", 
            direction_max_horizontal_rotation);
        direction_min_horizontal_rotation = EditorGUILayout.FloatField("Direction Up|Down Min -", 
            direction_min_horizontal_rotation);
        direction_max_vertical_rotation = EditorGUILayout.FloatField("Direction Left|Right max", 
            direction_max_vertical_rotation);
        direction_min_vertical_rotation = EditorGUILayout.FloatField("Direction Left|Right Min -", 
            direction_min_vertical_rotation);
        
        EditorGUILayout.Space(10);

        debris_timeline_start = EditorGUILayout.FloatField("Timeline Start", debris_timeline_start);
        debris_timeline_end = EditorGUILayout.FloatField("Timeline End", debris_timeline_end);

        EditorGUILayout.Space(10);
        
        timeline = EditorGUILayout.ObjectField("Debris Timeline", timeline, typeof(DebrisTimeline), 
                true) as DebrisTimeline;
        mesh_spawn_points_map = EditorGUILayout.ObjectField("Mesh|Spawnpoint Map", mesh_spawn_points_map, 
            typeof(SpawnPointsMap), true) as SpawnPointsMap;
        
        EditorGUILayout.Space(10);

        if (GUILayout.Button("Add new BuildingMap"))
        {
            building_maps.Add(null);
        }

        for (int i = 0; i < building_maps.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();

            building_maps[i] = (MeshBuildingStateMap)EditorGUILayout.ObjectField(building_maps[i], 
                typeof(MeshBuildingStateMap), true);

            if (GUILayout.Button("Remove"))
            {
                building_maps.RemoveAt(i);
            }

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space(10);
        
        debug_prefab = EditorGUILayout.ObjectField("Debug Prefab", debug_prefab, typeof(GameObject), 
            true) as GameObject;
        debug_parent_prefab = EditorGUILayout.ObjectField("Debug Parent Prefab", debug_parent_prefab, 
            typeof(GameObject), true) as GameObject;
        
        EditorGUILayout.Space(20);

        if (GUILayout.Button("Generate Spawn Points"))
        {
            generateSpawnPoints();
        }

        if (GUILayout.Button("Generate Directions"))
        {
            generateDirections();
        }
        
        EditorGUILayout.Space(10);

        if (GUILayout.Button("Generate Timeline"))
        {
            generateTimeline();
        }

        if (GUILayout.Button("Generate Forces"))
        {
            generateForces();
        }

        if (GUILayout.Button("Generate Debris Types"))
        {
            generateDebrisTypes();
        }

        if (GUILayout.Button("Generate Debug Visuals"))
        {
            generateDebugVisuals();
        }
        
        EditorGUILayout.Space(10);

        if (GUILayout.Button("Clear Debug Visuals"))
        {
            clearDebugVisuals();
        }

        if (GUILayout.Button("Clear All"))
        {
            clearAll();
        }
    }

    private void generateSpawnPoints()
    {
        mesh_spawn_points_map.map.Clear();
        
        // List of building maps - maps meshes to each building state.
        // Have multiple because we might end up with more than one building set.
        foreach (var map in building_maps)
        {
            // For each mesh for the different states for that building. 
            foreach (var state_mesh_pair in map.states)
            {
                List<spawnPointData> spawn_points = new List<spawnPointData>();

                List<Pair<Triangle, Vector3>> triangles_and_normals = getTriangles(state_mesh_pair.third);
                if (triangles_and_normals.Count == 0) continue;

                for (int i = 0; i < points_per_mesh; i++)
                {
                    int index = Random.Range(0, triangles_and_normals.Count);
                    Pair<Triangle, Vector3> selected_triangle_and_normal = triangles_and_normals[index];
                    
                    // Generate a random point inside the triangle.
                    Vector3 point_in_triangle = randomPointInTriangle(selected_triangle_and_normal.first);
                    
                    spawn_points.Add(new spawnPointData(point_in_triangle, Vector3.zero, 
                        selected_triangle_and_normal.second));
                }
                
                mesh_spawn_points_map.map.Add(new Triple<BuildingManager.BuildingType, BuildingManager.BuildingState, 
                    List<spawnPointData>>(state_mesh_pair.first, state_mesh_pair.second, spawn_points));
            }
        }
    }

    private void generateDirections()
    {
        foreach (var element in mesh_spawn_points_map.map)
        {
            for (int i = 0; i < element.third.Count; i++)
            {
                Vector3 normal = element.third[i].normal;

                // Cross product to get the local x axis
                Vector3 local_right = Vector3.Normalize(Vector3.Cross(Vector3.up, normal));
                // Cross product to get the local y axis
                Vector3 localUp = Vector3.Cross(normal, local_right);

                float horizontal_angle = Random.Range(-direction_min_horizontal_rotation, direction_max_horizontal_rotation);
                float vertical_angle = Random.Range(-direction_min_vertical_rotation, direction_max_vertical_rotation);

                // Creating the rotations around the local right and up directions
                Quaternion rotationRight = Quaternion.AngleAxis(horizontal_angle, local_right);
                Quaternion rotationUp = Quaternion.AngleAxis(vertical_angle, localUp);

                // Combining the two rotations
                Quaternion rotation = rotationRight * rotationUp;

                Vector3 direction = rotation * normal;
                direction = direction.normalized;
                element.third[i].direction = direction;
            }
        }
    }

    private void generateTimeline()
    {
        Dictionary<BuildingManager.RiskLevel, GameObject[]> risk_buildings_sets =
            new Dictionary<BuildingManager.RiskLevel, GameObject[]>();

        risk_buildings_sets.Add(BuildingManager.RiskLevel.SAFE, GameObject.FindGameObjectsWithTag(safe_risk_tag));
        risk_buildings_sets.Add(BuildingManager.RiskLevel.LOW, GameObject.FindGameObjectsWithTag(low_risk_tag));
        risk_buildings_sets.Add(BuildingManager.RiskLevel.MID, GameObject.FindGameObjectsWithTag(mid_risk_tag));
        risk_buildings_sets.Add(BuildingManager.RiskLevel.HIGH, GameObject.FindGameObjectsWithTag(high_risk_tag));
        foreach (var set in risk_buildings_sets)
        {
            foreach (var building in set.Value)
            {
                int debris_spawns_count = 0;

                switch (set.Key)
                {
                    case BuildingManager.RiskLevel.SAFE:
                        debris_spawns_count = Random.Range(safe_risk_debris_min, safe_risk_debris_max);
                        break;

                    case BuildingManager.RiskLevel.LOW:
                        debris_spawns_count = Random.Range(low_risk_debris_min, low_risk_debris_max);
                        break;

                    case BuildingManager.RiskLevel.MID:
                        debris_spawns_count = Random.Range(mid_risk_debris_min, mid_risk_debris_max);
                        break;

                    case BuildingManager.RiskLevel.HIGH:
                        debris_spawns_count = Random.Range(high_risk_debris_min, high_risk_debris_max);
                        break;

                    default:
                        throw new System.ArgumentOutOfRangeException();
                }

                for (int i = 0; i < debris_spawns_count; i++)
                {
                    float timepoint = Random.Range(debris_timeline_start, debris_timeline_end);
                    int building_id = building.GetComponent<BuildingData>().id;
                    int debris_index = Random.Range(0, points_per_mesh);

                    timeline.timeline.Add(new Pair<float, DebrisTimelineElement>(timepoint,
                        new DebrisTimelineElement(building_id, debris_index, 0.0f, DebrisHandler.DebrisType.BRICK)));
                }
            }
        }
    }

    private void generateForces()
    {
        foreach (var element in timeline.timeline)
        {
            element.second.force = Random.Range(min_force, max_force);
        }
    }

    private void generateDebrisTypes()
    {
        for (int i = 0; i < timeline.timeline.Count; i++)
        {
            int index = Random.Range(0, (int) DebrisHandler.DebrisType.COUNT);
            timeline.timeline[i].second.type = (DebrisHandler.DebrisType)index;
        }
    }

    private void generateDebugVisuals()
    {
        
    }

    private void clearDebugVisuals()
    {
        
    }

    private void clearAll()
    {
        mesh_spawn_points_map.map.Clear();
        timeline.timeline.Clear();
    }
    
    
    private List<Pair<Triangle, Vector3>> getTriangles(Mesh _mesh)
    {
        List<Pair<Triangle, Vector3>> triangles = new List<Pair<Triangle, Vector3>>();
            
        for (int x = 0; x < _mesh.triangles.Length; x += 3)
        {
            // get the vertices of the triangle
            Vector3 v1 = _mesh.vertices[_mesh.triangles[x]];
            Vector3 v2 = _mesh.vertices[_mesh.triangles[x + 1]];
            Vector3 v3 = _mesh.vertices[_mesh.triangles[x + 2]];

            Triangle triangle = new Triangle(v1, v2, v3);
                
            // continue if triangle's normal matches the y axis
            Vector3 normal = getNormal(triangle);
            if (isYAxisFacing(normal)) continue;
                
            triangles.Add(new Pair<Triangle, Vector3>(triangle, normal));
        }
            
        return triangles;
    }
    
    private Vector3 getNormal(Triangle _triangle)
    {
        // calculate the triangle's normal
        Vector3 edge_1 = _triangle.v2 - _triangle.v1;
        Vector3 edge_2 = _triangle.v3 - _triangle.v1;
        Vector3 normal = Vector3.Cross(edge_1, edge_2).normalized;

        return normal;
    }
    
    private bool isYAxisFacing(Vector3 _normal)
    {
        // if the normal is not parallel to the world's up or down direction, generate a spawn point
        if (Mathf.Abs(Vector3.Dot(_normal, Vector3.up)) < 0.9f && Mathf.Abs(Vector3.Dot(_normal, Vector3.down)) < 0.9f)
        {
            return false;
        }
            
        return true;
    }
    
    private Vector3 randomPointInTriangle(Triangle _triangle) 
    {
        // generate two random numbers
        float u = UnityEngine.Random.value;
        float v = UnityEngine.Random.value;

        // ensure the point is inside the triangle
        if (u + v > 1) 
        {
            u = 1 - u;
            v = 1 - v;
        }

        // calculate the point's position
        Vector3 point = _triangle.v1 + u * (_triangle.v2 - _triangle.v1) + v * (_triangle.v3 - _triangle.v1);
        return point;
    }
}
