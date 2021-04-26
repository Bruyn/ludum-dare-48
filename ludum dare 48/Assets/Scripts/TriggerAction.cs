using UnityEngine;
using UnityEngine.Events;

public class TriggerAction : PickUp
{
    [SerializeField] private UnityEvent triggerEvent;
    
    protected override void DoAction(GameObject player)
    {
        triggerEvent?.Invoke();
        gameObject.SetActive(false);
    }
}
