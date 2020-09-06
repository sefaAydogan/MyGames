using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject[] tetrominoes;
    public GameObject panel;
    public static bool IsFinished = false;
    private void Start()
    {
        NewTetromino();
    }

    private void Update()
    {
        FinishScreen(IsFinished);
    }

    public bool FinishScreen(bool ısFinished)
    {
        if (ısFinished)
        {
            panel.SetActive(true);
        }

        return false;
    }
    public void NewTetromino()
    {
        Instantiate(tetrominoes[Random.Range(0, tetrominoes.Length)], transform.position, Quaternion.identity);
    }

}
