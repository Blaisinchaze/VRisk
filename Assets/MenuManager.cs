using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Camera camera;

    public float distance_from_player = 2.0f;
    public float start_delay = 0.0f;

    private bool do_once = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (start_delay > 3.0f)
        {
            if (do_once)
            {
                this.transform.position = camera.transform.position +
                                          new Vector3(camera.transform.forward.x, 0, camera.transform.forward.z)
                                              .normalized *
                                          distance_from_player;

                this.transform.LookAt(new Vector3(camera.transform.position.x, camera.transform.position.y,
                    camera.transform.position.z));
                this.transform.forward *= -1;

                do_once = false;
            }
        }
        else
        {
            start_delay += Time.deltaTime;
        }
    }
}
