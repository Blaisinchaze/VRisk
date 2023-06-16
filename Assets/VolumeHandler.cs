using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeHandler : MonoBehaviour
{
    public GameData data;
    public Slider slider;
    public TextMeshProUGUI text_value;

    public void IncrementByOne()
    {
        slider.value += 1;
    }

    public void DecrementByOne()
    {
        slider.value -= 1;
    }

    public void OnValueChanged()
    {
        var value = slider.value;

        text_value.text = value.ToString();
        data.Volume = (int)value;
    }
}
