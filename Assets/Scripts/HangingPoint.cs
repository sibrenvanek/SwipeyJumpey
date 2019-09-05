using System.Collections;
using UnityEngine;

public abstract class HangingPoint : MonoBehaviour
{
    /*************
     * VARIABLES *
     *************/

    /**General**/
    [SerializeField] public PlayerMovement playerMovement;
    [SerializeField] public PlayerManager playerManager;
    private SpriteRenderer spriteRenderer;

    /**HangingPoint**/
    [SerializeField] public bool active = true;
    [SerializeField] private float timeBeforeReset = 5f;
    [SerializeField] private float maxHangingTime = 2f;
    [SerializeField] public bool holdingPlayer = false;
    [SerializeField] private int maxResets = 0; //0 is infinite
    private bool isInfinite = false;
    private int resetCounter = 0;

    /**Dragging**/
    [SerializeField] public float detectionRange = 2f;
    [SerializeField] public float centerRange = .2f;
    [SerializeField] public float dragRange = 0.5f;
    [SerializeField] public float dragSpeed = 3f;
    [SerializeField] public float minimalDraggingVelocity = 3f;

    /*************
     * FUNCTIONS *
     *************/

    /**General**/

    // Start is called before the first frame update
    private void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        isInfinite = maxResets > 0 ? false : true;
    }

    // Update is called once per frame
    private void Update()
    {
        HandleDragging();
    }

    /**Dragging**/

    // Handle functionality related to dragging the player towards the gameobject
    public virtual void HandleDragging() { }

    // Hold the player object at the point and freeze its position
    public void HoldPlayer()
    {
        Debug.Log("HEY");
        holdingPlayer = true;

        playerMovement.SetCanJump(true);
        playerManager.DisablePhysics();
        playerMovement.KillVelocity();
        playerMovement.SetHangingPoint(this);
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
    public IEnumerator WaitAndTurnOff()
    {
        float wait = maxHangingTime / 20;
        
        while (spriteRenderer.color.a > 0)
        {
             Color color = spriteRenderer.color;
             color.a -= 0.05f;
             spriteRenderer.color = color;
            yield return new WaitForSeconds(wait);
        }

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
        Color color = spriteRenderer.color;
        color.a = 1;
        spriteRenderer.color = color;
        active = true;
        resetCounter++;
    }
}
