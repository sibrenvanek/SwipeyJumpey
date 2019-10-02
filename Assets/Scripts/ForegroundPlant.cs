using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForegroundPlant : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer = null;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() 
    {
        SetRandomFlip();    
    }

    private void SetRandomFlip()
    {
        int random = Random.Range(0, 20);
        spriteRenderer.flipX = random % 2 == 0 ? true : false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("JetpackLaunch"))
            animator.SetTrigger("Impact");
    }
}
