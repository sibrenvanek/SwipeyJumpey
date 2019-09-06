using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /*************
     * VARIABLES *
     *************/

    /**General**/
    [SerializeField] private TrajectoryPrediction trajectoryPrediction = null;
    private Rigidbody2D rigidbody2d = null;
    private SpriteRenderer spriteRenderer;
    private HangingPoint hangingPoint = null;
    private bool facingLeft = false;

    /**Jumping**/
    private Vector2 jumpVelocity = new Vector2(0, 0);
    private Vector2 baseMousePosition = new Vector2(0, 0);
    private bool canJump = true;
    private bool dragging = false;
    [SerializeField] private float speedLimiter = 20;
    private float speedBoost = 0f;

    /*************
     * FUNCTIONS *
     *************/

    /**General**/

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

    // reset the value of hangingpoint
    public void RemoveHangingPoint()
    {
        hangingPoint = null;
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
            }

            float limiter = speedLimiter - speedBoost;

            if (limiter <= 0)
                limiter = 1;

            jumpVelocity.x = (baseMousePosition.x - Input.mousePosition.x) / limiter;
            jumpVelocity.y = (baseMousePosition.y - Input.mousePosition.y) / limiter;
            trajectoryPrediction.UpdateTrajectory(new Vector2(transform.position.x, transform.position.y), jumpVelocity, Physics2D.gravity, 20);
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
            KillVelocity();
            trajectoryPrediction.RemoveIndicators();
            speedBoost = 0;
            dragging = false;
            canJump = false;
            rigidbody2d.AddForce(jumpVelocity, ForceMode2D.Impulse);
        }
    }

    /**Jumping**/

    // Cancel any current jump plans
    public void CancelJump()
    {
        dragging = false;
        trajectoryPrediction.RemoveIndicators();
    }

    /**Velocity**/

    // Set the current velocity of the player to zero
    public void KillVelocity()
    {
        rigidbody2d.velocity = Vector3.zero;
    }
}
