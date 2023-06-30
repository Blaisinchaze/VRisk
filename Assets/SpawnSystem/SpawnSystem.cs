using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnSystem : MonoBehaviour
{
    public List<Transform> spawn_points_pool;
    public GameObject player_rig;

    private void Start()
    {
        spawn_points_pool.AddRange(gameObject.GetComponentsInChildren<Transform>());
        spawnPlayer();
    }

    public void spawnPlayer()
    {
        int index = Random.Range(0, spawn_points_pool.Count - 1);
        Debug.Log(spawn_points_pool[index].name);

        player_rig.transform.position = spawn_points_pool[index].position;
    }
}
