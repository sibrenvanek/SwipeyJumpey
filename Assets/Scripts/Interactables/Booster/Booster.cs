using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer), typeof(AudioSource))]
public abstract class Booster : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer = null;
    protected new Collider2D collider = null;
    protected AudioSource audioSource = null;

    [Header("Base options")]
    [SerializeField] private Animator animator = null;
    [SerializeField] private float force = 2f;
    [SerializeField] private AudioClip boostSFX = null;

    protected virtual void Awake() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    protected abstract void Activate(GameObject player);

    protected void Boost(Rigidbody2D rigidbody, Transform root = null)
    {
        if(root == null)
            root = transform;

        rigidbody.transform.position = transform.position;
        rigidbody.velocity = root.up * force;
        audioSource.PlayOneShot(boostSFX);
    }

    protected virtual void Disable()
    {
        collider.enabled = false;
    }

    protected virtual void Enable()
    {
        collider.enabled = true;
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Body"))
        {
            Activate(other.transform.root.gameObject);
        }
    }
}
