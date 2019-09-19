using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogOnStart : MonoBehaviour
{
    [SerializeField] private Conversation conversation = null;

    private DialogManager dialogManager;

    private void Start()
    {
        dialogManager = DialogManager.Instance;
        dialogManager.StartConversation(conversation);
    }
}
