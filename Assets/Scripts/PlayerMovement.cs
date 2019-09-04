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
    public bool canJump = true;
    private bool dragging = false;
    [SerializeField] private float speedLimiter = 10;


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
        if (Input.GetMouseButton(0))
        {
            if (!dragging)
            {
                baseMousePosition = Input.mousePosition;
                dragging = true;
            }

            jumpVelocity.x = (baseMousePosition.x - Input.mousePosition.x) / speedLimiter;
            jumpVelocity.y = (baseMousePosition.y - Input.mousePosition.y) / speedLimiter;
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
            rigidbody2d.AddForce(jumpVelocity, ForceMode2D.Impulse);
            dragging = false;
            DisableJump();
        }
    }

    /**Jumping**/

    // Set the canJump value to true
    public void EnableJump()
    {
        canJump = true;
    }

    // Disable the jump and cancel any current plans
    public void DisableJump()
    {
        dragging = false;
        canJump = false;
        trajectoryPrediction.RemoveIndicators();
    }

    // Cancel any current jump plans
    public void CancelJump()
    {
        dragging = false;
        trajectoryPrediction.RemoveIndicators();
    }

    /**Velocity&&Physics**/

    // Enable the physics calculations
    public void EnablePhysics()
    {
        rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
    }

    // Disable the physics calculations
    public void DisablePhysics()
    {
        rigidbody2d.bodyType = RigidbodyType2D.Kinematic;
        KillVelocity();
    }

    // Set the current velocity of the player to zero
    public void KillVelocity()
    {
        rigidbody2d.velocity = Vector3.zero;
    }
}
