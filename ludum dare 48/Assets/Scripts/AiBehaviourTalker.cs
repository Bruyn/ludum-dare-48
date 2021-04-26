using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AiBehaviourTalker : MonoBehaviour
{

    public Animator Animator;

    public void ExecutePunch()
    {
        Animator.SetBool("isPunching", true);
    }

    public bool IsPunching()
    {
        return Animator.GetBool("isPunching");
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("weight is " + RigLayer.rig.weight);
    }
}
