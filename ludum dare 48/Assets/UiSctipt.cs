using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiSctipt : MonoBehaviour
{
    public Text MenuTitle;
    public Text PressAny;

    private Animator _titleAnimator;
    private Animator _anyKeyAnimator;
    
    void Start()
    {
        _titleAnimator = MenuTitle.GetComponent<Animator>();
        _anyKeyAnimator = PressAny.GetComponent<Animator>();
    }

    public void StartMenu()
    {
        _titleAnimator.Play("AppearAnimation");
        StartCoroutine(AnyKeyCoroutine());

    }

    public void HideMenu()
    {
        _titleAnimator.Play("disappearAnimation");
        _anyKeyAnimator.Play("disappearButton");
    }
    
    private IEnumerator AnyKeyCoroutine()
    {
        yield return new WaitForSeconds(2);
        _anyKeyAnimator.Play("appearButton");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
