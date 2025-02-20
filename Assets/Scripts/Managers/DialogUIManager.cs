using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogUIManager : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private float fadeTime = 1f;

    private CanvasGroup dialogGroup = null;
    private TextMeshProUGUI dialogText = null;
    private TextMeshProUGUI speakerTitle = null;
    private Image speakerImage = null;

    private DialogManager dialogManager;
    private PauseMenu pauseMenu;

    private void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
        dialogManager = FindObjectOfType<DialogManager>();

        InitUIElements();

        dialogManager.OnStartDialog += DisplayDialog;
        dialogManager.OnEndDialog += HideDialog;

        dialogManager.OnNewDialog += SetDialog;

        dialogGroup.alpha = 0;
    }

    private void InitUIElements()
    {
        dialogGroup = GameObject.FindGameObjectWithTag("DialogGroup").GetComponent<CanvasGroup>();
        dialogText = GameObject.FindGameObjectWithTag("DialogText").GetComponent<TextMeshProUGUI>();
        speakerTitle = GameObject.FindGameObjectWithTag("SpeakerTitle").GetComponent<TextMeshProUGUI>();
        speakerImage = GameObject.FindGameObjectWithTag("SpeakerImage").GetComponent<Image>();
    }

    private void DisplayDialog()
    {
        dialogGroup.DOFade(1, fadeTime);
        pauseMenu.DisablePauseMenu();
    }

    private void HideDialog()
    {
        dialogGroup.DOFade(0, fadeTime);
        pauseMenu.EnablePauseMenu();
    }

    private void SetDialog(Dialog dialog)
    {
        dialogText.text = dialog.Text;

        speakerImage.sprite = dialog.Speaker.Sprite;
        speakerTitle.text = dialog.Speaker.Name;
    }

    public void SetDialogGroup(CanvasGroup dialogGroup)
    {
        this.dialogGroup = dialogGroup;
    }
}
