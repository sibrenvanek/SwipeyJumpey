using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    /*************
     * VARIABLES *
     *************/

    /**General**/
    [SerializeField] private TrajectoryPrediction trajectoryPrediction = null;
    [SerializeField] private SlowMotion slowMotion;
    private Rigidbody2D rigidbody2d = null;
    private SpriteRenderer spriteRenderer;
    private HangingPoint hangingPoint = null;
    private bool facingLeft = false;

    /**Jumping**/
    private Vector2 jumpVelocity = new Vector2(0, 0);
    private Vector2 baseMousePosition = new Vector2(0, 0);
    private bool canJump = true;
    private bool dragging = false;
    [SerializeField] private float speedLimiter = 20f;
    private float speedBoost = 0f;
    private float defaultGravity = 0f;
    [SerializeField] private float dashSpeedReduction = 1.5f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float gravityReduction = 5f;

    /*************
     * FUNCTIONS *
     *************/

    /**General**/

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultGravity = rigidbody2d.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        SetDirection();
    }

    // Set the direction the object is facing
    private void SetDirection()
    {
        spriteRenderer.flipX = facingLeft;
    }

    // Set the value for the hanging point variable
    public void SetHangingPoint(HangingPoint hangingPoint)
    {
        this.hangingPoint = hangingPoint;
    }

    // Set the value for the canJump variable
    public void SetCanJump(bool canJump)
    {
        this.canJump = canJump;
    }

    // Get the velocity of the player object
    public Vector2 GetVelocity()
    {
        return rigidbody2d.velocity;
    }

    // Add force to the player object
    public void AddForce(Vector2 force, ForceMode2D forceMode2d)
    {
        rigidbody2d.AddForce(force, forceMode2d);
    }

    // Set the value for the speedboost variable
    public void SetSpeedBoost(float speedBoost)
    {
        this.speedBoost = speedBoost;
    }

    /**Player Input**/

    // Handle playerinput
    private void HandleInput()
    {
        if (Input.GetMouseButton(0))
            HandleDrag();
        else
            HandleRelease();
    }

    // Handle the player dragging on the screen
    private void HandleDrag()
    {
        if (Input.GetMouseButton(0) && canJump)
        {
            if (!dragging)
            {
                baseMousePosition = Input.mousePosition;
                dragging = true;
                slowMotion.Go();
            }

            float limiter = speedLimiter - speedBoost;

            if (limiter <= 0)
                limiter = 1;

            jumpVelocity.x = (baseMousePosition.x - Input.mousePosition.x) / limiter;
            jumpVelocity.y = (baseMousePosition.y - Input.mousePosition.y) / limiter;
            trajectoryPrediction.UpdateTrajectory(new Vector2(transform.position.x, transform.position.y), jumpVelocity, Physics2D.gravity * rigidbody2d.gravityScale, 20);
            facingLeft = baseMousePosition.x < Input.mousePosition.x;
        }
    }

    // Handle the player releasing their finger from the screen
    private void HandleRelease()
    {
        if (!Input.GetMouseButton(0) && dragging && canJump)
        {
            if (hangingPoint != null)
            {
                hangingPoint.TurnOff();
                hangingPoint = null;
            }
            slowMotion.cancel();
            trajectoryPrediction.RemoveIndicators();
            speedBoost = 0;
            dragging = false;
            canJump = false;
            Jump();
        }
    }

    /**Jumping**/

    // Cancel any current jump plans
    public void CancelJump()
    {
        dragging = false;
        trajectoryPrediction.RemoveIndicators();
    }

    // Make the player character jump
    private void Jump()
    {
        KillVelocity();
        StartCoroutine(ReduceGravity());
        rigidbody2d.AddForce(jumpVelocity / dashSpeedReduction, ForceMode2D.Impulse);
    }

    // Temporarily reduce the gravityscale
    private IEnumerator ReduceGravity()
    {
        rigidbody2d.gravityScale = defaultGravity / gravityReduction;
        yield return new WaitForSeconds(dashTime);
        rigidbody2d.gravityScale = defaultGravity;
    }

    /**Velocity**/

    // Set the current velocity of the player to zero
    public void KillVelocity()
    {
        rigidbody2d.velocity = Vector3.zero;
    }
}
