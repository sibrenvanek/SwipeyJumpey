using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerDeath : MonoBehaviour
{

    public event Action OnDeath = delegate { };

    private PlayerMovement playerMovement;
    private Animator animator;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    public void Die()
    {
        OnDeath.Invoke();
        animator.SetTrigger("Die");
        playerMovement.KillVelocity();
        playerMovement.CancelJump();
        playerMovement.RemoveGravity();
    }

    public void DoneWithDying()
    {
        GameManager.Instance.SendPlayerToLastCheckpoint();
        playerMovement.RestoreGravity();
    }
}
