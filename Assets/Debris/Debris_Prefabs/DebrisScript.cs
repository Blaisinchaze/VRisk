using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class DebrisScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        GameManager.Instance.ParticleManager.triggerEffect(ParticleManager.ParticleID.DEBRIS_IMPACT, other.contacts[0].point,other.contacts[0].normal);
        GameManager.Instance.AudioManager.PlaySound(false, false, other.contacts[0].point, AudioManager.SoundID.DEBRIS_COLLISION);
    }

    private void FixedUpdate()
    {
        
    }
}
