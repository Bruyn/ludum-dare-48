using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AiBehaviourTalker : MonoBehaviour
{

    public Animator Animator;
    public RigLayer RigLayer;
    public GameObject gunModel;

    public CharAnimEventReceiver AnimEventReceiver;

    private void Awake()
    {
        AnimEventReceiver.OnPunchFinished.AddListener(PunchFinished);
    }

    private void PunchFinished(bool _)
    {
        FinishPunch();
    }

    public void ExecutePunch()
    {
        Animator.SetBool("isPunching", true);
        gunModel.SetActive(false);
        ToggleHandsIK(0);
    }

    public void FinishPunch()
    {
        gunModel.SetActive(true);
        ToggleHandsIK(1);
    }

    public bool IsPunching()
    {
        return Animator.GetBool("isPunching");
    }

    private void ToggleHandsIK(int value)
    {
        RigLayer.rig.weight = value;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("weight is " + RigLayer.rig.weight);
    }
}
