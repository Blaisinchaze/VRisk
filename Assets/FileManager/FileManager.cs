using System.IO;
using UnityEngine;

public class FileManager : MonoBehaviour
{
    public static string getFileContents(string[] _editor_path, string[] _android_path)
    {
        string path = getFilePath(_editor_path, _android_path);

        if (File.Exists(path))
        {
            return File.ReadAllText(path);
        }
        return null;
    }
    
    public static string getFilePath(string[] _editor_path, string[] _android_path)
    {
        #if UNITY_EDITOR
            var path = Path.Combine(_editor_path);
            return Path.Combine(Application.dataPath, path);
        #else
            var path = Path.Combine(_android_path);
            return Path.Combine(Application.persistentDataPath, path);
        #endif
    }
    
    public static string[][] parseCSV(string fileContents)
    {
        string[] lines = fileContents.Split('\n');
        
        string[][] data = new string[lines.Length][];
        for (int i = 0; i < lines.Length; i++)
        {
            data[i] = lines[i].Split(',');
        }

        return data;
    }

    public static void saveToFile(string[] _editor_path, string[] _android_path, string _content)
    {
        string path = getFilePath(_editor_path, _android_path);
        File.WriteAllText(path, _content);
    }

    public static void saveToCSV(string[] _editor_path, string[] _android_path, string[][] _content)
    {
        string path = getFilePath(_editor_path, _android_path);

        using (StreamWriter sw = new StreamWriter(path))
        {
            for (int i = 0; i < _content.Length; i++)
            {
                sw.WriteLine(string.Join(",", _content[i]));
            }
        }
    }
}
