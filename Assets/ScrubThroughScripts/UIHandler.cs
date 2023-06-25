using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using SFB;
using System.IO;
using Debug = UnityEngine.Debug;

namespace DataVisualiser
{
    public class UIHandler : MonoBehaviour
    {
        public GameObject playthrough_button_prefab;
        public RectTransform playthrough_button_container;
        public float tab_gap = 5;
        public float tab_height = 40;
        public float tab_width = 140;

        private List<Pair<GameObject, PlaythroughDataScript>> playthroughs;

        public GameObject menu_open;
        public GameObject menu_closed;

        private void Awake()
        {
            playthroughs = new List<Pair<GameObject, PlaythroughDataScript>>();
        }

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
                var playthrough_data = createTab();
                // instantiate and position button.

                string contents = FileManager.getFileContents(new[] {file});
                string[][] csv_contents = FileManager.parseCSV(contents);
                
                bool survived = false;
                string save_name = "";
                string save_date = "";
                string save_time = "";
                List<TimelineElement> timeline = new List<TimelineElement>();
                
                survived = csv_contents[csv_contents.Length-2][3] == "Survived\r";
                string filename = Path.GetFileNameWithoutExtension(file);
                
                string[] parts = filename.Split(new string[] { " - " }, StringSplitOptions.None);
                string dateAndTimePart = parts[0];
                save_name = parts.Length > 1 ? parts[1] : "";
                
                string[] dateAndTimeParts = dateAndTimePart.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                save_date = dateAndTimeParts.Length > 0 ? dateAndTimeParts[0].Trim() : "";
                save_time = dateAndTimeParts.Length > 1 ? dateAndTimeParts[1].Trim() : "";
                

                for (int line_index = 0; line_index < csv_contents.Length-1; line_index++)
                {
                    string[] line = csv_contents[line_index];
                    
                    float time = float.Parse(line[0]);
                    Vector2 grid_cell = new Vector2(float.Parse(line[1]), float.Parse(line[2]));
                    
                    timeline.Add(new TimelineElement(time, grid_cell));
                }

                playthrough_data.setData(save_name, save_date, save_time, timeline, survived);
                // stash data, has to be tied to UI button.
                // Have to create UI button to represent this data.
            }
        }

        private PlaythroughDataScript createTab()
        {
            float y_position = -playthroughs.Count * (tab_height + tab_gap);

            playthrough_button_container.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (tab_height + tab_gap) * playthroughs.Count);
            
            GameObject playthrough_tab = Instantiate(playthrough_button_prefab, playthrough_button_container);
            playthrough_tab.transform.localPosition = new Vector2(0, y_position);

            PlaythroughDataScript playthrough_data = playthrough_tab.GetComponent<PlaythroughDataScript>();
            
            playthroughs.Add(new Pair<GameObject, PlaythroughDataScript>(playthrough_tab, playthrough_data));
            
            return playthrough_data;
        }
    }
}
