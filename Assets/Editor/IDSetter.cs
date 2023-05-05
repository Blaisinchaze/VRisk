using PlasticGui.WorkspaceWindow.PendingChanges.Changelists;
using UnityEditor;
using UnityEngine;

public class IDSetter : EditorWindow
{
    private string safe_risk_tag = "Black";
    private string low_risk_tag = "Beige";
    private string mid_risk_tag = "Yellow";
    private string high_risk_tag = "Red";

    [MenuItem("Window/ID Setter")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        IDSetter window = (IDSetter)EditorWindow.GetWindow(typeof(IDSetter));
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Risk Tags", EditorStyles.boldLabel);

        EditorGUILayout.Space(10);

        EditorGUILayout.TextField("Safe tag: ", safe_risk_tag);
        EditorGUILayout.TextField("Low risk tag: ", low_risk_tag);
        EditorGUILayout.TextField("Mid risk tag: ", mid_risk_tag);
        EditorGUILayout.TextField("High risk tag: ", high_risk_tag);

        EditorGUILayout.Space(10);

        if (GUILayout.Button("Give Buildings IDs")) SetIDs(false);
        if (GUILayout.Button("Reset All IDs")) SetIDs(true);
    }

    private void SetIDs(bool reset)
    {
        GameObject[][] buildings = new GameObject[4][];
        int current_id = 0;

        buildings[0] = GameObject.FindGameObjectsWithTag(safe_risk_tag);
        buildings[1] = GameObject.FindGameObjectsWithTag(low_risk_tag);
        buildings[2] = GameObject.FindGameObjectsWithTag(mid_risk_tag);
        buildings[3] = GameObject.FindGameObjectsWithTag(high_risk_tag);

        for (int i = 0; i < 4; i++)
        {
            foreach (var building in buildings[i])
            {
                var building_data = building.GetComponent<BuildingData>();
                
                switch (i)
                {
                    case 0:
                        building.name = "SafeBuilding(" + current_id + ")";
                        break;
                    case 1:
                        building.name = "LowRiskBuilding(" + current_id + ")";
                        break;
                    case 2:
                        building.name = "MidRiskBuilding(" + current_id + ")";
                        break;
                    case 3:
                        building.name = "HighRiskBuilding(" + current_id + ")";
                        break;
                }

                building_data.id = reset ? 0 : current_id;
                current_id++;
            }
        }
    }
}
