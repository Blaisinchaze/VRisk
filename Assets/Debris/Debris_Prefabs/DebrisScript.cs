using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]

public class DebrisScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        //ParticleID _id, Vector3 _location, Vector3 _rotation, Transform _parent = null, bool _relative_to_parent = false)
        GameManager.Instance.ParticleManager.triggerEffect(ParticleManager.ParticleID.DEBRIS_IMPACT, other.contacts[0].point, other.contacts[0].normal);
    }
}
