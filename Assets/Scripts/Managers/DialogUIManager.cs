using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private CanvasGroup dialogGroup = null;
    [SerializeField] private TextMeshProUGUI dialogText = null;
    [SerializeField] private TextMeshProUGUI speakerTitle = null;
    [SerializeField] private Image speakerImage = null;

    [Header("Animation")]
    [SerializeField] private float fadeTime = 1f;

    private DialogManager dialogManager;

    private void Start()
    {
        dialogManager = DialogManager.Instance;

        dialogManager.OnStartDialog += DisplayDialog;
        dialogManager.OnEndDialog += HideDialog;

        dialogManager.OnNewDialog += SetDialog;

        dialogGroup.alpha = 0;
    }

    private void DisplayDialog()
    {
        dialogGroup.DOFade(1, fadeTime);
    }

    private void HideDialog()
    {
        dialogGroup.DOFade(0, fadeTime);
    }

    private void SetDialog(Dialog dialog)
    {
        dialogText.text = dialog.Text;

        speakerImage.sprite = dialog.Speaker.Sprite;
        speakerTitle.text = dialog.Speaker.Name;
    }

    public void SetDialogGroup(CanvasGroup _dialogGroup)
    {
        dialogGroup = _dialogGroup;
    }
}
