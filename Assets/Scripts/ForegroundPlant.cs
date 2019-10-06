using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForegroundPlant : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer = null;

    [SerializeField] private bool flip = false;

    private void OnValidate()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        if (flip)
        {
            FlipSprite();
        }
    }

    private void Awake()
    {
        if (flip)
        {
            FlipAnimation();
        }
    }

    private void FlipSprite()
    {
        spriteRenderer.flipX = true;
    }

    private void FlipAnimation()
    {
        animator.SetFloat("Speed", -1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("JetpackLaunch"))
            animator.SetTrigger("Impact");
    }
}
