using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleAnimation : MonoBehaviour
{
    public float speed = 0.2f;
    public float strength = 9f;
    private float _randomOffset;

    private void Start()
    {
        _randomOffset = Random.Range(0f, 2f);
    }

    private void Update()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Sin(Time.time * speed + _randomOffset) * strength;
        transform.position = pos;
    }
}
