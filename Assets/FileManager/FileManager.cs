using System.Collections.Generic;
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
            return Path.Combine(Application.dataPath, Path.Combine(_editor_path));
        #else
            return Path.Combine(Application.persistentDataPath, Path.Combine(_android_path));
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

    public static void saveToCSV(string[] _editor_path, string[] _android_path, List<List<string>> _content)
    {
        string[][] content = new string[_content.Count][];
        
        for (int row_index = 0; row_index < _content.Count; row_index++)
        {
            var row_data = _content[row_index];
            string[] columns = new string[row_data.Count];
            
            for (int column_index = 0; column_index < row_data.Count; column_index++)
            {
                columns[column_index] = row_data[column_index];
            }
            
            content[row_index] = columns;
        }

        saveToCSV(_editor_path, _android_path, content);
    }
}
