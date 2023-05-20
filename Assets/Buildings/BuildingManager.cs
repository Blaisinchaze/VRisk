using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.MeshOperations;

public class BuildingManager : MonoBehaviour
{
    public enum RiskLevel : int
    {
        SAFE = 0,
        LOW = 1,
        MID = 2,
        HIGH = 3
    }

    public enum BuildingState
    {
        COLLAPSED,
        VERY_DAMAGED,
        DAMAGED,
        NO_DAMAGE,
    }

    [SerializeField] private string safe_risk_tag = "Black";
    [SerializeField] private string low_risk_tag = "Beige";
    [SerializeField] private string mid_risk_tag = "Yellow";
    [SerializeField] private string high_risk_tag = "Red";

    public Dictionary<int, Triple<RiskLevel, GameObject, BuildingData>> buildings =
        new Dictionary<int, Triple<RiskLevel, GameObject, BuildingData>>();

    public bool shaking = false;

    private void Awake()
    {
        // Add all buildings to buildings list. 
        addBuildingsWithTag(safe_risk_tag, RiskLevel.SAFE);
        addBuildingsWithTag(low_risk_tag, RiskLevel.LOW);
        addBuildingsWithTag(mid_risk_tag, RiskLevel.MID);
        addBuildingsWithTag(high_risk_tag, RiskLevel.HIGH);
    }
    
    private void addBuildingsWithTag(string _tag, RiskLevel _risk)
    {
        // Grab list of building game objects with provided risk tag. 
        var game_objects = GameObject.FindGameObjectsWithTag(_tag);
        
        foreach (var game_object in game_objects)
        {
            // Grab building data. 
            BuildingData building_data = game_object.GetComponent<BuildingData>();

            if (building_data != null)
            {
                // Add building to list of buildings. 
                // Add instead of directly assigning to avoid overriting if IDs are wrong - will throw error. 
                buildings.Add(building_data.id, new Triple<RiskLevel, GameObject, BuildingData>(_risk, game_object, building_data));
            }
        }
    }
 
    private void Start()
    {
        // debug key for damaging a building.
        GameManager.Instance.InputHandler.input_asset.InputActionMap.Debug.started += trigger;
    }

    // Debug - to be removed. 
    void trigger(InputAction.CallbackContext context)
    {
        Debug.Log("triggered");
        damageBuilding(1);
    }

    private void FixedUpdate()
    {
        foreach (var building in buildings)
        {
            if (building.Value.third.transitioning)
            {
                if (building.Value.third.transition_timer < building.Value.third.transition_duration)
                {
                    building.Value.third.transition_timer += Time.fixedDeltaTime;
                    continue;
                }
                
                Debug.Log("fall");

                /*building.Value.third.mesh_filter.mesh =
                    building.Value.third.building_map.states[(int) building.Value.third.state].second;
                building.Value.third.collider.sharedMesh = building.Value.third.building_map
                    .states[(int) building.Value.third.state].second;*/
                
                // Temporary - Above is the code for swapping meshes and colliders, but don't have meshes yet.
                // Below like is temp until we have meshes. 
                building.Value.second.gameObject.transform.Translate(0, -2, 0);

                building.Value.third.transition_timer = 0;
                building.Value.third.transitioning = false;
                building.Value.third.state--;

            }
        }
    }

    public void damageBuilding(int _building_id)
    {
        // Grab reference to desired building. 
        var building = buildings[_building_id];

        // If building is not collapsed, initiate damage transition.
        if (building.third.state > 0)
        {
            Debug.Log("started");
            building.third.transitioning = true;
            
                    // trigger particles
        }
    }

    public void triggerGlobalShake(float _intensity, float _duration)
    {
        shaking = true;
    }

    public void triggerLocalisedShake(float _intensity, float _duration)
    {
        
    }
}
