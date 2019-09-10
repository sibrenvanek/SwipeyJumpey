using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    /*************
     * VARIABLES *
     *************/

    /**General**/
    public event Action OnSlowMotionActivated = delegate {};
    public event Action OnSlowMotionDeActivated = delegate {};
    [SerializeField] private TrajectoryPrediction trajectoryPrediction = null;
    [SerializeField] private SlowMotion slowMotion = null;
    private Rigidbody2D rigidbody2d = null;
    private SpriteRenderer spriteRenderer = null;
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
    public bool slowMotionJumpAvailable { get; private set; } = false;
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
        grounded = IsGrounded();
    }

    // Set the direction the object is facing
    private void SetDirection()
    {
        spriteRenderer.flipX = facingLeft;
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
        if (Input.GetMouseButton(0) && (grounded || slowMotionJumpAvailable))
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

        Vector3 firstMousePoint = mainCamera.ScreenToWorldPoint(baseMousePosition);
        firstMousePoint.z = transform.position.z;
        Vector3 lastMousePoint = mainCamera.ScreenToWorldPoint(lastMousePosition);
        lastMousePoint.z = transform.position.z;

        if (Vector2.Distance(firstMousePoint, lastMousePoint) < maximumCancelDistance)
        {
            CancelJump();
            return;
        }

        trajectoryPrediction.RemoveIndicators();

        if (grounded)
        {
            grounded = false;
            Jump();
        }
        else if (slowMotionJumpAvailable)
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

    /**Jumping**/

    // Cancel any current jump plans
    public void CancelJump()
    {
        if (slowMotionJumpAvailable && !grounded)
        {
            slowMotionJumpAvailable = false;
            slowMotion.Cancel();
        }

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
        }
    }

    // Check if the player is on the ground
    bool IsGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.Raycast(transform.position, Vector2.down, 1f + transform.localScale.y * 0.5f, LayerMask.GetMask("SafeGround"));

        if (!raycastHit2d)
            return false;

        return (raycastHit2d.collider.gameObject.CompareTag("SafeGround") && rigidbody2d.velocity.y <= 0);
    }
}
