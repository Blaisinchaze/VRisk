using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DataVisualiser
{
    public class PlaythroughDataScript : MonoBehaviour
    {
        public Toggle selected;
        
        public TMP_Text save_name;
        public TMP_Text save_date;
        public TMP_Text save_time;

        public List<TimelineElement> timeline;
        public bool survived;

        public void setData(string _save_name, string _save_date, string _save_time, List<TimelineElement> _timeline, bool _survived)
        {
            save_name.text = _save_name;
            save_date.text = _save_date;
            save_time.text = _save_time;
            timeline = _timeline;
            survived = _survived;
        }
    }

    [System.Serializable]
    public class TimelineElement
    {
        public float time;
        public Vector2 grid_cell;

        public TimelineElement(float _time, Vector2 _grid_cell)
        {
            time = _time;
            grid_cell = _grid_cell;
        }
    }
}