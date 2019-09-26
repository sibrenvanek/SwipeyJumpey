using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CirclingEnergy : MonoBehaviour
{
    [SerializeField] private float circleInterval = 1f;
    [SerializeField] private Animator animator = null;
    private bool active = true;
    private SpriteRenderer spriteRenderer = null;

    private void Awake() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    public void StopCircling()
    {
        spriteRenderer.enabled = false;
        active = false;
    }

    public void StartCircling()
    {
        spriteRenderer.enabled = true;
        if(animator != null)
        {
            active = true;
            StartCoroutine(Circle());
        }
        else
        {
            Debug.LogWarning("Cannot start circling because animator is null.");
        }
    }

    private IEnumerator Circle()
    {
        if(active)
        {
            yield return new WaitForSeconds(circleInterval);
            animator.SetTrigger("Circle");
            StartCoroutine(Circle());
        }else{
            yield return null;
        }
    }
}
