using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SetNewMaterial))]
public class MaterialChangerEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SetNewMaterial script = (SetNewMaterial)target;
        if (GUILayout.Button("Set Material"))
        {
            script.ChangeMaterial();
        }
    }
}
