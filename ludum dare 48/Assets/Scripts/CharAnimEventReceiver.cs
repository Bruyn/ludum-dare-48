using System.Collections;
using System.Collections.Generic;
using Sigtrap.Relays;
using UnityEngine;

public class CharAnimEventReceiver : MonoBehaviour
{
    public Relay<bool> KickLanded = new Relay<bool>();
    public Relay<bool> OnStep = new Relay<bool>();
    public Relay<bool> OnPunchFinished = new Relay<bool>();

    public Relay<bool> OnPunch = new Relay<bool>();

    void OnKickLanded()
    {
        KickLanded.Dispatch(true);
    }

    void Step()
    {
        OnStep.Dispatch(true);
    }

    void PunchFinished()
    {
        OnPunchFinished.Dispatch(true);
    }

    void Punch()
    {
        OnPunch.Dispatch(true);
    }
}