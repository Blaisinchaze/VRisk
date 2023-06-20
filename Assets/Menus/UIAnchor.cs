using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UIAnchor : MonoBehaviour
{
    public GameObject camera;

    [SerializeField] private bool apply_offset = false;
    [SerializeField] private float Inital_offset = 7000f;

    [SerializeField] private float view_angle = 45.0f;
    [SerializeField] private float walk_area = 0.5f;
    [SerializeField] private float lerp_speed_rot = 5.0f;
    [SerializeField] private float lerp_speed_pos = 3.0f;
    [SerializeField] private float threshold = 0.05f;
    
    private bool reset_angle = true;
    private bool reset_pos = true;

    private Vector3 default_pos;
    private Vector3 anchor_pos;
    
    private void Awake()
    {
        default_pos = transform.position;
    }

    private void Start()
    {
        if (apply_offset)
        {
            //Doing so makes the menu fall from the sky, its just something easy that adds a nice touch
            transform.position = new Vector3(default_pos.x, default_pos.y + Inital_offset, default_pos.z);
        }
    }

    private void Update()
    {
        //Finds out where the anchor should be located based on camera posiion
        Vector3 camera_pos = camera.transform.position;
        anchor_pos = new Vector3(camera_pos.x, default_pos.y + camera_pos.y, camera_pos.z);
        
        //If the player has gotten fairly away from the menu, it gets re-moved in range of the player
        if (reset_pos)
        {
            if (Vector3.Distance(transform.position, anchor_pos) >= threshold)
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
            //Checks if the player is in range
            if (!VectorHelper.ApproximatelyEqual(transform.position, anchor_pos, walk_area)) reset_pos = true;
        }
        
        Quaternion camera_rot = Quaternion.Euler(0, camera.transform.eulerAngles.y, 0);
        Quaternion anchor_rot = Quaternion.Euler(0, transform.eulerAngles.y, 0);

        //Checks if the menu should be moved to face the player again
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
            //Checks if the angle of the camera is facing away from the menu visible angle
            if (Quaternion.Angle(anchor_rot, camera_rot) > view_angle) reset_angle = true;
        }

        transform.rotation = anchor_rot;
    }
}
