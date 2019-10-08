using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class ConversationWaiter : MonoBehaviour
{
    private DialogManager dialogManager;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        dialogManager = DialogManager.FindObjectOfType<DialogManager>();

        dialogManager.OnStartDialog += KillPlayerMovement;
        dialogManager.OnEndDialog += RevivePlayerMovement;
    }

    private void KillPlayerMovement()
    {
        playerMovement.CancelJump();
        playerMovement.KillVelocity();
        playerMovement.Disable();
    }

    private void RevivePlayerMovement()
    {
        playerMovement.Enable();
    }
}
