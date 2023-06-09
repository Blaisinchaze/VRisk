using System.Collections.Generic;
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
        
        private static float direction_range_around_normal;

        private static DebrisTimeline timeline;
        private static bool generate_debug_game_objects = false;
        private static List<Vector3> debris_normals;
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
            
            direction_range_around_normal = data.direction_range_around_normal;

            max_force = data.max_force;
            min_force = data.min_force;

            debris_timeline_start = data.debris_timeline_start;
            debris_timeline_end = data.debris_timeline_end;

            timeline = data.timeline;
            generate_debug_game_objects = data.generate_debug_game_objects;
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

            data.direction_range_around_normal = direction_range_around_normal;

            data.max_force = max_force;
            data.min_force = min_force;

            data.debris_timeline_start = debris_timeline_start;
            data.debris_timeline_end = debris_timeline_end;

            data.timeline = timeline;
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
            foreach (var element in timeline.timeline)
            {
                element.first = Random.Range(debris_timeline_start, debris_timeline_end);
            }
        }

        private void generateDirections()
        {
            for (int i = 0; i < timeline.timeline.Count; i++)
            {
                Vector3 normal = debris_normals[i];

                // create a random axis perpendicular to the normal
                Vector3 axis = Vector3.Cross(normal, Random.onUnitSphere);

                // generate a random angle within the specified range
                float angle = Random.Range(-direction_range_around_normal, direction_range_around_normal);

                // generate a quaternion representing a rotation around the axis by the angle
                Quaternion rotation = Quaternion.AngleAxis(angle, axis);

                // apply the rotation to the normal to get a direction vector
                Vector3 direction = rotation * normal;

                direction = direction.normalized;

                timeline.timeline[i].second.direction = direction;

                if (!debug_objects.IsUnityNull() && debug_objects.Count > 0)
                {
                    debug_objects[i].GetComponent<DebugPrefabScript>().direction = direction;
                }
            }
        }

        private void generateDebrisTypes()
        {
            for (int i = 0; i < timeline.timeline.Count; i++)
            {
                int index = UnityEngine.Random.Range(0, (int) DebrisHandler.DebrisType.COUNT);
                timeline.timeline[i].second.type = (DebrisHandler.DebrisType) index;
            }
        }

        private void wipeTimeline()
        {
            timeline.timeline.Clear();
            debris_normals.Clear();
            debug_objects.Clear();

            if (!debug_objects_parent.IsUnityNull())
            {
                DestroyImmediate(debug_objects_parent);
                debug_objects_parent = null;
            }
        }

        private void generateSpawnPoints()
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
                            debris_spawns_count = UnityEngine.Random.Range(safe_risk_debris_min, safe_risk_debris_max);
                            break;
                        
                        case BuildingManager.RiskLevel.LOW:
                            debris_spawns_count = UnityEngine.Random.Range(low_risk_debris_min, low_risk_debris_max);
                            break;
                        
                        case BuildingManager.RiskLevel.MID:
                            debris_spawns_count = UnityEngine.Random.Range(mid_risk_debris_min, mid_risk_debris_max);
                            break;
                        
                        case BuildingManager.RiskLevel.HIGH:
                            debris_spawns_count = UnityEngine.Random.Range(high_risk_debris_min, high_risk_debris_max);
                            break;
                        
                        default:
                            throw new System.ArgumentOutOfRangeException();
                    }

                    // get triangles - return if there is none
                    List<Pair<Triangle, Vector3>> triangles = getTriangles(building);
                    if (triangles.Count == 0) return;

                    for (int i = 0; i < debris_spawns_count; ++i)
                    {
                        int index = UnityEngine.Random.Range(0, triangles.Count);
                        Pair<Triangle, Vector3> triangle_and_normal = triangles[index];
                        
                        // generate a random point inside the triangle
                        Vector3 point_in_triangle = randomPointInTriangle(triangle_and_normal.first.v1, triangle_and_normal.first.v2, triangle_and_normal.first.v3);
                        Vector3 spawn_point_position = building.transform.TransformPoint(point_in_triangle);

                        // instantiate a game object at the spawn point
                        if (generate_debug_game_objects)
                        {
                            if (debug_objects_parent.IsUnityNull())
                            {
                                debug_objects_parent = Instantiate(debug_parent_prefab);
                            }
                            
                            GameObject spawn_point;
                            if (debug_prefab.IsUnityNull())
                            {
                                spawn_point = new GameObject("Debris Spawn Point");
                            }
                            else
                            {
                                spawn_point = Instantiate(debug_prefab);
                                debug_objects.Add(spawn_point);
                            }
                            
                            spawn_point.transform.SetParent(debug_objects_parent.transform);
                            spawn_point.transform.position = spawn_point_position;
                        }

                        debris_normals.Add(triangle_and_normal.second);
                        timeline.timeline.Add(new Pair<float, DebrisTimelineElement>(10, new DebrisTimelineElement(spawn_point_position, new Vector3(0,0,0), 0, DebrisHandler.DebrisType.BRICK)));
                    }
                }
            }
        }

        private List<Pair<Triangle, Vector3>> getTriangles(GameObject _building)
        {
            MeshFilter mesh_filter = _building.GetComponent<MeshFilter>();
            if (mesh_filter.IsUnityNull()) return null;
                    
            Mesh mesh = mesh_filter.sharedMesh;
            List<Pair<Triangle, Vector3>> triangles = new List<Pair<Triangle, Vector3>>();
            
            for (int x = 0; x < mesh.triangles.Length; x += 3)
            {
                // get the vertices of the triangle
                Vector3 v1 = mesh.vertices[mesh.triangles[x]];
                Vector3 v2 = mesh.vertices[mesh.triangles[x + 1]];
                Vector3 v3 = mesh.vertices[mesh.triangles[x + 2]];

                Triangle triangle = new Triangle(v1, v2, v3);
                
                // continue if triangle's normal matches the y axis
                Vector3 normal = getNormal(triangle, _building);
                if (isYAxisFacing(normal)) continue;
                
                triangles.Add(new Pair<Triangle, Vector3>(triangle, normal));
            }
            
            return triangles;
        }

        private Vector3 getNormal(Triangle _triangle, GameObject _building)
        {
            // calculate the triangle's normal
            Vector3 edge_1 = _triangle.v2 - _triangle.v1;
            Vector3 edge_2 = _triangle.v3 - _triangle.v1;
            Vector3 normal = Vector3.Cross(edge_1, edge_2).normalized;
            
            // transform the normal to world space
            normal = _building.transform.TransformDirection(normal);
            
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

