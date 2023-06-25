using UnityEngine;
using SFB;

public class UIHandler : MonoBehaviour
{
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
    
    public void selectFolder()
    {
        // Open folder browser dialog
        var paths = StandaloneFileBrowser.OpenFolderPanel("Select Folder", "", false);

        // Check if a folder was selected
        if (paths.Length > 0)
        {
            // Get the selected folder path
            string folderPath = paths[0];

            // Use the folder path
            Debug.Log("Selected folder: " + folderPath);
        }
    }
}
