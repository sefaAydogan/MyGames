using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rays : MonoBehaviour
{
    
    private void Update()
    {
        Sensor();
        
    }

    private void Sensor()
    {
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position,-Vector3.forward,out hit,2f))
        {
            
            
            
        }
    }
}
