using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private Conversation conversation = null;

    private DialogManager dialogManager;
    private bool triggered = false;

    private void Start()
    {
        dialogManager = FindObjectOfType<DialogManager>();
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
        if (other.transform.parent != null)
        {
            GameObject parent = other.transform.parent.gameObject;
            return parent != null && parent.CompareTag("Player");
        }
        else
        {
            return false;
        }
    }

    private void StartConversation()
    {
        triggered = true;
        dialogManager.StartConversation(conversation);
    }
}
