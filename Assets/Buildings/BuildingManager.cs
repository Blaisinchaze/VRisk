using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private void Start()
    {
        InputHandler.Instance.input_asset.InputActionMap.Debug.started += trigger;
        GameObject[][] all_building_arr = new GameObject[4][];
        
        all_building_arr[(int)RiskLevel.SAFE] = GameObject.FindGameObjectsWithTag(safe_risk_tag);
        all_building_arr[(int)RiskLevel.LOW]  = GameObject.FindGameObjectsWithTag(low_risk_tag);
        all_building_arr[(int)RiskLevel.MID]  = GameObject.FindGameObjectsWithTag(mid_risk_tag);
        all_building_arr[(int)RiskLevel.HIGH] = GameObject.FindGameObjectsWithTag(high_risk_tag);

        foreach (var building_list in all_building_arr)
        {
            foreach (var building in building_list)
            {
                buildings.Add(building.GetComponent<BuildingData>().id, new Triple<RiskLevel, GameObject, BuildingData>(RiskLevel.LOW, building.gameObject, building.GetComponent<BuildingData>()));
                //buildings[building.GetComponent<BuildingData>().id]
            }
        }
    }

    void trigger(InputAction.CallbackContext context)
    {
        Debug.Log("triggered");
        damageBuilding(1);
    }

    private void Update()
    {

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
                    
                    Debug.Log("shaking : " + building.Value.third.transition_timer);
                    continue;
                }
                
                Debug.Log("fall");
                building.Value.second.gameObject.transform.Translate(0,-2, 0);
                building.Value.third.transition_timer = 0;
                building.Value.third.transitioning = false;
                //shake building
                // move down on Y.
            }
        }
    }

    public void damageBuilding(int _building_id)
    {
        var building = buildings[_building_id];

        /*if (building.third.state > 0 && (int)building.third.state <= (int) BuildingState.NO_DAMAGE)
        {
            building.third.state--;

            building.second.GetComponent<MeshFilter>().mesh = building.third.building_map.states[(int)building.third.state].second;
        }*/

        if (building.third.state > 0 && (int) building.third.state <= (int) BuildingState.NO_DAMAGE)
        {
            Debug.Log("started");
            building.third.transitioning = true;
            building.third.state--;
        }



        // swap mesh
        
        // trigger particles
        
        
    }
}
