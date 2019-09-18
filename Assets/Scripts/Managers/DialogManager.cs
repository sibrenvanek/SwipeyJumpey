using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private Button button = null;

    public static DialogManager Instance;

    public event Action OnStartDialog = delegate {};
    public event Action<Dialog> OnNewDialog = delegate {};
    public event Action OnEndDialog = delegate {};

    private Queue<Dialog> dialogs;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        dialogs = new Queue<Dialog>();

        button.onClick.AddListener(DisplayNextDialog);
    }

    public void StartConversation(Conversation conversation)
    {
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
            return;
        }

        Dialog dialog = dialogs.Dequeue();
        OnNewDialog.Invoke(dialog);
    }

    private void EndDialog()
    {
        OnEndDialog.Invoke();
    }

    public void SetButton(Button _button)
    {
        button = _button;
    }
}
