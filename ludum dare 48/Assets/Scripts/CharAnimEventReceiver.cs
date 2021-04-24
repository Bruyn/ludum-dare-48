using System.Collections;
using System.Collections.Generic;
using Sigtrap.Relays;
using UnityEngine;

public class CharAnimEventReceiver : MonoBehaviour
{
    
    public Relay<bool> KickLanded = new Relay<bool>();
    
    void OnKickLanded()
    {
        KickLanded.Dispatch(true);
    }
    
}
