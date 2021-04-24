using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 positionOffset = new Vector3(0.6f, -1.5f, 0f);
    
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
