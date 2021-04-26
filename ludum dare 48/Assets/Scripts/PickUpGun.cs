using UnityEngine;

public class PickUpGun : PickUp
{
    protected override void DoAction(GameObject player)
    {
        PlayerAttack playerAttack = player.GetComponent<PlayerAttack>();
        playerAttack.EnableGun();
    }
}