using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public Rigidbody physics;
    public float maxX;
    public float minX;
    public float power = 100;

    public float sensorLength = 10f;

    private int _playerScore = 0;
    public TMP_Text scoreText;
    public float maxSpeed = 30f;
    private float _speedUp = 100f;

    // Start is called before the first frame update
    void Start()
    {
        physics = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveLeftRight();
        if (_playerScore == _speedUp )
        {
            maxSpeed +=10;
            _speedUp *= 2;
        }
    }

    private void MoveLeftRight()
    {
        float horizontal = 0f;

        if (Input.GetKeyDown(KeyCode.A))
        {
            horizontal = -2f;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            horizontal = 2f;
        }

        Vector3 vec = new Vector3(horizontal, 0, 0);
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x + vec.x, minX, maxX),
            transform.position.y,
            transform.position.z
        );

        scoreText.text = "SCORE : " + _playerScore;
       
    }

    private void FixedUpdate()
    {
        Sensor();
        Go();
    }

    private void Go()
    {
        float currentSpeed = physics.velocity.z;
        if (currentSpeed < maxSpeed)
        {
            physics.AddForce(new Vector3(0, 0, 1f*power*Time.fixedDeltaTime));
        }
        else
        {
            physics.AddForce(new Vector3(0, 0, 0));
        }
    }

    private void Sensor()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position,transform.forward,out hit, sensorLength ))
        {
            if (hit.transform.CompareTag("obstacles"))
            {
                Color myColor = GetComponent<Renderer> ().material.color;
                Color otherColor = hit.transform.GetComponent<Renderer> ().material.color;
                
                if (IsEqualTo(myColor, otherColor))
                {
                    Debug.DrawLine(transform.position,hit.point);
                    hit.collider.isTrigger = true;
                }
            }
        }
        
    }
    public static bool IsEqualTo(Color me, Color other)
    {
        return me.r == other.r && me.g == other.g && me.b == other.b && me.a == other.a;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("turncolor"))
        {
            Destroy(other.gameObject,2f);
        }
        else
        {
            Destroy(other.gameObject.transform.parent.gameObject,2f);
            _playerScore += 5;
        }
        
    }
}
