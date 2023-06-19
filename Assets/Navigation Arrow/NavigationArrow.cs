using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class NavigationArrow : MonoBehaviour
{
    public List<GameObject> navigation_points;
    public GameObject hand;

    public GameObject closest_point;
    public float closest_distance;

    private void Awake()
    {
        if (navigation_points.Count > 0)
        {
            closest_point = navigation_points[0];
        }
    }

    private void Update()
    {
        if (navigation_points.Count > 0)
        {
            determineClosestPoint();
            rotateArrow();
        }
        else
        {
            // If nothing to point at, point forward.
            hand.transform.localRotation = Quaternion.identity;
        }
    }

    private void rotateArrow()
    {
        Vector3 direction = closest_point.transform.position - hand.transform.position;
        direction = direction.normalized;

        Quaternion rotation = Quaternion.LookRotation(direction);
        hand.transform.rotation = rotation;
        hand.transform.localRotation = Quaternion.Euler(0.0f, hand.transform.localRotation.eulerAngles.y, 0.0f);
    }

    private void determineClosestPoint()
    {
        foreach (var point in navigation_points)
        {
            closest_distance = Vector3.Distance(hand.transform.position, closest_point.transform.position);
            float hand_point_distance = Vector3.Distance(hand.transform.position, point.transform.position);

            if (Mathf.Abs(hand_point_distance) < Mathf.Abs(closest_distance))
            {
                closest_point = point;
            }
        }
    }
}
