using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    /*************
     * VARIABLES *
     *************/

    /**General**/
    [SerializeField] private TrajectoryPrediction trajectoryPrediction = null;
    [SerializeField] private SlowMotion slowMotion = null;
    private Rigidbody2D rigidbody2d = null;
    private SpriteRenderer spriteRenderer;
    private Camera mainCamera = null;
    private bool facingLeft = false;
    private bool grounded = false;

    /**Jumping**/
    [SerializeField] private Vector2 maxVelocity = Vector2.zero;
    [SerializeField] private float speedLimiter = 20f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float gravityReduction = 5f;
    [SerializeField] private float maximumCancelDistance = 1f;
    private Vector2 jumpVelocity = new Vector2(0, 0);
    private Vector2 baseMousePosition = new Vector2(0, 0);
    private Vector2 lastMousePosition = new Vector2(0, 0);
    private bool jumpAvailable = true;
    private bool slowMotionJumpAvailable = false;
    private bool dragging = false;
    private float defaultGravity = 0f;

    /*************
     * FUNCTIONS *
     *************/

    /**General**/

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
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

    // Set the value for the jumpAvailable variable
    public void SetJumpAvailable(bool jumpAvailable)
    {
        this.jumpAvailable = jumpAvailable;
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
        if (Input.GetMouseButton(0) && (jumpAvailable || slowMotionJumpAvailable))
        {
            if (!dragging)
            {
                baseMousePosition = Input.mousePosition;
                dragging = true;
                if (!grounded)
                  slowMotion.Go();
            }

            if (speedLimiter <= 0)
                speedLimiter = 1;

            lastMousePosition = Input.mousePosition;
            jumpVelocity.x = Mathf.Clamp((baseMousePosition.x - Input.mousePosition.x) / speedLimiter, -maxVelocity.x, maxVelocity.x);
            jumpVelocity.y = Mathf.Clamp((baseMousePosition.y - Input.mousePosition.y) / speedLimiter, -maxVelocity.y, maxVelocity.y);

            trajectoryPrediction.UpdateTrajectory(new Vector2(transform.position.x, transform.position.y), jumpVelocity, Physics2D.gravity * rigidbody2d.gravityScale, dashTime);
            facingLeft = baseMousePosition.x < Input.mousePosition.x;
        }
    }

    // Handle the player releasing their finger from the screen
    private void HandleRelease()
    {
        if (Input.GetMouseButton(0) || !dragging)
            return;

        Vector3 mousePoint = mainCamera.ScreenToWorldPoint(lastMousePosition);
        mousePoint.z = transform.position.z;

        if (Vector2.Distance(transform.position, mousePoint) < maximumCancelDistance)
        {
            CancelJump();
            return;
        }

        trajectoryPrediction.RemoveIndicators();

        if (jumpAvailable)
        {
            jumpAvailable = false;
            dragging = false;
            grounded = false;
            Jump();
        }
        else if(slowMotionJumpAvailable)
        {
            slowMotionJumpAvailable = false;
            slowMotion.Cancel();
            Jump();
        }

        dragging = false;
    }

    //Set the value of the slowMotionJumpAvailable variable
    public void SetSlowMotionJumpAvailable(bool slowMotionJumpAvailable)
    {
        this.slowMotionJumpAvailable = slowMotionJumpAvailable;
    }

    // Set the value of the grounded variable
    public void SetGrounded(bool grounded)
    {
        this.grounded = grounded;
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
        StartCoroutine(RemoveGravity());
        rigidbody2d.AddForce(jumpVelocity, ForceMode2D.Impulse);
    }

    // Temporarily remove gravity
    private IEnumerator RemoveGravity()
    {
        rigidbody2d.gravityScale = 0;
        yield return new WaitForSeconds(dashTime);
        rigidbody2d.gravityScale = defaultGravity;
    }

    /**Velocity**/

    // Set the current velocity of the player to zero
    public void KillVelocity()
    {
        rigidbody2d.velocity = Vector3.zero;
    }

    /**Collisions**/

    // OnTriggerEnter is called when a collision with a trigger occurs
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fuel"))
        {
            slowMotionJumpAvailable = true;
            collision.GetComponent<Fuel>().PickUp(rigidbody2d);
            Destroy(collision.gameObject);
        }
    }
}
