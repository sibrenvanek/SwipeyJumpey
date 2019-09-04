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
            GameManager.Instance.ResetPlayerToCheckpoint();
        }
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            other.gameObject.GetComponent<Checkpoint>().Check();
            playerMovement.EnableJump();
        }
    }
}