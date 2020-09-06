using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public List<GameObject> prefabs;

    public GameObject longStick;
    public GameObject toLeftL;
    public GameObject toRightL;
    public GameObject cube;
    public GameObject toLeftZ;
    public GameObject toRightZ;
    public Transform topPlatform;
    public Transform leftBorder;
    public Transform rightBorder;
    private  int _increment = 0;
    public Text textGameFinish;
    private GameObject _currentObject;
    public GameObject gameFinishPanel; 
        
    float timer = 0f;
    private float wait2sn = 0;
    
    void Start()
    {
        prefabs = new List<GameObject>();
        prefabs.Add(longStick);
        prefabs.Add(toLeftL);
        prefabs.Add(toRightL);
        prefabs.Add(cube);
        prefabs.Add(toLeftZ);
        prefabs.Add(toRightZ);
        
        int rnd = Random.Range(0, 9);
        prefabs = prefabs.OrderBy(a => Guid.NewGuid()).ToList();
        Vector3 vec = new Vector3(rnd*2,0,0);
        _currentObject = Instantiate(prefabs[rnd%6], leftBorder.position + vec, Quaternion.identity);
        _increment++;

    }
    
    void Update()
    {
        _currentObject.transform.position += Vector3.down * (Time.deltaTime * 5);
        timer += Time.deltaTime;
        int rnd = Random.Range(0, 9);
        if (timer > wait2sn && isHit.IsObjectHit)
        {
            isHit.IsObjectHit = false;
            wait2sn += 2f;
            prefabs = prefabs.OrderBy(a => Guid.NewGuid()).ToList();
            Vector3 vec = new Vector3(rnd*2,0,0);
            _currentObject = Instantiate(prefabs[rnd%6], leftBorder.position + vec, Quaternion.identity);
            _increment++;
            _currentObject.transform.position += Vector3.down;

        }

        if (Input.GetKeyDown(KeyCode.L))
        {
           _currentObject.transform.Rotate(Vector3.forward*90);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            _currentObject.transform.Rotate(Vector3.forward * (90 * -1));
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _currentObject.transform.position += Vector3.right;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            _currentObject.transform.position += Vector3.left;
        }

        if (isHit.IsGameFinish)
        {
            gameFinishPanel.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            isHit.IsGameFinish = false;
            gameFinishPanel.gameObject.SetActive(false);
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
}
