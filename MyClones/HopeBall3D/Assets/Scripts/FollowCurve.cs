using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FollowCurve : MonoBehaviour
{
    #region Public Variables
        public AnimationCurve curve;
        public GameObject centerSphereHitEffect;
        public GameObject sideSphereHitEffect;
        public float ballSensitivity = 1.5f;
        public Path path;
        public float minStartPos;
        public float maxStartPos;
        public float speed = 5f;
        public Text scoreText;
        public GameObject panel;
        public AudioSource hitPlatformAudio;
    #endregion
    

    #region Private Variables
        private float _mouseLastXPos;
        private float _mouseXPosition;
        private bool _started = false;
        private int _nextNode = 1;
        private Text _panelScoreText;
        private List<Transform> _node;
        private bool _isGameEnded = false;
        private float _horizontalMovement;
        private bool _isNode = true;
        private float _progress;
        private float _startDistance;
        private float _score;
    #endregion
    
    private void Start()
    {
        hitPlatformAudio = GetComponent<AudioSource>();
        scoreText.text = "Score :" + 0f;
        _panelScoreText = panel.transform.GetChild(2).GetComponent<Text>();
    }

    private void Update()
    {
        GetHorizontalInput();
    }

    private void GetHorizontalInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _mouseLastXPos = Input.mousePosition.x;
        }
        if (Input.GetMouseButton(0))
        {
            _mouseXPosition = Input.mousePosition.x - _mouseLastXPos;
            _mouseLastXPos = Input.mousePosition.x;
        }
    }
    private void FixedUpdate()
    {
        if (!_started)
        {
            _started = true;
        }
        if (_isNode)
        {
            _node = path.nodes2;
            _isNode = false;
            _startDistance = _node[_nextNode].position.z - transform.position.z;
        }
        else
        {
            if (path.nodes2 != null && !_isGameEnded)
            {
                float distanceZ = _node[_nextNode].position.z - transform.position.z;
                _progress = 1 - distanceZ / _startDistance;
                float posY = curve.Evaluate(_progress)*(distanceZ/2f);
                float yaxis = 0.7f;
                _horizontalMovement = _mouseXPosition;
                
                transform.position = new Vector3(
                    Mathf.Clamp(transform.position.x + _horizontalMovement*ballSensitivity*Time.fixedDeltaTime, minStartPos, maxStartPos),
                    y: posY + yaxis,
                    transform.position.z + speed * Time.fixedDeltaTime);

                if ((_node[_nextNode].position.z <= transform.position.z))
                {
                    _nextNode++;
                    _startDistance = _node[_nextNode].position.z - transform.position.z;
                    RaycastSensor();
                }
            }
            else
            {
                panel.SetActive(true);
                _panelScoreText.text = "Scored : " + _score;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("SampleScene");
            _score = 0;
        }
    }

    private void RaycastSensor()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if (hit.transform.CompareTag("midcylinder"))
            {
                var x = Instantiate(centerSphereHitEffect, transform.position, Quaternion.identity);
                _score += 10;
                scoreText.text = "Score : " + _score;
                hitPlatformAudio.Play();
                Destroy(x, 2f);
            }
            else if (hit.transform.CompareTag("sidecylinder"))
            {
                var y = Instantiate(sideSphereHitEffect, transform.position, Quaternion.identity);
                _score += 5;
                scoreText.text = "Score : " + _score;
                hitPlatformAudio.Play();
                Destroy(y, 2f);
            }
        }
        else
        {
            _isGameEnded = true;
            Rigidbody rb = this.transform.gameObject.AddComponent<Rigidbody>();
            rb.AddForce(Vector3.forward*200f,ForceMode.Force);
        }
    }
    
}