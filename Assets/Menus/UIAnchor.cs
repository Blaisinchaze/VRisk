using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnchor : MonoBehaviour
{
    public GameObject camera;
    
    [SerializeField] private float view_angle = 45.0f;
    [SerializeField] private float lerp_speed = 5.0f;

    private bool reset = true;
    private float threshold = 0.05f;
    private Vector3 default_pos;
    
    private void Awake()
    {
        default_pos = transform.position;
    }

    private void Update()
    {
        Vector3 camera_pos = camera.transform.position;
        Vector3 anchor_pos = new Vector3(camera_pos.x, default_pos.y + camera_pos.y, camera_pos.z);
        transform.position = anchor_pos;

        Quaternion camera_rot = Quaternion.Euler(0, camera.transform.eulerAngles.y, 0);
        Quaternion anchor_rot = Quaternion.Euler(0, transform.eulerAngles.y, 0);

        if (reset)
        {
            if (Quaternion.Angle(anchor_rot, camera_rot) > threshold)
            {
                anchor_rot = Quaternion.Lerp(anchor_rot, camera_rot, lerp_speed * Time.deltaTime);
            }
            else
            {
                reset = false;
            }
        }
        else
        {
            if (Quaternion.Angle(anchor_rot, camera_rot) > view_angle)
            {
                reset = true;
            }
        }

        transform.rotation = anchor_rot;
    }
}
