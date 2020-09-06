using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isHit : MonoBehaviour
{
    public static bool IsObjectHit = false;

    public static bool IsGameFinish = false;
    
   
    private void OnCollisionEnter(Collision other)
    {
        IsObjectHit = true;
        this.transform.gameObject.GetComponent<Rigidbody>().useGravity = true;
        this.transform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        if (IsFisished(other.transform))
        {
            IsGameFinish = true;
        }
    }

    public static bool IsFisished(Transform topLimit)
    {
        if (topLimit.position.y >= 17f)
        {
            return true;
        }

        return false;
    }
    
    
}

