using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
