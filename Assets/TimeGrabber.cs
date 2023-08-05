using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class TimeGrabber : MonoBehaviour
{
    public GameData data;
    public TextMeshProUGUI textMesh;
    
    void Start()
    {
        TimeSpan t = TimeSpan.FromSeconds( data.totalTime);
        string timeElapsed = string.Format("{0:D2}:{1:D2}:{2:D3}", 
            t.Minutes, 
            t.Seconds, 
            t.Milliseconds);
        textMesh.text = timeElapsed;
    }
}
