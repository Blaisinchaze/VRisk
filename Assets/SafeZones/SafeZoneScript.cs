using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SafeZoneScript : MonoBehaviour
{
    public GameObject head;
    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.AudioManager.PlaySound(false, false, head.transform.position, AudioManager.SoundID.WIN);
        Debug.Log("transition");
    }
}
