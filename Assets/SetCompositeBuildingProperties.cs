using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SetCompositeBuildingProperties : MonoBehaviour
{
    [SerializeField] private List<GameObject> buildings;
    [SerializeField] private int nOfChildren = 4;

    public enum BuildingState : int
    {
        BASE = 0,
        GRADE1 = 1,
        GRADE2 = 2,
        GRADE3 = 3
    }

    //State of destruction
    private BuildingState currentState = BuildingState.BASE;

    public void SetState(BuildingState state)
    {
        currentState = state;
    }

    public BuildingState GetState()
    {
        return currentState;
    }

    //Color of the building
    private SetNewMaterial.MaterialSelection color = SetNewMaterial.MaterialSelection.CYAN;

    public void SetColor(SetNewMaterial.MaterialSelection newColor)
    {
        color = newColor;
    }

    public SetNewMaterial.MaterialSelection GetColor()
    {
        return color;
    }

    //Pavement properties
    private bool pavement = true;

    public void SetPavement(bool visible)
    {
        pavement = visible;
    }

    public bool GetPavement()
    {
        return pavement;
    }

    private void Start()
    {
        GroupBuildings();
    }

    //Groups all the buildings inside the list
    private void GroupBuildings()
    {
        // Does not group if already grouped
        if (buildings.Count == nOfChildren) return;
        buildings.Clear();

        foreach (Transform building in transform)
        {
            if (!building.gameObject.CompareTag("Ignore"))
            {
                buildings.Add(building.gameObject);
            }
        }
    }

    // Sets color and checks if the pavement should be present or not
    public void ChangeProperties()
    {
        GroupBuildings();

        foreach (GameObject building in buildings)
        {
            SetNewMaterial script = building.GetComponent<SetNewMaterial>();
            script.setMat(color);
            script.setPavement(pavement);
            script.ChangeMaterial();
        }
    }

    //Mainly meant for the editor, sets the correct building state
    public void SetCurrentState()
    {
        GroupBuildings();

        foreach (GameObject building in buildings)
        {
            building.SetActive(false);
        }
        buildings[(int)currentState].SetActive(true);
    }
}
