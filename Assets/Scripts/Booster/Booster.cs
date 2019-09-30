using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
public abstract class Booster : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer = null;
    protected new Collider2D collider = null;

    [Header("Base options")]
    [SerializeField] private Animator animator = null;
    [SerializeField] private float force = 2f;

    protected virtual void Awake() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
    }

    protected abstract void Activate(GameObject player);

    protected void Boost(Rigidbody2D rigidbody, Transform root = null)
    {
        if(root == null)
            root = transform;
            
        rigidbody.AddForce(root.up * force, ForceMode2D.Impulse);
        //BoostAnimation();
    }

    protected virtual void Disable()
    {
        spriteRenderer.DOKill();
        spriteRenderer.DOFade(0,0);
        collider.enabled = false;
    }

    protected virtual void Enable()
    {
        spriteRenderer.DOKill();
        spriteRenderer.DOFade(1, 0.1f);
        collider.enabled = true;
    }

    private void BoostAnimation()
    {
        animator.SetTrigger("Boost");
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Body"))
        {
            Activate(other.transform.root.gameObject);
        }
    }
}
