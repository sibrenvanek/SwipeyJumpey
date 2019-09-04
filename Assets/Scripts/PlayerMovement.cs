using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 velocityToApply = new Vector2(0, 0);
    private Vector2 baseMousePosition = new Vector2(0, 0);
    private bool direction = false;
    public bool canJump = true;
    private Rigidbody2D rigidbody2d = null;
    [SerializeField] private TrajectoryPrediction trajectoryPrediction = null;
    private SpriteRenderer spriteRenderer;
    private HangingPoint currentHangingPoint = null;
    private bool dragging = false;
    [SerializeField] private float speedLimiter = 10;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canJump)
            return;

        if (Input.GetMouseButton(0))
        {
            if (!dragging)
            {
                baseMousePosition = Input.mousePosition;
                dragging = true;
            }

            velocityToApply.x = (baseMousePosition.x - Input.mousePosition.x) / speedLimiter;
            velocityToApply.y = (baseMousePosition.y - Input.mousePosition.y) / speedLimiter;
            trajectoryPrediction.UpdateTrajectory(new Vector2(transform.position.x, transform.position.y), velocityToApply, Physics2D.gravity, 20);
            direction = baseMousePosition.x < Input.mousePosition.x;
        }

        if (!Input.GetMouseButton(0) && dragging && canJump)
        {
            if (currentHangingPoint != null)
            {
                currentHangingPoint.TurnOff();
                currentHangingPoint = null;
            }
            KillVelocity();
            trajectoryPrediction.RemoveIndicators();
            rigidbody2d.AddForce(velocityToApply, ForceMode2D.Impulse);
            dragging = false;
            DisableJump();
        }

        spriteRenderer.flipX = direction;
    }

    public void StartHang(HangingPoint hangingPoint)
    {
        currentHangingPoint = hangingPoint;
        EnableJump();
        DisablePhysics();
    }

    public void StopHang()
    {
        DisableJump();
        EnablePhysics();
    }

    public void EnableJump()
    {
        canJump = true;
    }

    private void DisableJump()
    {
        dragging = false;
        canJump = false;
        trajectoryPrediction.RemoveIndicators();
    }

    private void EnablePhysics()
    {
        rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
    }

    private void DisablePhysics()
    {
        rigidbody2d.bodyType = RigidbodyType2D.Kinematic;
        KillVelocity();
    }

    public void CancelJump()
    {
        dragging = false;
    }

    public void KillVelocity()
    {
        rigidbody2d.velocity = Vector3.zero;
    }
}
