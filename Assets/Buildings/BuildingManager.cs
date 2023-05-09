using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public enum RiskLevel : int
    {
        SAFE = 0,
        LOW = 1,
        MID = 2,
        HIGH = 3
    }

    [SerializeField] private string safe_risk_tag   = "Black";
    [SerializeField] private string low_risk_tag    = "Beige";
    [SerializeField] private string mid_risk_tag    = "Yellow";
    [SerializeField] private string high_risk_tag   = "Red";

    private List<Triple<int, RiskLevel, GameObject>> building_list;

    private void Start()
    {
        GameObject[][] all_building_arr = new GameObject[4][];
        
        all_building_arr[(int)RiskLevel.SAFE] = GameObject.FindGameObjectsWithTag(safe_risk_tag);
        all_building_arr[(int)RiskLevel.LOW]  = GameObject.FindGameObjectsWithTag(low_risk_tag);
        all_building_arr[(int)RiskLevel.MID]  = GameObject.FindGameObjectsWithTag(mid_risk_tag);
        all_building_arr[(int)RiskLevel.HIGH] = GameObject.FindGameObjectsWithTag(high_risk_tag);

        List<Pair<float, GameObject>> all_buildings_list;



    }
}
