using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class GameManager : MonoBehaviour
{
    public GameObject path;
    public GameObject jumpPlatform;

    public GameObject player;
    public Transform startPoint;
    private GameObject _lastSpawnObject;

    private void Awake()
    {
    }

    private void Update()
    {
    }

    private void SpawnPlatform()
    {
        for (int i =0 ; i < 100; i++)
        {
            _lastSpawnObject = Instantiate(jumpPlatform, startPoint.position + new Vector3(0, 0, (Random.Range(1,5)+i)*10), Quaternion.identity, parent:path.transform);
        }
    }
}
