using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityInput;
using UnityEngine;

public class TestDestination : MonoBehaviour
{
    protected UnityEngine.AI.NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public Vector3 debugrayend;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast (ray, out hit))
            {
                Debug.DrawLine (hit.point, debugrayend, Color.red);
                Debug.Log(hit.point);
                navMeshAgent.SetDestination(hit.point);
                _mousePositio = hit.point;
            }
        }
    }

    private Vector3 _mousePositio;
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_mousePositio, 1);
    }
}