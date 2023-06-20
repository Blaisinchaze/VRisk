using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeightManager : MonoBehaviour
{
    public TextMeshProUGUI textCM;
    public TextMeshProUGUI textFT;
    
    public int height = 170;

    private void Start()
    {
        UpdateText();
    }

    public void IncrementValue(int amount)
    {
        height += amount;
        UpdateText();
    }

    public void DecrementValue(int amount)
    {
        if (height == 0) return;

        bool skip = false;

        if (height < amount)
        {
            height = 0;
            skip = true;
        }

        if(!skip) height -= amount;
        UpdateText();
    }

    private void UpdateText()
    {
        textCM.text = height + " CM";

        var feet = height * 0.0328084f; //Conversion cm to feet

        int feetPart = Mathf.FloorToInt(feet);
        int inchesPart = Mathf.RoundToInt((feet - feetPart) * 12f);

        textFT.text = feetPart + "' " + inchesPart + "' FT";
    }
}
