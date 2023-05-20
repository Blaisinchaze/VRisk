using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

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
    
    public Dictionary<int, Triple<RiskLevel, GameObject, BuildingData>> buildings = new Dictionary<int, Triple<RiskLevel, GameObject, BuildingData>>();
    public bool shaking;

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
        //damageBuilding(1, 0.2f, 0.05f, 4);
        var building = buildings[1];

        // If building is not collapsed, initiate damage transition.
        if (building.third.state > 0)
        {
            building.third.transitioning = true;
            
            //StartCoroutine(ShakeBuildingBuildingCollapseVersion(building.second, building.third, 0.2f, 0.05f, 4));
            // trigger particles
        }
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

                /*building.Value.third.mesh_filter.mesh =
                    building.Value.third.building_map.states[(int) building.Value.third.state].second;
                building.Value.third.collider.sharedMesh = building.Value.third.building_map
                    .states[(int) building.Value.third.state].second;*/
                
                // Temporary - Above is the code for swapping meshes and colliders, but don't have meshes yet.
                // Below like is temp until we have meshes. 
                building.Value.second.gameObject.transform.Translate(0, -2, 0);
                triggerLocalisedShake(building.Value.second, 0.2f, 0.05f, 2, 40);

                building.Value.third.transition_timer = 0;
                building.Value.third.transitioning = false;
                building.Value.third.state--;

            }
        }
    }

    public void damageBuilding(int _building_id, float _intensity, float _shaking_reposition_interval, float _duration)
    {
        // Grab reference to desired building. 
        var building = buildings[_building_id];

        // If building is not collapsed, initiate damage transition.
        if (building.third.state > 0)
        {
            building.third.transitioning = true;

            triggerGlobalShake(_intensity, _shaking_reposition_interval, _duration);
            // trigger particles
        }
    }

    public void triggerGlobalShake(float _max_intensity, float _shaking_reposition_interval, float _duration)
    {
        foreach (var building in buildings)
        {
            StartCoroutine(ShakeBuildingSeismicVersion(building.Value.second, building.Value.third, _max_intensity, _shaking_reposition_interval, _duration));
        }
    }

    private void triggerLocalisedShake(GameObject _building, float _max_intensity, float _shaking_reposition_interval, float _duration, float _affect_radius)
    {
        foreach (var building in buildings)
        {
            float distance = Vector3.Distance(_building.transform.position, building.Value.second.transform.position);

            if (distance < _affect_radius)
            {
                float building_specific_intensity = _max_intensity * MathF.Sin(0.5f * distance * Mathf.PI);

                if (distance < 1)
                {
                    building_specific_intensity = _max_intensity;
                }

                if (building_specific_intensity > 0.05)
                {
                    StartCoroutine(ShakeBuildingBuildingCollapseVersion(building.Value.second, building.Value.third,
                        building_specific_intensity, _shaking_reposition_interval, _duration));
                }
            }
        }
    }
    
    /// <summary>
    /// Shakes a given building - this version builds in intensity and then diminishes.
    /// </summary>
    /// <param name="_building"> The building to shake. </param>
    /// <param name="_building_data"> The BuildingData instance associated with the building. </param>
    /// <param name="_max_intensity"> The maximum distance from the original position for shaking movement. </param>
    /// <param name="_shaking_reposition_interval"> The time between movements. </param>
    /// <param name="_duration"> The time in seconds for the building to be shaking. </param>
    /// <returns></returns>
    private IEnumerator ShakeBuildingSeismicVersion(GameObject _building, BuildingData _building_data, float _max_intensity, float _shaking_reposition_interval, float _duration)
    {
        float elapsed = 0.0f;
        float reposition_timer = 0.0f;

        while (elapsed < _duration)
        {
            if (reposition_timer > _shaking_reposition_interval)
            {
                float progress = elapsed / _duration;
                float intensity = _max_intensity * MathF.Sin(progress * Mathf.PI);

                float x = Random.Range(-1f, 1f) * intensity + _building_data.original_position.x;
                float y = _building.transform.position.y;
                float z = Random.Range(-1f, 1f) * intensity + _building_data.original_position.z;

                _building.transform.position = new Vector3(x, y, z);

                reposition_timer = 0.0f;
            }

            reposition_timer += Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }

        // bring back to original position after shaking
        //_building.transform.position = _building_data.original_position;
        
        _building.transform.position = new Vector3(_building_data.original_position.x, _building.transform.position.y,
            _building_data.original_position.z);
    }
    
    /// <summary>
    /// Shakes a given building - This version does not build in intensity, but it does diminish over time.
    /// </summary>
    /// <param name="_building"> The building to shake. </param>
    /// <param name="_building_data"> The BuildingData instance associated with the building. </param>
    /// <param name="_max_intensity"> The maximum distance from the original position for shaking movement. </param>
    /// <param name="_shaking_reposition_interval"> The time between movements. </param>
    /// <param name="_duration"> The time in seconds for the building to be shaking. </param>
    /// <returns></returns>
    private IEnumerator ShakeBuildingBuildingCollapseVersion(GameObject _building, BuildingData _building_data, float _max_intensity, float _shaking_reposition_interval, float _duration)
    {
        float elapsed = 0.0f;
        float reposition_timer = 0.0f;

        while (elapsed < _duration)
        {
            if (reposition_timer > _shaking_reposition_interval)
            {
                float progress = elapsed / _duration;
                float intensity = _max_intensity * Mathf.Cos(0.5f * progress * Mathf.PI);

                float x = Random.Range(-1f, 1f) * intensity + _building_data.original_position.x;
                float y = _building.transform.position.y;
                float z = Random.Range(-1f, 1f) * intensity + _building_data.original_position.z;

                _building.transform.position = new Vector3(x, y, z);

                reposition_timer = 0.0f;
            }

            reposition_timer += Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }

        // bring back to original position after shaking
        //_building.transform.position = _building_data.original_position;

        _building.transform.position = new Vector3(_building_data.original_position.x, _building.transform.position.y,
            _building_data.original_position.z);
    }
}
