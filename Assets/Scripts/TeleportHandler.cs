using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportHandler : MonoBehaviour
{
    private PlayerMovement player = null;
    [SerializeField] private Teleporter teleporterOne = null;
    [SerializeField] private Teleporter teleporterTwo = null;
    [SerializeField] private bool reverseY = false;
    [SerializeField] private bool reverseX = false;
    private bool onOne = false;
    private bool onTwo = false;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        teleporterOne.OnTeleporting += delegate { TeleportOne(); };
        teleporterTwo.OnTeleporting += delegate { TeleportTwo(); };
        teleporterOne.OnTriggerExit += delegate { LeftTeleporterOne(); };
        teleporterTwo.OnTriggerExit += delegate { LeftTeleporterTwo(); };
    }

    private void TeleportOne()
    {
        if (onOne || onTwo)
            return;

        onTwo = true;

        Rigidbody2D playerRigidbody = player.GetComponentInParent<Rigidbody2D>();

        if (reverseY)
            playerRigidbody.velocity += new Vector2(0, playerRigidbody.velocity.y * -2);

        if (reverseX)
            playerRigidbody.velocity += new Vector2(0, playerRigidbody.velocity.x * -2);

        player.gameObject.transform.position = teleporterTwo.transform.position;
    }

    private void TeleportTwo()
    {
        if (onOne || onTwo)
            return;

        onOne = true;

        Rigidbody2D playerRigidbody = player.GetComponentInParent<Rigidbody2D>();

        if (reverseY)
            playerRigidbody.velocity += new Vector2(0, playerRigidbody.velocity.y * -2);

        if (reverseX)
            playerRigidbody.velocity += new Vector2(0, playerRigidbody.velocity.x * -2);
        player.gameObject.transform.position = teleporterOne.transform.position;
    }

    private void LeftTeleporterOne()
    {
        onOne = false;
    }

    private void LeftTeleporterTwo()
    {
        onTwo = false;
    }
}
