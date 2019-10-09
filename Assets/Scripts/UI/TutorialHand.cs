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
    private JetpackPickup jetpackPickup;

    private void Start()
    {
        if (ProgressionManager.Instance.GetDisplayedTutorial())
        {
            Destroy(gameObject);
        }
        else
        {

            playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            spriteRenderer = GetComponent<SpriteRenderer>();

            jetpackPickup = FindObjectOfType<JetpackPickup>();

            if (jetpackPickup == null)
            {
                Destroy(gameObject, fadeTimer);
            }
            else if (jetpackPickup.gameObject.activeInHierarchy)
            {
                spriteRenderer.DOFade(0, 0);
                jetpackPickup.OnJetpackPickup += Display;
                playerMovement.OnJump += StopShowing;
            }
            else
            {
                Destroy(gameObject, fadeTimer);
            }
        }
    }

    private void Display()
    {
        spriteRenderer.DOFade(1, 0);
    }

    private void StopShowing()
    {
        jetpackPickup.OnJetpackPickup -= Display;
        playerMovement.OnJump -= StopShowing;
        ProgressionManager.Instance.SetDisplayedTutorial();

        spriteRenderer.DOFade(0, fadeTimer);

        if (gameObject != null)
        {
            Destroy(gameObject, fadeTimer);
        }
    }
}
