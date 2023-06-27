using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CustomSlideManager : MonoBehaviour
{
    public TextMeshProUGUI meshText;
    public Slider slider;

    public float increaseValue = 1.0f;

    private void Start()
    {
        meshText.text = slider.value.ToString(CultureInfo.InvariantCulture);
    }

    public void OnValueChanged()
    {
        var value = slider.value;
        meshText.text = value.ToString(CultureInfo.InvariantCulture);
    }

    public void OnValueIncrease()
    {
        slider.value += increaseValue;
    }

    public void OnValueDecrease()
    {
        slider.value -= increaseValue;
    }
}
