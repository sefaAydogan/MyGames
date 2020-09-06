using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraFollow : MonoBehaviour
{
    public GameObject sphere;
    public float moveSpeed;
    private Vector3 _distanceBetween;
    private void Start()
    {
        _distanceBetween = transform.position - sphere.transform.position;
    }

    void LateUpdate ()
    {
        transform.position = Vector3.Lerp(transform.position,
            sphere.transform.position+_distanceBetween, Time.deltaTime * moveSpeed);
    }
    
}
