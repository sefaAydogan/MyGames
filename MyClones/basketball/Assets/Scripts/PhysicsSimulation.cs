using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PhysicsSimulation 
{
    public static Vector3 Simulate(float t,PhysicsData physicsData)
    {
        Vector3 gravity = Physics.gravity;
        float posX, posY, posZ;
        
        posX = CalculateAcceleration(gravity.x, t) +physicsData.startVelocity.x * t;
        posY = physicsData.startVelocity.y * t + CalculateAcceleration(gravity.y, t);
        posZ = 0;//physicsData.startVelocity.z * t + CalculateAcceleration(gravity.z, t);
        
        Vector3 position = physicsData.startPosition + new Vector3(posX,posY,posZ);

        return position;
    }
    public static Vector3 Move(float t,PhysicsData physicsData)
    {
        Vector3 gravity = Physics.gravity;
        float posX, posY, posZ;
        
        posX = CalculateAcceleration(gravity.x, t) +physicsData.startVelocity.x * t;
        posY = physicsData.startVelocity.y * t + CalculateAcceleration(gravity.y, t);
        posZ = 0;//physicsData.startVelocity.z * t + CalculateAcceleration(gravity.z, t);
        
        Vector3 position = physicsData.startPosition + new Vector3(posX,posY,posZ);

        return position;
    }


    private static float CalculateAcceleration(float a,float t)
    {
        // 1/2 a*t^2
        return 0.5f * a * Mathf.Pow(t,2);
    }
}
[System.Serializable]
public struct PhysicsData
{
    public Vector3 startVelocity;
    public Vector3 startPosition;


}
