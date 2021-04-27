using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 positionOffset = new Vector3(-5.2f, -7f, -5f);
    public bool IsActive = true;
    public GameObject Canvas;
    
    private GameObject player;

    private Vector3 targetPosition = Vector3.zero;
    private Vector3 startPosition = Vector3.zero;
    private bool canInterpolate = false;
    private float fraction = 0;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void ActivateAbilityToInterpolate()
    {
        canInterpolate = true;
    }

    void Update()
    {
        if (canInterpolate && !IsActive && targetPosition == Vector3.zero && Input.anyKey)
        {
            targetPosition = player.transform.position - positionOffset;
            startPosition = transform.position;
            player.GetComponent<MovementScript>().Animator.SetTrigger("stopSmoking");
            MusicManager.Instance.PlayMusic(1);
        }

        if (targetPosition == transform.position && !IsActive)
        {
            Canvas.GetComponent<UiSctipt>().HideMenu();
            IsActive = true;
            StartCoroutine(StartGameplayCoroutine());
        }
        
        if (IsActive)
        {
            transform.position = player.transform.position - positionOffset;
            return;
        }

        if (targetPosition != Vector3.zero)
        {
            fraction += Time.deltaTime * 5;
            fraction = Mathf.Clamp(fraction, 0, 1);
            transform.position = Vector3.Lerp(startPosition, targetPosition, fraction);
        }
    }
    
    private IEnumerator StartGameplayCoroutine()
    {
        yield return new WaitForSeconds(6);
        player.GetComponent<MovementScript>().StartGameplay();
    }
}
