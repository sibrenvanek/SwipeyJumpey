using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Experimental.Rendering.LWRP;

public class Fuel : MonoBehaviour
{
    /*************
     * VARIABLES *
     *************/

    /**General**/
    [SerializeField] private float respawnTime = 2f;
    [SerializeField] private float fadeInTime = .5f;
    [SerializeField] private float forceBoost = 2f;
    [SerializeField] private Vector2 velocityLoss = new Vector2(2f,2f);
    private Light2D light = null;
    private float defaultLightIntensity = 0f;
    private SpriteRenderer spriteRenderer = null;
    private new Collider2D collider = null;

    /*************
     * FUNCTIONS *
     *************/

    /**General**/

    private void Awake() 
    {
        light = GetComponentInChildren<Light2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();    
        collider = GetComponent<Collider2D>();
        defaultLightIntensity = light.intensity;
    }
    public void PickUp(Rigidbody2D rigidbody)
    {
        AddForceBoost(rigidbody);
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
        light.intensity = 0f;
        collider.enabled = false;
    }

    private void Activate()
    {
        collider.enabled = true;
        spriteRenderer.DOFade(1, fadeInTime);
        light.intensity = defaultLightIntensity;
    }

    private void AddForceBoost(Rigidbody2D rigidbody)
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x / velocityLoss.x, rigidbody.velocity.y / velocityLoss.y);
        rigidbody.AddForce(Vector2.up * forceBoost, ForceMode2D.Impulse);
    }
}
