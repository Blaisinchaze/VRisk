using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTest : MonoBehaviour
{
    public GameObject test;
    private GestureController gesture;
    
    // Start is called before the first frame update
    void Start()
    {
        gesture = test.GetComponent<GestureController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gesture.moving)
        {
            transform.Translate(Vector3.back * (gesture.speed * Time.fixedDeltaTime));
        }
    }
}
