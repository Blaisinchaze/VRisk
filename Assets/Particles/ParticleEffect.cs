using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObjects/ParticleEffect")]
public class ParticleEffect : ScriptableObject
{
    public ParticleManager.ParticleID id;
    public GameObject prefab;
}
