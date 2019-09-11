using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainMenuHead : MonoBehaviour
{
    [Header("Rotate animation")]
    [SerializeField] private float rotateTime = 1f;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        Sequence animation = DOTween.Sequence();
        animation.Append(rectTransform.DOLocalRotate(new Vector3(0, 180, 0), rotateTime));
        animation.Append(rectTransform.DOLocalRotate(new Vector3(0, 0, 0), rotateTime));

        animation.SetLoops(-1);
        animation.Play();
    }
}
