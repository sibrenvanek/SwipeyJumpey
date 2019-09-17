using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (OtherIsPlayer(other))
        {
            KillPlayerMovement(other);
        }
    }

    private bool OtherIsPlayer(Collider2D other)
    {
        GameObject parent = other.transform.parent.gameObject;
        return parent != null && parent.CompareTag("Player");
    }

    private void KillPlayerMovement(Collider2D other)
    {
        GameObject player = other.transform.parent.gameObject;
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

        playerMovement.Disable();
        playerMovement.KillVelocity();
    }
}
