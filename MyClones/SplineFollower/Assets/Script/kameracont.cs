using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class kameracont : MonoBehaviour
{
    public GameObject player;

    Vector3 distance_between;

    void Start ()
    {
        distance_between = transform.position - player.transform.position;
    }

    void LateUpdate ()
    {
        transform.position = player.transform.position + distance_between;
    }
}
