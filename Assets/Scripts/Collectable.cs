using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Collider2D))]
public abstract class Collectable : MonoBehaviour
{
    protected new Collider2D collider = null;
    private Animator animator = null;

    protected virtual void Awake() 
    {
        collider = GetComponent<Collider2D>();    
        animator = GetComponent<Animator>();
    }

    public virtual void Collect()
    {
        collider.enabled = false;
        animator.SetTrigger("Collect");
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
