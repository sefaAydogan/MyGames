using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraFollow : MonoBehaviour
{
    private Vector3 _distance;
    public Transform player;
    public float moveSpeed = 10f;

    void Start()
    {
        _distance = transform.position - player.position;
    }
    
    void LateUpdate()
    {
        transform.DOMove(player.transform.position+_distance, 1.5f);
        // transform.position = Vector3.Lerp(transform.position,
        //     player.transform.position+_distance, Time.fixedDeltaTime * moveSpeed);
    }
}
