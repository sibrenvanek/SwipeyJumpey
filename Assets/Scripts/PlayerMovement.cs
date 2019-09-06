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
    private bool facingLeft = false;

    /**Jumping**/
    private Vector2 jumpVelocity = new Vector2(0, 0);
    private Vector2 baseMousePosition = new Vector2(0, 0);
    private bool jumpAvailable = true;
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

    // Set the value for the freeJump variable
    public void SetFreeJump(bool freeJump)
    {
        this.jumpAvailable = freeJump;
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
        if (!Input.GetMouseButton(0) && dragging && jumpAvailable)
        {
            jumpAvailable = false;
            KillVelocity();
            trajectoryPrediction.RemoveIndicators();
            speedBoost = 0;
            dragging = false;
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
