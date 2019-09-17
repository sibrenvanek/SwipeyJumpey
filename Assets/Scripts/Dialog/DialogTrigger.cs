using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private Conversation conversation;

    private DialogManager dialogManager;
    private bool triggered = false;

    private void Start()
    {
        dialogManager = DialogManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (OtherIsPlayer(other))
        {
            if (!triggered)
                StartConversation();
        }
    }

    private bool OtherIsPlayer(Collider2D other)
    {
        GameObject parent = other.transform.parent.gameObject;
        return parent != null && parent.CompareTag("Player");
    }

    private void StartConversation()
    {
        triggered = true;
        dialogManager.StartConversation(conversation);
    }
}
