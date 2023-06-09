using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShineMovement : MonoBehaviour
{
    public bool shining = true;
    public float shine_speed;
    public float leap = 100;
    public float shine_duration = 1.0f;
    private float shine_timer = 0;
    
    private Vector2 initial_pos;
    private Vector2 final_pos;

    private RectTransform rect_transform;

    void Awake()
    {
        rect_transform = this.GetComponent<RectTransform>();
    }

    void Start()
    {
        initial_pos = rect_transform.anchoredPosition;
        final_pos = new Vector2(initial_pos.x + leap, initial_pos.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!shining) return;

        shine_timer += Time.fixedDeltaTime;
        if (shine_timer > shine_duration)
        {
            rect_transform.anchoredPosition = initial_pos;
            shining = false;
            shine_timer = 0;
        }

        if (rect_transform.anchoredPosition != final_pos)
        {
            rect_transform.anchoredPosition = Vector2.Lerp(rect_transform.anchoredPosition, final_pos, shine_speed);
        }
    }
}
