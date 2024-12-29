using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] float distanceToCover;
    [SerializeField] float speed = 2.0f;

    private Vector3 startingPosition;
    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        Vector3 v = startingPosition;
        v.x = distanceToCover * Mathf.Sin(Time.time * speed);
        transform.position = v;
    }
}
