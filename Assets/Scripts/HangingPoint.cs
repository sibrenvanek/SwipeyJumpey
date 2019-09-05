using System.Collections;
using UnityEngine;

/*********
 * ENUMS *
 *********/

public enum HangingPointType
{
    Fuel,
    ACouldHaveHangingPointType
}

public class HangingPoint : MonoBehaviour
{
    /*************
     * VARIABLES *
     *************/

    /**General**/

    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerManager playerManager;

    /**HangingPoint**/

    [SerializeField] private bool active = true;
    [SerializeField] private float timeBeforeReset = 5f;
    [SerializeField] private float maxHangingTime = 2f;
    [SerializeField] private bool holdingPlayer = false;
    [SerializeField] private HangingPointType hangingPointType;
    [SerializeField] private int maxResets = 0; //0 is infinite
    private bool isInfinite = false;
    private int resetCounter = 0;

    /**Dragging**/

    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float centerRange = .2f;
    [SerializeField] private float dragRange = 1f;
    [SerializeField] private float dragSpeed = 3f;
    [SerializeField] private float minimalDraggingVelocity = 3f;

    /*************
     * FUNCTIONS *
     *************/

    /**General**/

    // Start is called before the first frame update
    private void Start()
    {
        isInfinite = maxResets > 0 ? false : true;
    }

    // Update is called once per frame
    private void Update()
    {
        HandleDragging();
    }

    /**Dragging**/

    // Handle functionality related to dragging the player towards the gameobject
    private void HandleDragging()
    {
        if (!active)
            return;

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
                Debug.Log(playerVelocity.x + playerVelocity.y);
                playerMovement.KillVelocity();
                playerManager.transform.position = Vector2.MoveTowards(playerManager.transform.position, transform.position, dragSpeed * Time.deltaTime);
            }
            else
            {
                DragPlayer();
            }
        }
    }

    // Hold the player object at the point and freeze its position
    private void HoldPlayer()
    {
        holdingPlayer = true;

        playerMovement.SetCanJump(true);
        playerManager.DisablePhysics();
        playerMovement.KillVelocity();
        playerMovement.SetHangingPoint(this);
    }

    // Drag the player towards the object by applying force in the direction
    private void DragPlayer()
    {
        Vector2 force = Vector2.zero;
        force.x = transform.position.x - playerManager.transform.position.x;
        force.y = transform.position.y - playerManager.transform.position.y;
        playerMovement.AddForce(force * 15, ForceMode2D.Force);
    }

    /**HangingPoint**/

    // Turn the hangingpoint off
    public void TurnOff()
    {
        playerMovement.SetCanJump(false);
        playerMovement.CancelJump();
        playerManager.EnablePhysics();

        active = false;
        holdingPlayer = false;

        if (isInfinite || resetCounter < maxResets)
        {
            StartCoroutine(WaitAndResetPoint());
        }
    }

    // Wait for the maximum allowed hangingtime and then turn the point off
    private IEnumerator WaitAndTurnOff()
    {
        yield return new WaitForSeconds(maxHangingTime);

        if (active)
        {
            TurnOff();
        }
    }

    // Wait and reset the point
    private IEnumerator WaitAndResetPoint()
    {
        yield return new WaitForSeconds(timeBeforeReset);
        ResetPoint();
    }

    // Set the point active and reset the counter
    private void ResetPoint()
    {
        active = true;
        resetCounter++;
    }
}
