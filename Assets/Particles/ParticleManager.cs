using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ParticleManager : MonoBehaviour
{
    public enum ParticleID
    {
        DEBRIS_IMPACT
    }

    public struct ParticleEmitter
    {
        public GameObject parent;
        public List<ParticleSystem> particle_systems;
        
        public ParticleEmitter(GameObject _parent, List<ParticleSystem> _systems)
        {
            parent = _parent;
            particle_systems = _systems;
        }
    }
    
    public Dictionary<ParticleID, List<ParticleEmitter>> particle_emitters;
    public List<Pair<ParticleEffect, int>> number_of_pooled_effects;

    private ParticleSystem test;

    private void Awake()
    {
        particle_emitters = new Dictionary<ParticleID, List<ParticleEmitter>>();
        
        foreach (var set in number_of_pooled_effects)
        {
            for (int i = 0; i < set.second; i++)
            {
                var particle_object = Instantiate(set.first.prefab, transform, true);
                List<ParticleSystem> particle_systems =  new List<ParticleSystem>(particle_object.GetComponentsInChildren<ParticleSystem>());

                if (!particle_emitters.ContainsKey(set.first.id))
                {
                    particle_emitters.Add(set.first.id, new List<ParticleEmitter>());
                }
                
                particle_emitters[set.first.id].Add(new ParticleEmitter(particle_object, particle_systems));
            }
        }
    }

    private void Start()
    {
        GameManager.Instance.InputHandler.input_asset.InputActionMap.Debug.started += Test;
    }

    private void Test(InputAction.CallbackContext _context)
    {
        triggerEffect(ParticleID.DEBRIS_IMPACT, new Vector3(-26.4012032f,10.7600002f,-49.6206245f), new Vector3(0,0,0));
    }

    public void triggerEffect(ParticleID _id, Vector3 _location, Vector3 _rotation)
    {
        if (!particle_emitters.ContainsKey(_id))
        {
            Debug.Log("ParticleEmitters dictionary does not contain key " + _id);
            return;
        }

        foreach (var emitter in particle_emitters[_id])
        {
            if (!emitter.parent.activeSelf)
            {
                emitter.parent.SetActive(true);

                emitter.parent.transform.position = _location;
                emitter.parent.transform.rotation = Quaternion.Euler(_rotation);
                
                StartCoroutine(delayedDeactivation(emitter));

                //
                test = emitter.particle_systems[0];
                return;
            }
            
            Debug.Log("No available " + _id + " particle available in the pool - consider increasing the pool");
        }
    }

    public void triggerEffect(ParticleID _id, Vector3 _location, Vector3 _rotation, Transform _parent, bool _relative_to_parent)
    {
        if (!particle_emitters.ContainsKey(_id))
        {
            Debug.Log("ParticleEmitters dictionary does not contain key " + _id);
            return;
        }

        foreach (var emitter in particle_emitters[_id])
        {
            if (!emitter.parent.activeSelf)
            {
                emitter.parent.SetActive(true);
                emitter.parent.transform.SetParent(_parent);

                if (_relative_to_parent)
                {
                    emitter.parent.transform.localPosition = _location;
                    emitter.parent.transform.localRotation = Quaternion.Euler(_rotation);
                }
                else
                {
                    emitter.parent.transform.position = _location;
                    emitter.parent.transform.rotation = Quaternion.Euler(_rotation);
                }

                StartCoroutine(delayedDeactivation(emitter));

                return;
            }
        }
        
        Debug.Log("No available " + _id + " particle available in the pool - consider increasing the pool");
    }

    IEnumerator delayedDeactivation(ParticleEmitter _emitter)
    {
        yield return new WaitUntil(() => hasEffectStopped(_emitter));
        _emitter.parent.SetActive(false);
    }

    private bool hasEffectStopped(ParticleEmitter _emitter)
    {
        foreach (var system in _emitter.particle_systems)
        {
            if (system.isPlaying)
            {
                return false;
            }
        }
        return true;
    }
}
