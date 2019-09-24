using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogOnStart : MonoBehaviour
{
    [SerializeField] private Conversation conversation = null;

    private DialogManager dialogManager;
    private bool dialogBlocked = false;

    private void Start()
    {
        StartCoroutine(StartConversation());
    }

    private IEnumerator StartConversation()
    {
        yield return new WaitForSeconds(0.25f);
        if (!dialogBlocked)
        {
            dialogManager = DialogManager.Instance;
            dialogManager.StartConversation(conversation);
        }
    }

    public void BlockDialog()
    {
        dialogBlocked = true;
    }
}
