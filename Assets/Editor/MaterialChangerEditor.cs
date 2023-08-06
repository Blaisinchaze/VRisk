using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SetNewMaterial))]
public class MaterialChangerEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        SetNewMaterial script = (SetNewMaterial)target;

        EditorGUI.BeginChangeCheck();
        
        script.setMat((SetNewMaterial.MaterialSelection)EditorGUILayout.EnumPopup("Material", script.getMat()));
        script.setPavement(EditorGUILayout.Toggle("Pavement", script.getPavement()));
        
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        
        bool hasChanged = EditorGUI.EndChangeCheck();
        
        if (hasChanged)
        {
            script.ChangeMaterial();
        }
        
        DrawDefaultInspector();
    }
}

[CustomEditor(typeof(SetCompositeBuildingProperties))]
public class ColorChangerEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        SetCompositeBuildingProperties script = (SetCompositeBuildingProperties)target;

        EditorGUI.BeginChangeCheck();
        
        script.SetColor((SetNewMaterial.MaterialSelection)EditorGUILayout.EnumPopup("Color", script.GetColor()));
        script.SetPavement(EditorGUILayout.Toggle("Pavement", script.GetPavement()));
        script.SetState((SetCompositeBuildingProperties.BuildingState)EditorGUILayout.EnumPopup("Starting State", script.GetState()));

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        
        bool hasChanged = EditorGUI.EndChangeCheck();
        
        if (hasChanged)
        {
            script.ChangeProperties();
            script.SetCurrentState();
        }
        
        DrawDefaultInspector();
    }
}
