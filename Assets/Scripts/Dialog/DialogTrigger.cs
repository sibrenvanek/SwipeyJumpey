using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private Conversation conversation;

    private DialogManager dialogManager;

    private void Start()
    {
        dialogManager = DialogManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (OtherIsPlayer(other))
        {
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
        dialogManager.StartConversation(conversation);
    }
}
