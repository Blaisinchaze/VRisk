using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class MenuController : MonoBehaviour
{
    public float animationTime = 0.5f;
    
    //private GameObject canvas;

    [SerializeField] private bool startOpen = false;
    [SerializeField] private bool open = true;

    void Start()
    {
        //canvas = transform.GetChild(0).gameObject;
        
        if (!startOpen)
        {
            transform.localScale = new Vector3(0, 0, 1);
            open = false;
            //canvas.SetActive(false);
        }
    }

    public void OpenMenu()
    {
        //canvas.SetActive(true);
        open = true;
        transform.LeanScale(new Vector3(1,1,1), animationTime);
    }

    public void CloseMenu()
    {
        //canvas.SetActive(false);
        open = false;
        transform.LeanScale(new Vector3(0,0,1), animationTime).setEaseInBack();
    }

    public void ToggleOpenClose()
    {
        if (open)
        {
            CloseMenu();
        }
        else
        {
            OpenMenu();
        }
    }
}
