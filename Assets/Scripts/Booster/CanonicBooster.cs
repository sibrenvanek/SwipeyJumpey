using UnityEngine;

public class CanonicBooster : Booster
{
    [Header("Canonic booster options")]
    [SerializeField] private float dragSpeed = 2f;
    private bool holdingPlayer = false;
    private PlayerMovement playerMovement = null;
    private PlayerManager playerManager = null;
    
    private void Update() {
        if(holdingPlayer && Input.GetMouseButton(0))
        {
            holdingPlayer = false;
            Boost(playerMovement.GetComponent<Rigidbody2D>());

            EnablePlayerMovement();
        }
    }

    protected override void FixedUpdate() {
        base.FixedUpdate();

        if(holdingPlayer)
        {
            playerMovement.transform.position = Vector2.Lerp(playerMovement.transform.position, transform.position, dragSpeed * Time.fixedDeltaTime);
        }
    }

    protected override void Activate(GameObject player)
    {
        if(playerMovement == null)
            playerMovement = player.GetComponent<PlayerMovement>();

        if(playerManager == null)
            playerManager = player.GetComponent<PlayerManager>();

        DisablePlayerMovement();

        holdingPlayer = true;
    }

    private void DisablePlayerMovement()
    {
        playerMovement.KillVelocity();
        playerMovement.Disable();
        playerMovement.RemoveGravity();
    }

    private void EnablePlayerMovement()
    {
        playerMovement.RestoreGravity();
        playerMovement.Enable();
    }

}