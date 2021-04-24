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

        Vector3 transformedMovement = _camera.transform.TransformVector(movementVector);        
        _rigidbody.MovePosition(_rigidbody.transform.position + transformedMovement * (Movementspeed * Time.deltaTime));
        
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Physics.Raycast(ray, out hit, 100f, screenRayMask.value);
        _mousePosition = hit.point;
        
        Vector3 lookingDirection = (_mousePosition - transform.position).normalized;
        lookingDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(lookingDirection);

        Vector3 movementX = new Vector3(horAxis, 0, 0);
        Vector3 movementZ = new Vector3(0, 0, vertAxis);
        
        Vector3 lookingX = new Vector3(lookingDirection.x, 0, 0);
        Vector3 lookingZ = new Vector3(0, 0, lookingDirection.z);
        
        
        // Animator.SetFloat("MovementSpeed", movementVector.magnitude);
    }
    
}
