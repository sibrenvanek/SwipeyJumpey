using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TutorialHand : MonoBehaviour
{
    [Header("Fade animation")]
    [SerializeField] private float fadeTimer = 1f;

    private PlayerMovement playerMovement;
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        playerMovement.OnJump += StopShowing;
    }

    private void StopShowing()
    {
        playerMovement.OnJump -= StopShowing;

        spriteRenderer.DOFade(0, fadeTimer);
        Destroy(gameObject, fadeTimer);
    }
}
