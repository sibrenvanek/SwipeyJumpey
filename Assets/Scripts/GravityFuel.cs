using UnityEngine;

public class GravityFuel : HangingPoint
{
    /*************
     * VARIABLES *
     *************/

    /**Dragging**/
    [SerializeField] private float dragRange = 0.5f;
    [SerializeField] private float minimalDraggingVelocity = 3f;

    /*************
     * FUNCTIONS *
     *************/

    /**Dragging**/

    // Handle functionality related to dragging the player towards the gameobject
    public override void HandleDragging()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerManager.transform.position);
        Vector2 playerVelocity = playerMovement.GetVelocity();

        if (distanceToPlayer <= detectionRange && !holdingPlayer)
        {
            if (distanceToPlayer < centerRange)
            {
                HoldPlayer();
                StartCoroutine(WaitAndTurnOff());
            }
            else if (distanceToPlayer < dragRange && Mathf.Abs(playerVelocity.x + playerVelocity.y) < minimalDraggingVelocity)
            {
                playerMovement.KillVelocity();
                playerManager.transform.position = Vector2.MoveTowards(playerManager.transform.position, transform.position, dragSpeed * Time.deltaTime);
            }
            else
            {
                DragPlayer();
            }
        }
    }

    // Drag the player towards the object by applying force in the direction
    private void DragPlayer()
    {
        Vector2 force = Vector2.zero;
        force.x = transform.position.x - playerManager.transform.position.x;
        force.y = transform.position.y - playerManager.transform.position.y;
        playerMovement.AddForce(force * 15, ForceMode2D.Force);
    }
}
