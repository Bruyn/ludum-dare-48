using System.Collections;
using System.Collections.Generic;
using Ludiq.Peek;
using UnityEngine;
using UnityEngine.UI;

public class RespawnController : MonoBehaviour
{
    public GameObject respawnText;
    public Animator animator;
    public GameObject Gun;

    private Health _health;
    private RagdollOnDeath _ragdoll;
    
    // Start is called before the first frame update
    void Start()
    {
        _health = GetComponent<Health>();
        _health.OnDeath.AddListener(HanldeDeath);

        _ragdoll = GetComponent<RagdollOnDeath>();
    }

    void HanldeDeath(Damage damage)
    {
        respawnText.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_health.IsDead())
        {
            return;
        }

        if (Input.GetKey(KeyCode.R))
        {
            _health.currentHealth = _health.maxHealth;
            _ragdoll.DisableRagdoll();
            respawnText.SetActive(false);

            if (!GetComponent<PlayerAttack>().meleeAtack)
            {
                Gun.SetActive(true);
                GetComponent<PlayerAttack>().ResetGun();
            }
            animator.enabled = true;
        }
    }
}
