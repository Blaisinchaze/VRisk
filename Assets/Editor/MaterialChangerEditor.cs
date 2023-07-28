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

        bool hasChanged = EditorGUI.EndChangeCheck();
        
        if (hasChanged)
        {
            script.ChangeMaterial();
        }
        
        DrawDefaultInspector();
    }
}
