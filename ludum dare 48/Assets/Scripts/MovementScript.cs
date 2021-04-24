using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float Movementspeed = 5f;
    public Animator Animator;
    public LayerMask screenRayMask;

    private Rigidbody _rigidbody;
    private Vector3 _mousePosition;
    private Camera _camera;
    
    void Start()
    {
        _camera = Camera.main;
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        float horAxis = Input.GetAxis("Horizontal");
        float vertAxis = Input.GetAxis("Vertical");
        
        Vector3 movementVector = new Vector3(horAxis, 0, vertAxis);
        movementVector.Normalize();
        
        _rigidbody.MovePosition(_rigidbody.transform.position + movementVector * (Movementspeed * Time.deltaTime));
        
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Physics.Raycast(ray, out hit, 100f, screenRayMask.value);
        _mousePosition = hit.point;

        Vector3 lookingDirection = (_mousePosition - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(lookingDirection);
        
        Animator.SetFloat("MovementSpeed", movementVector.magnitude);
    }
    
}
