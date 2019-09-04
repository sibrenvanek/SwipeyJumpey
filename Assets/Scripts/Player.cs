using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerMovement playerMovement = null;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("DeadZone"))
        {
            playerMovement.CancelJump();
            playerMovement.KillVelocity();
            GameManager.Instance.ResetPlayerToCheckpoint();
        }
        else if (other.gameObject.CompareTag("SafeGround"))
        {
            playerMovement.EnableJump();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            other.gameObject.GetComponent<Checkpoint>().Check();
        }
    }
}
