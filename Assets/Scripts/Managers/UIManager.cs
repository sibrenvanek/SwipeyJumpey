﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [Header("SlowMotion effect")]
    [SerializeField] private float slowMotionFadeTime = 0.2f;
    [SerializeField] private CanvasGroup slowMotionGroup = null;

    private GameObject player = null;
    private SlowMotion playerSlowMotion = null;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    private void Start()
    {
        InitliazeSlowMotionEffect();
    }

    private void InitliazeSlowMotionEffect()
    {
        playerSlowMotion = player.GetComponent<SlowMotion>();

        playerSlowMotion.OnSlowMotionActivated += ShowSlowMotion;
        playerSlowMotion.OnSlowMotionDeActivated += HideSlowMotion;
    }

    private void ShowSlowMotion()
    {
        slowMotionGroup.DOFade(1, slowMotionFadeTime);
    }

    private void HideSlowMotion()
    {
        slowMotionGroup.DOFade(0, slowMotionFadeTime);
    }

}