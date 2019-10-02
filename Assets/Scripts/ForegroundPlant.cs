using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForegroundPlant : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("JetpackLaunch"))
            animator.SetTrigger("Impact");
    }
}
