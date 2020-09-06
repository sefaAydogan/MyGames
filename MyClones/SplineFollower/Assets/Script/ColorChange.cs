using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    private void Start()
    {
      
    }

    private void Update()
    {
        
        transform.RotateAround(transform.position,transform.up,45*Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.gameObject.GetComponent<Renderer>().material.color = this.GetComponent<Renderer>().material.color;
    }
}
