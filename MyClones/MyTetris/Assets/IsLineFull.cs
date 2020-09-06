using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class IsLineFull : MonoBehaviour
{
    public static bool[] LineFull;
    private void Start()
    {
        LineFull = new bool[21];
    }

    private void Update()
    {
        CheckLine();
    }

    private void CheckLine()
    {
        
    }
}
