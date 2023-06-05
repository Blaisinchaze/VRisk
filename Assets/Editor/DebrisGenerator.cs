using System;
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
        public string safe_risk_tag = "Black";
        public string low_risk_tag = "Beige";
        public string mid_risk_tag = "Yellow";
        public string high_risk_tag = "Red";

        public int safe_risk_debris_max;
        public int low_risk_debris_max;
        public int mid_risk_debris_max;
        public int high_risk_debris_max;

        public int safe_risk_debris_min;
        public int low_risk_debris_min;
        public int mid_risk_debris_min;
        public int high_risk_debris_min;

        public float spawnPointInterval = 1f;
        public Vector3 forceDirection = Vector3.forward;

        public DebrisTimeline timeline_to_populate;

        [MenuItem("Window/Debris Spawn Point Generator")]
        public static void ShowWindow()
        {
            GetWindow<DebrisGenerator>("Debris Spawn Point Generator");
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
            
            spawnPointInterval = EditorGUILayout.FloatField("Spawn Point Interval", spawnPointInterval);
            forceDirection = EditorGUILayout.Vector3Field("Force Direction", forceDirection);
            
            EditorGUILayout.Space(20);
            
            timeline_to_populate = EditorGUILayout.ObjectField("Timeline to Populate", timeline_to_populate, typeof(DebrisTimeline), true) as DebrisTimeline;
            
            EditorGUILayout.Space(20);
            if (GUILayout.Button("Generate Spawn Points"))
            {
                generateSpawnPoints();
            }
            if (GUILayout.Button("Wipe Timeline"))
            {
                wipeTimeline();
            }
        }

        public void wipeTimeline()
        {
            timeline_to_populate.timeline.Clear();
        }

        public void generateSpawnPoints()
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
                            throw new ArgumentOutOfRangeException();
                    }

                    // get triangles - return if there is none
                    List<Triangle> triangles = getTriangles(building);
                    if (triangles.Count == 0) return;

                    for (int i = 0; i < debris_spawns_count; ++i)
                    {
                        int index = UnityEngine.Random.Range(0, triangles.Count);
                        Triangle triangle = triangles[index];
                        
                        // generate a random point inside the triangle
                        Vector3 point_in_triangle = randomPointInTriangle(triangle.v1, triangle.v2, triangle.v3);
                        Vector3 spawn_point_position = building.transform.TransformPoint(point_in_triangle);
                        Quaternion spawn_point_rotation = Quaternion.FromToRotation(Vector3.forward, -forceDirection);

                        // instantiate a game object at the spawn point
                        GameObject spawnPoint = new GameObject("Debris Spawn Point");
                        spawnPoint.transform.position = spawn_point_position;
                        spawnPoint.transform.rotation = spawn_point_rotation;
                        timeline_to_populate.timeline.Add(new Pair<float, TimelineDebrisTrigger>(10, new TimelineDebrisTrigger(spawn_point_position, new Vector3(0,0,0), DebrisHandler.DebrisType.BRICK)));
                    }
                }
            }
        }

        private List<Triangle> getTriangles(GameObject _building)
        {
            MeshFilter mesh_filter = _building.GetComponent<MeshFilter>();
            if (mesh_filter.IsUnityNull()) return null;
                    
            Mesh mesh = mesh_filter.sharedMesh;
            List<Triangle> triangles = new List<Triangle>();
                    
            for (int x = 0; x < mesh.triangles.Length; x += 3)
            {
                // get the vertices of the triangle
                Vector3 v1 = mesh.vertices[mesh.triangles[x]];
                Vector3 v2 = mesh.vertices[mesh.triangles[x + 1]];
                Vector3 v3 = mesh.vertices[mesh.triangles[x + 2]];

                Triangle triangle = new Triangle(v1, v2, v3);
                
                // break if triangle's normal matches the y axis
                if (isYAxisFacing(triangle, _building)) continue;
                triangles.Add(triangle);
            }

            return triangles;
        }

        private bool isYAxisFacing(Triangle _triangle, GameObject _building)
        {
            // calculate the triangle's normal
            Vector3 edge_1 = _triangle.v2 - _triangle.v1;
            Vector3 edge_2 = _triangle.v3 - _triangle.v1;
            Vector3 normal = Vector3.Cross(edge_1, edge_2).normalized;

            // transform the normal to world space
            normal = _building.transform.TransformDirection(normal);

            // if the normal is not parallel to the world's up or down direction, generate a spawn point
            if (Mathf.Abs(Vector3.Dot(normal, Vector3.up)) < 0.9f && Mathf.Abs(Vector3.Dot(normal, Vector3.down)) < 0.9f)
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

