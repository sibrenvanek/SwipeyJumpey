using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;

public abstract class Fuel : MonoBehaviour
{
    public event Action OnPickUp = delegate { };

    [SerializeField] private float freezeTime = 0.2f;
    [SerializeField] private float respawnTime = 2f;
    [SerializeField] private float fadeInTime = .5f;
    [SerializeField] private Animator pickUpAnimator = null;
    private new Collider2D collider = null;
    private SpriteRenderer spriteRenderer = null;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    
        collider = GetComponent<Collider2D>();
    }

    public void PickUp(Rigidbody2D rigidbody)
    {
        OnPickUp.Invoke();
        pickUpAnimator.SetTrigger("PickUp");
        AddForceBoost(rigidbody);
        FreezeManager.Instance.Freeze(freezeTime);
        StartCoroutine(Respawn());
    }


    private IEnumerator Respawn()
    {
        DeActivate();
        yield return new WaitForSeconds(respawnTime);
        Activate();
    }

    private void DeActivate()
    {
        spriteRenderer.DOKill();
        spriteRenderer.DOFade(0, 0);
        collider.enabled = false;
    }

    private void Activate()
    {
        collider.enabled = true;
        spriteRenderer.DOFade(1, fadeInTime);
    }

    public virtual void AddForceBoost(Rigidbody2D rigidbody) {}
}
