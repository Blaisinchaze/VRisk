using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public enum BuildingType
    {
        BASIC_SET
    }

    [SerializeField] private string safe_risk_tag = "Black";
    [SerializeField] private string low_risk_tag = "Beige";
    [SerializeField] private string mid_risk_tag = "Yellow";
    [SerializeField] private string high_risk_tag = "Red";
    
    private Dictionary<int, Triple<RiskLevel, GameObject, BuildingData>> buildings = new Dictionary<int, Triple<RiskLevel, GameObject, BuildingData>>();

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
                // Add instead of directly assigning to avoid overwriting if IDs are wrong - will throw error. 
                buildings.Add(building_data.id,
                    new Triple<RiskLevel, GameObject, BuildingData>(_risk, game_object, building_data));
            }
        }
    }

    public void damageBuilding(int _building_id, float _intensity, float _shaking_reposition_interval, float _impact_shake_duration, float _affect_radius)
    {
        // Grab reference to desired building. 
        var building = buildings[_building_id];

        // If building is not collapsed, initiate damage transition.
        if (building.third.state > 0)
        {
            var building_data = building.third;
            building_data.state--;

            //_building_data.mesh_filter.mesh = _building_data.building_map.states[(int) _building_data.state].second;
            //_building_data.collider.sharedMesh = _building_data.building_map.states[(int) _building_data.state].second;

            // Temporary - Above is the code for swapping meshes and colliders, but don't have meshes yet.
            // Below like is temp until we have meshes. 
            building_data.gameObject.transform.Translate(0, -2, 0);
        
            AudioManager.SoundID sound_id = building_data.state == BuildingState.COLLAPSED
                ? AudioManager.SoundID.BUILDING_COLLAPSE
                : AudioManager.SoundID.BUILDING_DAMAGE;
        
            GameManager.Instance.AudioManager.PlaySound(false, false, building_data.original_position, sound_id);
            GameManager.Instance.ParticleManager.triggerBuildingCollapseEffect(ParticleManager.ParticleID.BUILDING_DAMAGE, building_data.mesh_renderer);

            triggerLocalisedShake(building_data.gameObject, _intensity, _shaking_reposition_interval, _impact_shake_duration, _affect_radius);
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
        Vector3 centre = _building.transform.position;

        Collider[] hit_colliders = Physics.OverlapSphere(centre, _affect_radius);
        foreach (var collider in hit_colliders)
        {
            if (collider.gameObject.CompareTag(safe_risk_tag) || collider.gameObject.CompareTag(low_risk_tag)
                || collider.gameObject.CompareTag(mid_risk_tag) || collider.gameObject.CompareTag(high_risk_tag))
            {
                var building = buildings[collider.GetComponent<BuildingData>().id];
                
                float distance = Vector3.Distance(_building.transform.position, building.second.transform.position);

                // Linearly decrease intensity with distance
                float building_specific_intensity = Mathf.Clamp01(1f - distance / _affect_radius) * _max_intensity;
                if (building_specific_intensity > 0.05f)
                {
                    StartCoroutine(ShakeBuildingBuildingCollapseVersion(building.second, building.third,
                        building_specific_intensity, _shaking_reposition_interval, _duration));
                }
            }
            else if (collider.gameObject.CompareTag("Player"))
            {
                float distance = Vector3.Distance(_building.transform.position, collider.gameObject.transform.position);

                GameManager.Instance.HapticFeedbackHandler.triggerCosIntensityHapticFeedback(1 - distance/_affect_radius, _duration);
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
                float progress = (elapsed / _duration);
                float intensity = _max_intensity * GameManager.earthquakeIntensityCurve(progress);

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
                float intensity = _max_intensity * Mathf.Cos(progress * Mathf.PI);

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
        _building.transform.position = new Vector3(_building_data.original_position.x, _building.transform.position.y,
            _building_data.original_position.z);
    }

    public Triple<RiskLevel, GameObject, BuildingData> getBuilding(int _id)
    {
        if (buildings.ContainsKey(_id))
        {
            return buildings[_id];
        }

        return null;
    }
}
