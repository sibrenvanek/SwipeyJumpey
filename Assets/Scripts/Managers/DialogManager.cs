using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    private Button button;

    public event Action OnStartDialog = delegate { };
    public event Action<Dialog> OnNewDialog = delegate { };
    public event Action OnEndDialog = delegate { };
    private CanvasManager canvasManager = null;
    private Queue<Dialog> dialogs;

    private void Awake()
    {
        dialogs = new Queue<Dialog>();
    }

    private void Start()
    {
        canvasManager = FindObjectOfType<CanvasManager>();
        InitNextButton();
    }

    private void InitNextButton()
    {
        button = GameObject.FindGameObjectWithTag("NextDialogButton").GetComponent<Button>();
        button.onClick.AddListener(DisplayNextDialog);
    }

    public void StartConversation(Conversation conversation)
    {
        if (conversation.IsTutorial() && ProgressionManager.Instance.GetDisplayedTutorial())
            return;

        canvasManager.EnableNextButton();
        OnStartDialog.Invoke();
        dialogs.Clear();

        foreach (Dialog dialog in conversation.Dialogs)
        {
            dialogs.Enqueue(dialog);
        }

        DisplayNextDialog();
    }

    public void DisplayNextDialog()
    {
        if (dialogs.Count == 0)
        {
            EndDialog();
            canvasManager.DisableNextButton();
            return;
        }

        Dialog dialog = dialogs.Dequeue();
        OnNewDialog.Invoke(dialog);
    }

    private void EndDialog()
    {
        OnEndDialog.Invoke();
    }

    public void SetButton(Button button)
    {
        this.button = button;
    }
}
