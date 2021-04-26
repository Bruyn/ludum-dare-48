using UnityEngine;

public class PickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerAttack>() == null)
            return;
        
        DoAction(other.gameObject);
        Destroy(this.gameObject);
    }

    protected virtual void DoAction(GameObject player)
    {
        
    }
}
