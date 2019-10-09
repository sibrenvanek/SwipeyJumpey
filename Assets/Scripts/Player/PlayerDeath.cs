using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerDeath : MonoBehaviour
{

    public event Action OnDeath = delegate { };

    private PlayerMovement playerMovement;
    private Animator animator;
    [SerializeField] private Jetpack jetpack = null;
    private bool dying = false;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    public void Die()
    {
        if (dying)
            return;

        dying = true;
        OnDeath.Invoke();
        animator.SetTrigger("Die");
        playerMovement.KillVelocity();
        playerMovement.CancelJump();
        playerMovement.RemoveGravity();
        playerMovement.Disable();
    }

    public void DoneWithDying()
    {
        StartCoroutine(WaitAndRespawn(.5f, .5f));
        jetpack.Explode();
    }

    
    private IEnumerator WaitAndRespawn(float waitTime = 0f, float time = 1f)
    {
        DarthFader.Instance.FadeGameOut(time);

        yield return new WaitForSeconds(waitTime);

        dying = false;
        GameManager.Instance.SendPlayerToLastCheckpoint();
        playerMovement.RestoreGravity();
        DarthFader.Instance.FadeGameInInSeconds(.5f, time);
    }
}
