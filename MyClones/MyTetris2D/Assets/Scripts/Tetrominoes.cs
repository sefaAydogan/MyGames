using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tetrominoes : MonoBehaviour
{
    public Vector3 rotationPoint;
    private float _previousTime;
    public float fallTime = 0.8f;
    public static int Height = 20;
    public static int Width = 10;
    private static Transform[,] _grid = new Transform[Width,Height];
    public Vector3 endPoint;
    public Text restartText;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position += new Vector3(-1,0,0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(-1,0,0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(+1,0,0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(+1,0,0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint),new Vector3(0,0,1),90);
            if (!ValidMove())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint),new Vector3(0,0,1),-90);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(sceneName: "MyScene");
            Spawner.IsFinished = false;
        }

        if (Time.time - _previousTime > (Input.GetKey(KeyCode.S) ? fallTime/10 :fallTime))
        {
            transform.position += new Vector3(0,-1,0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(0,-1,0);
                AddGrid();
                CheckForLine();
                if (transform.position.y <= endPoint.y)
                {
                    this.enabled = false;
                    Spawner.IsFinished = false;
                    FindObjectOfType<Spawner>().NewTetromino();
                }
                else
                {
                    Spawner.IsFinished = true;
                }
                
            }
            _previousTime = Time.time;
        }
    }

    private void CheckForLine()
    {
        for (int i = Height-1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }

    // private bool CheckForEnd(int )
    // {
    //     if (expr)
    //     {
    //         
    //     }
    //     return false;
    // }

    private void DeleteLine(int i)
    {
        for (int j = 0; j < Width; j++)
        {
            Destroy(_grid[j,i].gameObject);
            _grid[j, i] = null;
        }
    }

    private void RowDown(int i)
    {
        for (int y = i; y < Height; y++)
        {
            for (int j = 0; j < Width; j++)
            {
                if (_grid[j,y] != null)
                {
                    _grid[j, y - 1] = _grid[j, y];
                    _grid[j, y] = null;
                    _grid[j, y - 1].transform.position -= new Vector3(0,1,0);
                }
            }
        }
    }

    private bool HasLine(int i)
    {
        for (int j = 0; j < Width; j++)
        {
            if (_grid[j,i] == null)
            {
                return false;
            }
        }

        return true;
    }

    private void AddGrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (_grid != null) _grid[roundedX, roundedY] = children;
        }
    }

    private bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);
            if (roundedX < 0 || roundedX >=Width || roundedY < 0 || roundedY >=Height)
            {
                return false;
            }

            if (_grid[roundedX,roundedY] != null)
            {
                return false;
            }

        }

        return true;
    }
}

