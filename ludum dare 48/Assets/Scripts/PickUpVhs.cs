using UnityEngine;

public class PickUpVhs : PickUp
{
    protected override void DoAction(GameObject player)
    {
        PlayerAttack playerAttack = player.GetComponent<PlayerAttack>();
        playerAttack.EnableKick();
    }
}
