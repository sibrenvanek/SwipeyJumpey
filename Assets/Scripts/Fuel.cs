using UnityEngine;

public class Fuel : HangingPoint
{
    /**Dragging**/

    // Handle functionality related to dragging the player towards the gameobject
    public override void HandleDragging()
    {
        if (!active)
            return;

        float distanceToPlayer = Vector2.Distance(transform.position, playerMovement.transform.position);

        if (distanceToPlayer <= detectionRange && !holdingPlayer)
        {
            if (distanceToPlayer > centerRange)
            {
                DragPlayer();
            }
            else
            {
                HoldPlayer();
                StartCoroutine(WaitAndTurnOff());
            }
        }
    }

    // Drag the player towards the object by applying force in the direction
    private void DragPlayer()
    {
        playerMovement.KillVelocity();
        playerMovement.transform.position = Vector2.MoveTowards(playerMovement.transform.position, transform.position, dragSpeed * Time.deltaTime);
    }
}
