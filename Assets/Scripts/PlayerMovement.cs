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
    private bool facingLeft = false;
    private bool grounded = false;

    /**Jumping**/
    [SerializeField] private Vector2 maxVelocity = Vector2.zero;
    private Vector2 jumpVelocity = new Vector2(0, 0);
    private Vector2 baseMousePosition = new Vector2(0, 0);
    private bool jumpAvailable = true;
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
        if (Input.GetMouseButton(0) && jumpAvailable)
        {
            if (!dragging)
            {
                baseMousePosition = Input.mousePosition;
                dragging = true;
                if (!grounded)
                  slowMotion.Go();
            }

            float limiter = speedLimiter - speedBoost;

            if (limiter <= 0)
                limiter = 1;

            jumpVelocity.x = Mathf.Clamp((baseMousePosition.x - Input.mousePosition.x) / limiter, -maxVelocity.x, maxVelocity.x);
            jumpVelocity.y = Mathf.Clamp((baseMousePosition.y - Input.mousePosition.y) / limiter, -maxVelocity.y, maxVelocity.y);
            //trajectoryPrediction.UpdateTrajectory(new Vector2(transform.position.x, transform.position.y), jumpVelocity, Physics2D.gravity * rigidbody2d.gravityScale, 20);
            facingLeft = baseMousePosition.x < Input.mousePosition.x;
        }
    }

    // Handle the player releasing their finger from the screen
    private void HandleRelease()
    {
        if (!Input.GetMouseButton(0) && dragging && jumpAvailable)
        {
            jumpAvailable = false;
            KillVelocity();
            //trajectoryPrediction.RemoveIndicators();
            speedBoost = 0;
            dragging = false;
            slowMotion.Cancel();
            rigidbody2d.AddForce(jumpVelocity, ForceMode2D.Impulse);
            Jump();
            grounded = false;
        }
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

    /**Collisions**/

    // OnTriggerEnter is called when a collision with a trigger occurs
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fuel"))
        {
            jumpAvailable = true;
            Destroy(collision.gameObject);
        }
    }
}
