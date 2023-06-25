using UnityEngine;
using SFB;
using System.IO;

public class UIHandler : MonoBehaviour
{
    public GameObject playthrough_button_prefab;
    
    public GameObject menu_open;
    public GameObject menu_closed;

    public void toggleMenu(bool _active)
    {
        if (_active)
        {
            menu_open.SetActive(true);
            menu_closed.SetActive(false);
        }
        else
        {
            menu_open.SetActive(false);
            menu_closed.SetActive(true);
        }
    }
    
    public void importData()
    {
        loadCSVData(selectFolder());
    }

    private string selectFolder()
    {
        string folder_path = "";
        var paths = StandaloneFileBrowser.OpenFolderPanel("Select Folder", "", false);

        if (paths.Length > 0)
        { 
            folder_path = paths[0];
            Debug.Log("Selected folder: " + folder_path);
        }

        return folder_path;
    }

    private void loadCSVData(string _folder_path)
    {
        string[] csv_files = Directory.GetFiles(_folder_path, "*.csv");

        foreach (var file in csv_files)
        {
            string contents = FileManager.getFileContents(new[] {file});

            string[][] csv_contents = FileManager.parseCSV(contents);
            
            
            // stash data, has to be tied to UI button.
            // Have to create UI button to represent this data.
        }
    }
}
