using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Collider2D), typeof(AudioSource))]
public abstract class Collectable : MonoBehaviour
{
    protected new Collider2D collider = null;
    private Animator animator = null;
    private AudioSource audioSource = null;
    private PlayerManager player = null;

    protected virtual void Awake() 
    {
        collider = GetComponent<Collider2D>();    
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public virtual void Collect()
    {
        collider.enabled = false;
        animator.SetTrigger("Collect");
        if(player == null)
            player = FindObjectOfType<PlayerManager>();

        player.Collect(this);
    }

    public void PlaySFX(float pitchAddition)
    {
        audioSource.pitch = audioSource.pitch + pitchAddition;
        audioSource.Play();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Collect();
        }
    }

    public virtual void TurnOff()
    {
        gameObject.SetActive(false);
    }
}
