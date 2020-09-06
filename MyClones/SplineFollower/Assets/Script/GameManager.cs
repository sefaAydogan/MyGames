using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    
   
    
    
    public GameObject obstacles;
    private Vector3 _spawnPoint;
    private List<Vector3> _cubePositions;
    private List<int> _cubeId;
    
    public Material pink;
    public Material blue;
    public Material yellow;
    public Material red;
    private List<Material> _colorChange;
    public GameObject colorChanger;
    private List<GameObject> _colorChangerList;

    void Start()
    {
        Invoke(nameof(CreateLevel),1f);
    }

    private void CreateLevel()
    {
        _cubePositions = new List<Vector3>();
        _cubeId = new List<int>();
        _cubePositions.Add(new Vector3(2, 1, -4));
        _cubePositions.Add(new Vector3(4, 1, -4));
        _cubePositions.Add(new Vector3(0, 1, -4));
        _cubePositions.Add(new Vector3(-2, 1, -4));
        _cubePositions = _cubePositions.OrderBy(a => Guid.NewGuid()).ToList();
        _cubeId.Add(0);
        _cubeId.Add(1);
        _cubeId.Add(2);
        _cubeId.Add(3);

        for (int i = 0; i < 50; i++)
        {
            _cubeId = _cubeId.OrderBy(a => Guid.NewGuid()).ToList();
            obstacles.transform.GetChild(_cubeId[0]).transform.position = _cubePositions[0];
            obstacles.transform.GetChild(_cubeId[1]).transform.position = _cubePositions[1];
            obstacles.transform.GetChild(_cubeId[2]).transform.position = _cubePositions[2];
            obstacles.transform.GetChild(_cubeId[3]).transform.position = _cubePositions[3];
            _spawnPoint = new Vector3(0, 0, (i + 1) * 50);
            Instantiate(obstacles, _spawnPoint, Quaternion.identity);
        }

        _colorChange = new List<Material> {pink, blue, yellow, red};
        _colorChangerList = new List<GameObject>();
        for (int i = 0; i < 10; i++)
        {
            _colorChange = _colorChange.OrderBy(a => Guid.NewGuid()).ToList();

            _spawnPoint = new Vector3(0, 2, (i + 1) * 120);
            _colorChangerList.Add(Instantiate(colorChanger, _spawnPoint, Quaternion.identity));
            _colorChangerList[i].GetComponent<Renderer>().material.color = _colorChange[i % 4].color;
        }
    }


    void Update()
    {
        
    }
    
}


