using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Path : MonoBehaviour
{

    public Color lineColor;

    private List<Transform> _nodes;
    public List<Transform> nodes2;
    private Vector3 _distance;
    private Transform[] _pathTransforms;
    private Transform[] _pathTransforms2;

    private void Awake()
    {
        PathCreate();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = lineColor;
        _nodes = new List<Transform>();
        _pathTransforms = GetComponentsInChildren<Transform>();

        for (int i = 0; i < _pathTransforms.Length; i++)
        {
            if (_pathTransforms[i] != transform)
            {
                _nodes.Add(_pathTransforms[i]);
            }
        }

        for (int i = 0; i < _nodes.Count; i++)
        {
            Vector3 currentNode = _nodes[i].position;
            Vector3 prevNode = Vector3.zero;
            if (i > 0)
            {
                prevNode = _nodes[i - 1].position;
            }
            else if (i == 0 && _nodes.Count > 1)
            {
                prevNode = _nodes[_nodes.Count - 1].position;
            }

            Gizmos.DrawLine(prevNode, currentNode);
        }
    }
    
    public void PathCreate()
    {
        nodes2 = new List<Transform>();
        _pathTransforms2 = GetComponentsInChildren<Transform>();
        for (int j = 0; j < _pathTransforms2.Length; j++)
        {
            if (_pathTransforms2[j] != transform)
            {
                nodes2.Add(_pathTransforms2[j]);
            }
        }
    }
    
    // public float DistanceBetweenTwoNodes(int current,int next)
    // {
    //     return nodes2[next].position.z - nodes2[current].position.z;
    // }
}
    