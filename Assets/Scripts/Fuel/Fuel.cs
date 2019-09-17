using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Experimental.Rendering.LWRP;

public class Fuel : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] particleSystems = null;
    [SerializeField] private float freezeTime = 0.2f;
    [SerializeField] private float respawnTime = 2f;
    [SerializeField] private float fadeInTime = .5f;
    [SerializeField] private float forceBoost = 2f;
    [SerializeField] private Vector2 velocityLoss = new Vector2(2f,2f);
    private Light2D light2d = null;
    private new Collider2D collider = null;
    private SpriteRenderer spriteRenderer = null;
    private bool frozen = false;

    private void Awake()
    {
        light2d = GetComponentInChildren<Light2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();    
        collider = GetComponent<Collider2D>();
    }

    public void PickUp(Rigidbody2D rigidbody)
    {
        AddForceBoost(rigidbody);
        PlayParticleSystems();
        StartCoroutine(FreezeFrame());
        StartCoroutine(Respawn());
    }

    private IEnumerator FreezeFrame()
    {
        if(!frozen)
        {
            float original = Time.timeScale;
            Time.timeScale = 0;
            frozen = true;
            yield return new WaitForSecondsRealtime(freezeTime);
            Time.timeScale = original;
            frozen = false;
        }
    }

    private void PlayParticleSystems()
    {
        foreach (var particleSystem in particleSystems)
        {
            particleSystem.Play();
        }
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
        light2d.enabled = false;
        collider.enabled = false;
    }

    private void Activate()
    {
        collider.enabled = true;
        spriteRenderer.DOFade(1, fadeInTime);
        light2d.enabled = true;
    }

    private void AddForceBoost(Rigidbody2D rigidbody)
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x / velocityLoss.x, rigidbody.velocity.y / velocityLoss.y);
        rigidbody.AddForce(Vector2.up * forceBoost, ForceMode2D.Impulse);
    }
}
