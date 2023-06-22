using System.IO;
using UnityEngine;

public class FileManager : MonoBehaviour
{
    public static string getFile(string _editor_path, string _android_path)
    {
        string path = getFilePath(_editor_path, _android_path);

        if (File.Exists(path))
        {
            return File.ReadAllText(path);
        }
        return null;
    }
    
    public static string getFilePath(string _editor_path, string _android_path)
    {
        #if UNITY_EDITOR
            return Path.Combine(Application.dataPath, _editor_path);
        #else
            return Path.Combine(Application.persistentDataPath, _android_path);
        #endif
    }
    
    public static string[][] parseCSV(string fileContents)
    {
        string[] lines = fileContents.Split(',');
        
        string[][] data = new string[lines.Length][];
        for (int i = 0; i < lines.Length; i++)
        {
            data[i] = lines[i].Split(',');
        }

        return data;
    }
}
