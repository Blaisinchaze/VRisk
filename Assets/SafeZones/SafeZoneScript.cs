using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SafeZoneScript : MonoBehaviour
{
    public GameObject head;
    public NavigationArrow nav_arrow;
    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.AudioManager.PlaySound(false, false, head.transform.position, AudioManager.SoundID.WIN);
        Debug.Log("transition");

        nav_arrow.navigating = false;
    }

    private void OnTriggerExit(Collider other)
    {
        nav_arrow.navigating = true;
    }
}
