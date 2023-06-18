using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("transition");
    }
}
