using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TouchTutorialHand : MonoBehaviour
{
    private bool active = true;
    private PlayerMovement playerMovement;

    private void Start()
    {
        if (ProgressionManager.Instance.GetDisplayedTutorial())
        {
            Destroy(gameObject);
        }
        else
        {
            playerMovement = FindObjectOfType<PlayerMovement>();
            playerMovement.OnHop += TurnOff;
        }
    }

    private void TurnOff()
    {
        if (active)
        {
            StartCoroutine(FadeAndDeactivate());
        }
    }

    private IEnumerator FadeAndDeactivate()
    {
        GetComponent<SpriteRenderer>().DOFade(0, 2f);
        active = false;
        yield return new WaitForSeconds(2f);
        playerMovement.OnHop -= TurnOff;
        Destroy(gameObject);
    }
}
