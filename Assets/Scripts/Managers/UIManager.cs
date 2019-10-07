using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("SlowMotion effect")]
    [SerializeField] private float slowMotionFadeTime = 0.2f;
    [SerializeField] private CanvasGroup slowMotionGroup = null;
    private GameObject player = null;
    private SlowMotion playerSlowMotion = null;

    private bool isShowing = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        InitializeSlowMotionEffect();
    }

    private void InitializeSlowMotionEffect()
    {
        playerSlowMotion = player.GetComponent<SlowMotion>();

        playerSlowMotion.OnSlowMotionActivated += ShowSlowMotion;
        playerSlowMotion.OnSlowMotionDeActivated += HideSlowMotion;
    }

    private void ShowSlowMotion()
    {
        if(!isShowing)
        {
            slowMotionGroup.DOFade(1, slowMotionFadeTime);
            isShowing = true;
        }
    }

    private void HideSlowMotion()
    {
        if(isShowing)
        {
            slowMotionGroup.alpha = 0;
            isShowing = false;
        }
    }

    public void SetSlowMotionGroup(CanvasGroup _slowMotionGroup)
    {
        slowMotionGroup = _slowMotionGroup;
    }
}
