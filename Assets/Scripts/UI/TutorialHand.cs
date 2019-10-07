using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TutorialHand : MonoBehaviour
{
    [Header("Fade animation")]
    [SerializeField] private float fadeTimer = 1f;

    private PlayerMovement playerMovement;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if (ProgressionManager.Instance.GetDisplayedTutorial())
            gameObject.SetActive(false);

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
        ProgressionManager.Instance.SetDisplayedTutorial();

        spriteRenderer.DOFade(0, fadeTimer);
        if (gameObject != null)
        {
            Destroy(gameObject, fadeTimer);
        }
    }
}
