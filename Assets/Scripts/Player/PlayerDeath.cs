using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;

    private Animator animator;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    public void Die()
    {
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