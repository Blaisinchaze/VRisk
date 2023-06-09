using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DebrisScript : MonoBehaviour
{
    private float elapsed_time = 0;
    private Vector3 original_scale;

    private void Awake()
    {
        original_scale = gameObject.transform.localScale;
    }

    private void OnCollisionEnter(Collision other)
    {
        GameManager.Instance.ParticleManager.triggerEffect(ParticleManager.ParticleID.DEBRIS_IMPACT, other.contacts[0].point,other.contacts[0].normal);
        GameManager.Instance.AudioManager.PlaySound(false, false, other.contacts[0].point, AudioManager.SoundID.DEBRIS_COLLISION);
    }

    private void FixedUpdate()
    {
        elapsed_time += Time.fixedDeltaTime;

        float progress = elapsed_time / GameManager.Instance.DebrisHandler.debris_lifetime;

        gameObject.transform.localScale = Vector3.Lerp(original_scale, Vector3.zero, progress);
        
        if (GameManager.Instance.DebrisHandler.debris_lifetime < elapsed_time)
        {
            gameObject.SetActive(false);
        }
        Debug.Log(progress);
    }

    private void OnEnable()
    {
        elapsed_time = 0;
    }
}
