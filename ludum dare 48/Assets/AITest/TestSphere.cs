using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSphere : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject sphere;
    
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 cameraPosition = Camera.main.transform.position;
        if (Physics.Raycast(cameraPosition, (transform.position - cameraPosition).normalized, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                sphere.SetActive(false);
            }
            else
            {
                sphere.SetActive(true);
            }
        }
        
        
    }
}
