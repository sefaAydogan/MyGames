using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Trajectory : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int pointCount;
    //public GameObject point;
    public PhysicsData physicsData;
    public float distance;
    public float power = 0f;
    public float startPosPower = 0f;
    
    private Vector3 _startPoint;
    private Vector3 _currentPoint;
    private Vector3 _direction;
    private float _magnitude;
    private Rigidbody _physics;
    //private bool _isHit = false;
    public float minStartPos;
    public float maxStartPos;
    public Camera cam;
    public static float[] PassTime = new float[1];
    public Text text;
    private float _score = 0;
    private Vector3 _firstPos;
    private bool _isMoveEnd = true;
    private bool _isMouseRelease = false;
    private int _timeCounter = 0;
    private void Start()
    {
        cam = Camera.main;
        PassTime[0] = 0;
        PassTime = new float[pointCount+1];
        _physics = this.GetComponent<Rigidbody>();

    }

    private void Update()
    {
        if (_isMouseRelease)
        {
            _isMoveEnd = false;
            if (_timeCounter < pointCount)
            {
                Vector3 position = PhysicsSimulation.Move(PassTime[_timeCounter], physicsData);
                this.transform.position = position;
                _timeCounter++;
            }

            if (_timeCounter == pointCount)
            {
                _isMoveEnd = true;
                _physics.useGravity = true;
                _physics.isKinematic = false;
            }
            
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0) && _isMoveEnd)
        {
            _physics.useGravity = false;
            _physics.isKinematic = true;
            _timeCounter = 0;
            _isMouseRelease = false;
            this.GetComponent<Rigidbody>().useGravity = false;
            lineRenderer.GetComponent<LineRenderer>().enabled = true;
            _startPoint = cam.ScreenToViewportPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0) && _isMoveEnd)
        {
            _currentPoint =  cam.ScreenToViewportPoint(Input.mousePosition);
            _direction = _currentPoint - _startPoint;
            if (_direction.magnitude < 0.1f)
                return;
            _direction *= -1;
            _magnitude = _direction.magnitude;
            _direction.z = 0;
            physicsData.startVelocity = _direction*20;
            physicsData.startPosition = this.transform.position;
            physicsData.startVelocity.z = 0;
            RenderTrajectoryLine(pointCount,distance);
        }

        if (Input.GetMouseButtonUp(0) && _isMoveEnd)
        {
            lineRenderer.GetComponent<LineRenderer>().enabled = false;
            _isMouseRelease = true;
        }
        
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        _firstPos = this.transform.position;
        _firstPos.z = cam.transform.position.z;
        if (other.CompareTag("isBasket"))
        {
            cam.transform.DOMove(_firstPos, 1f);
            _score++;
            text.text = "Your Score :" + _score;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Destroy(other.transform.parent.gameObject);
    }

    private void DecideStartPoint()
    {
        if (Input.GetMouseButtonDown(0))
        {
            return;
        }
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 vec = new Vector3(horizontal, 0, 0);
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x+vec.x,minStartPos,maxStartPos),
            transform.position.y,
            transform.position.z
        );
    }
    
    private void RenderTrajectoryLine(int pointCount, float pointDistanceTime)
    {
        float time = 0f;
        lineRenderer.positionCount = this.pointCount;
        for (int i = 0; i < pointCount; i++)
        {
            Vector3 position = PhysicsSimulation.Simulate(time, physicsData);
            lineRenderer.SetPosition(i,position);
            time += Time.deltaTime*pointDistanceTime;
            PassTime[i+1] = time;
        }
    }

    // private void RenderTrajectoryWithPoint(int pointCount, float pointDistanceTime)
    // {
    //     float time = 0;
    //     for (int i = 0; i < pointCount; i++)
    //     {
    //         Vector3 position = PhysicsSimulation.Simulate(time, physicsData);
    //         Instantiate(point ,position, Quaternion.identity);
    //         time += Time.fixedDeltaTime*pointDistanceTime;
    //     }
    // }
}
