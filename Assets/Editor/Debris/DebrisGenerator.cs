/*
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Editor
{
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
    
    public class DebrisGenerator : EditorWindow
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
        
        private static float direction_range_around_normal;

        private static DebrisTimeline timeline;
        private static SpawnPointsMap mesh_spawn_points_map;
        private static bool generate_debug_game_objects = false;
        private static Dictionary<BuildingManager.BuildingState, List<Vector3>> debris_normals;
        private static List<GameObject> debug_objects;

        private static float max_force;
        private static float min_force;

        private static float debris_timeline_start;
        private static float debris_timeline_end;

        private static GameObject debug_prefab;
        private static GameObject debug_parent_prefab;
        private static GameObject debug_objects_parent;

        [MenuItem("Window/Debris Spawn Point Generator")]
        public static void ShowWindow()
        {
            GetWindow<DebrisGenerator>("Debris Spawn Point Generator");
        }

        private void OnEnable()
        {
            string[] ids = AssetDatabase.FindAssets("t:DebrisEditorData");
            if (ids.Length > 0)
            {
                string path = AssetDatabase.GUIDToAssetPath(ids[0]);
                data = AssetDatabase.LoadAssetAtPath<DebrisEditorData>(path);
            }

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
            
            direction_range_around_normal = data.direction_range_around_normal;

            max_force = data.max_force;
            min_force = data.min_force;

            debris_timeline_start = data.debris_timeline_start;
            debris_timeline_end = data.debris_timeline_end;

            timeline = data.timeline;
            mesh_spawn_points_map = data.mesh_spawn_points_map;
            generate_debug_game_objects = data.generate_debug_game_objects;
            debris_normals = new Dictionary<BuildingManager.BuildingState, List<Vector3>>();
            debris_normals = data.debris_normals;
            debug_objects = data.debug_objects;

            debug_prefab = data.debug_prefab;
            debug_parent_prefab = data.debug_parent_prefab;
            debug_objects_parent = GameObject.FindGameObjectWithTag("DebrisDebugParent");
        }

        private void OnDisable()
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
            
            data.direction_range_around_normal = direction_range_around_normal;

            data.max_force = max_force;
            data.min_force = min_force;

            data.debris_timeline_start = debris_timeline_start;
            data.debris_timeline_end = debris_timeline_end;

            data.timeline = timeline;
            data.mesh_spawn_points_map = mesh_spawn_points_map;
            data.generate_debug_game_objects = generate_debug_game_objects;
            data.debris_normals = debris_normals;
            data.debug_objects = debug_objects;

            data.debug_prefab = debug_prefab;
            data.debug_parent_prefab = debug_parent_prefab;
        }

        void OnGUI()
        {
            GUILayout.Label("Generate Debris Spawn Points", EditorStyles.boldLabel);
            
            EditorGUILayout.Space(20);
            
            safe_risk_tag = EditorGUILayout.TextField("Safe Risk Tag: ", safe_risk_tag);
            low_risk_tag = EditorGUILayout.TextField("Low Risk Tag: ", low_risk_tag);
            mid_risk_tag = EditorGUILayout.TextField("Mid Risk Tag: ", mid_risk_tag);
            high_risk_tag = EditorGUILayout.TextField("High Risk Tag: ", high_risk_tag);
            
            EditorGUILayout.Space(20);
            
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
            
            EditorGUILayout.Space(20);

            max_force = EditorGUILayout.FloatField("Max Force", max_force);
            min_force = EditorGUILayout.FloatField("Min Force", min_force);
            
            EditorGUILayout.Space(20);

            direction_range_around_normal = EditorGUILayout.FloatField("Direction Range Around Normal", direction_range_around_normal);
            
            EditorGUILayout.Space(20);

            debris_timeline_start = EditorGUILayout.FloatField("Timeline Start", debris_timeline_start);
            debris_timeline_end = EditorGUILayout.FloatField("Timeline End", debris_timeline_end);
            

            EditorGUILayout.Space(20);

            timeline = EditorGUILayout.ObjectField("Debris Timeline", timeline, typeof(DebrisTimeline), true) as DebrisTimeline;
            mesh_spawn_points_map = EditorGUILayout.ObjectField("Mesh|Spawnpoint Map", mesh_spawn_points_map, typeof(SpawnPointsMap), true) as SpawnPointsMap;
            debug_prefab = EditorGUILayout.ObjectField("Debug Prefab", debug_prefab, typeof(GameObject), true) as GameObject;
            debug_parent_prefab = EditorGUILayout.ObjectField("Debug Parent Prefab", debug_parent_prefab, typeof(GameObject), true) as GameObject;
            generate_debug_game_objects = EditorGUILayout.Toggle("Generate Debug Objects", generate_debug_game_objects);

            EditorGUILayout.Space(20);
            if (GUILayout.Button("Generate Spawn Points"))
            {
                generateSpawnPoints();
            }

            if (GUILayout.Button("Generate Directions"))
            {
                generateDirections();
            }

            if (GUILayout.Button("Generate Timeline Trigger Points"))
            {
                generateTimelinePoints();
            }

            if (GUILayout.Button("Generate Forces"))
            {
                generateForces();
            }

            if (GUILayout.Button("Generate Debris Types"))
            {
                generateDebrisTypes();
            }

            if (GUILayout.Button("Wipe Timeline"))
            {
                wipeTimeline();
            }
        }

        private void generateForces()
        {
            foreach (var element in timeline.timeline)
            {
                element.second.force = Random.Range(min_force, max_force);
            }
        }

        private void generateTimelinePoints()
        {
            Dictionary<BuildingManager.RiskLevel, GameObject[]> risk_buildings_sets = new Dictionary<BuildingManager.RiskLevel, GameObject[]>();

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
                   
                       timeline.timeline.Add(new Pair<float, DebrisTimelineElement>(timepoint, new DebrisTimelineElement(building_id, debris_index, 0.0f, DebrisHandler.DebrisType.BRICK)));
                   }
               }
           }
        }

        private void generateDirections()
        {
            foreach (var set in mesh_spawn_points_map.map)
            {
                for (int i = 0; i < mesh_spawn_points_map.map.Count; i++)
                {
                    Vector3 normal = debris_normals[set.first][i];

                    // create a random axis perpendicular to the normal
                    Vector3 axis = Vector3.Cross(normal, Random.onUnitSphere);

                    // generate a random angle within the specified range
                    float angle = Random.Range(-direction_range_around_normal, direction_range_around_normal);

                    // generate a quaternion representing a rotation around the axis by the angle
                    Quaternion rotation = Quaternion.AngleAxis(angle, axis);

                    // apply the rotation to the normal to get a direction vector
                    Vector3 direction = rotation * normal;

                    direction = direction.normalized;

                    set.third[i].direction = direction;
                }
            }
        }

        private void generateDebrisTypes()
        {
            for (int i = 0; i < timeline.timeline.Count; i++)
            {
                int index = Random.Range(0, (int) DebrisHandler.DebrisType.COUNT);
                timeline.timeline[i].second.type = (DebrisHandler.DebrisType) index;
            }
        }

        private void wipeTimeline()
        {
            timeline.timeline.Clear();
            mesh_spawn_points_map.map.Clear();
            debris_normals.Clear();

            /*debug_objects.Clear();

            if (!debug_objects_parent.IsUnityNull())
            {
                DestroyImmediate(debug_objects_parent);
                debug_objects_parent = null;
            }#1#
        }

        private void generateSpawnPoints()
        {
            foreach (var state_mesh_pair in mesh_spawn_points_map.map)
            {
                List<spawnPointData> mesh_spawns = new List<spawnPointData>();
                List<Vector3> normals = new List<Vector3>();

                // get triangles - return if there is none
                List<Pair<Triangle, Vector3>> triangles = getTriangles(state_mesh_pair.second);
                if (triangles.Count == 0) return;

                for (int i = 0; i < points_per_mesh; ++i)
                {
                    int index = Random.Range(0, triangles.Count);
                    Pair<Triangle, Vector3> triangle_and_normal = triangles[index];

                    // generate a random point inside the triangle
                    Vector3 point_in_triangle = randomPointInTriangle(triangle_and_normal.first.v1,
                        triangle_and_normal.first.v2, triangle_and_normal.first.v3);

                    normals.Add(triangle_and_normal.second);
                    //mesh_spawns.Add(new spawnPointData(point_in_triangle, Vector3.zero));
                }
                
                debris_normals.Add(state_mesh_pair.first, normals);
                state_mesh_pair.third.AddRange(mesh_spawns);
            }
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
        
        private Vector3 randomPointInTriangle(Vector3 v1, Vector3 v2, Vector3 v3) 
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
            Vector3 point = v1 + u * (v2 - v1) + v * (v3 - v1);
            return point;
        }
    }
}
*/

