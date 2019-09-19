using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogOnStart : MonoBehaviour
{
    [SerializeField] private Conversation conversation = null;

    private DialogManager dialogManager;

    private void Start()
    {
        StartCoroutine(StartConversation());
    }

    private IEnumerator StartConversation()
    {
        yield return new WaitForSeconds(0.25f);

        dialogManager = DialogManager.Instance;
        dialogManager.StartConversation(conversation);
    }
}
