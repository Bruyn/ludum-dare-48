using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 positionOffset = new Vector3(-5.2f, -7f, -5f);
    
    private GameObject player;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        transform.position = player.transform.position - positionOffset;
    }
}
