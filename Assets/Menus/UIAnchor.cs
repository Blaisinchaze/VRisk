using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UIAnchor : MonoBehaviour
{
    public GameObject camera;
    
    [SerializeField] private float view_angle = 45.0f;
    [SerializeField] private float walk_area = 0.5f;
    [SerializeField] private float lerp_speed_rot = 5.0f;
    [SerializeField] private float lerp_speed_pos = 3.0f;

    private bool reset_angle = true;
    private bool reset_pos = true;
    private float threshold = 0.05f;
    
    private Vector3 default_pos;
    private Vector3 anchor_pos;
    
    private void Awake()
    {
        default_pos = transform.position;
    }

    private void Update()
    {
        Vector3 camera_pos = camera.transform.position;
        anchor_pos = new Vector3(camera_pos.x, default_pos.y + camera_pos.y, camera_pos.z);
        
        if (reset_pos)
        {
            if (Vector3.Distance(transform.position, anchor_pos) >= 0.05f)
            {
                transform.position = Vector3.Lerp(transform.position, anchor_pos, lerp_speed_pos * Time.deltaTime);
            }
            else
            {
                reset_pos = false;
            }
        }
        else
        { 
            if (!ApproximatelyEqual(transform.position, anchor_pos, walk_area)) reset_pos = true;
        }
        
        Quaternion camera_rot = Quaternion.Euler(0, camera.transform.eulerAngles.y, 0);
        Quaternion anchor_rot = Quaternion.Euler(0, transform.eulerAngles.y, 0);

        if (reset_angle)
        {
            if (Quaternion.Angle(anchor_rot, camera_rot) > threshold)
            {
                anchor_rot = Quaternion.Lerp(anchor_rot, camera_rot, lerp_speed_rot * Time.deltaTime);
            }
            else
            {
                reset_angle = false;
            }
        }
        else
        {
            if (Quaternion.Angle(anchor_rot, camera_rot) > view_angle) reset_angle = true;
            
        }

        transform.rotation = anchor_rot;
    }
    
    bool ApproximatelyEqual(Vector3 a, Vector3 b, float threshold)
    {
        return Mathf.Abs(a.x - b.x) <= threshold &&
               Mathf.Abs(a.y - b.y) <= threshold &&
               Mathf.Abs(a.z - b.z) <= threshold;
    }
}
