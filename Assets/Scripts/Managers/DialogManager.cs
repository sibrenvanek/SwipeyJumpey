using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;

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

    public event Action OnStartDialog = delegate { };
    public event Action<Dialog> OnNewDialog = delegate { };
    public event Action OnEndDialog = delegate { };

    private Queue<Dialog> dialogs;

    private void Start()
    {
        dialogs = new Queue<Dialog>();
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

        Debug.Log(dialog.Text);
    }

    private void EndDialog()
    {
        OnEndDialog.Invoke();
    }
}
