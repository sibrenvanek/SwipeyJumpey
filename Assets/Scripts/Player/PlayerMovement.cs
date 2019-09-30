using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float maxVelocity = 0f;
    [SerializeField] private float speedLimiter = 20f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float maximumCancelDistance = 1f;
    [SerializeField] private float timeDiff = 1f;

    [SerializeField] private TrajectoryPrediction trajectoryPrediction = null;
    [SerializeField] private SlowMotion slowMotion = null;
    [SerializeField] private Jetpack jetpack = null;
    [SerializeField] private Vector2 hopVelocity = Vector2.zero;
    public event Action OnJump = delegate {};
    public event Action OnCanJump = delegate {};
    private Rigidbody2D rigidbody2d = null;
    private SpriteRenderer spriteRenderer = null;
    private bool facingLeft = false;
    private bool grounded = false;
    private PlayerManager playerManager = null;
    private bool notifiedJump = true;
    private Vector2 jumpVelocity = new Vector2(0, 0);
    private Vector2 baseMousePosition = new Vector2(0, 0);
    private Vector2 lastMousePosition = new Vector2(0, 0);
    Vector2 maxVelocityVector = Vector2.zero;
    public bool slowMotionJumpAvailable { get; private set; } = false;
    public bool slowMotionActivated = false;
    private bool dragging = false;
    private bool inputEnabled = true;
    private float defaultGravityScale = 0f;
    private bool gravityTemporarilyOff = false;
    private bool gravityOff = false;
    private SpriteRenderer jetpackSpriteRenderer = null;
    private bool hopping = false;
    private bool hopAvailable = false;

    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        jetpack = GetComponentInChildren<Jetpack>();
        jetpackSpriteRenderer = jetpack.GetComponent<SpriteRenderer>();
        defaultGravityScale = rigidbody2d.gravityScale;
    }

    void Update()
    {
        if (inputEnabled)
            HandleInput();

        SetDirection();
        grounded = IsGrounded();

        if (CheckCancelSlowmotionJump())
        {
            CancelSlowmotionJump();
        }

        CheckIfCanJump();
    }

    private void CheckIfCanJump()
    {
        if ((slowMotionJumpAvailable || grounded) && !notifiedJump)
        {
            OnCanJump.Invoke();
            notifiedJump = true;
        }
    }

    private void SetDirection()
    {
        spriteRenderer.flipX = facingLeft;
        jetpackSpriteRenderer.flipX = facingLeft;
    }

    public void Disable()
    {
        inputEnabled = false;
    }

    public void Enable()
    {
        inputEnabled = true;
    }

    private void HandleInput()
    {
        if (Input.GetMouseButton(0))
            HandleDrag();
        else
            HandleRelease();
    }

    private void HandleDrag()
    {
        if (Input.GetMouseButton(0))
        {
            if (!dragging)
            {
                StartCoroutine(TapCoroutine());
                baseMousePosition = Input.mousePosition;
                dragging = true;
            }

            if ((grounded || slowMotionJumpAvailable) && !jetpack.EngineCharging)
            {
                jetpack.Charge();
            }

            lastMousePosition = Input.mousePosition;

            if (ShouldJumpCancel())
            {
                return;
            }
            else if (hopping)
            {
                StopCoroutine(TapCoroutine());
                hopping = false;
            }

            if (!slowMotionActivated && !grounded && slowMotionJumpAvailable)
            {
                slowMotion.Go();
                slowMotionActivated = true;
            }

            if (speedLimiter <= 0)
                speedLimiter = 1;

            jumpVelocity.x = (baseMousePosition.x - Input.mousePosition.x) / speedLimiter;
            jumpVelocity.y = (baseMousePosition.y - Input.mousePosition.y) / speedLimiter;

            float angle = CalculateAngle(jumpVelocity);
            maxVelocityVector = CalculateMaxVelocity(maxVelocity, angle);
            Vector2 oldJumpVelocity = new Vector2(Mathf.Clamp(jumpVelocity.x, -maxVelocity, maxVelocity), Mathf.Clamp(jumpVelocity.y, -maxVelocity, maxVelocity));
            if (angle < 90 && angle > -90)
            {
                jumpVelocity.x = Mathf.Clamp((baseMousePosition.x - Input.mousePosition.x) / speedLimiter, -maxVelocityVector.x, maxVelocityVector.x);
                jumpVelocity.y = Mathf.Clamp((baseMousePosition.y - Input.mousePosition.y) / speedLimiter, -maxVelocityVector.y, maxVelocityVector.y);
            }
            else
            {
                float maxXVelocity = Mathf.Abs(maxVelocityVector.x);
                jumpVelocity.x = Mathf.Clamp((baseMousePosition.x - Input.mousePosition.x) / speedLimiter, -maxXVelocity, maxXVelocity);
                jumpVelocity.y = Mathf.Clamp((baseMousePosition.y - Input.mousePosition.y) / speedLimiter, -maxVelocityVector.y, maxVelocityVector.y);
            }
            if (oldJumpVelocity.magnitude != 0 || Double.IsInfinity(oldJumpVelocity.magnitude))
            {
                timeDiff = jumpVelocity.magnitude / oldJumpVelocity.magnitude;
            }
            else
            {
                timeDiff = 1;
            }
            if (timeDiff > 1)
            {
                timeDiff = 1;
            }
            if (grounded || slowMotionJumpAvailable)
            {
                trajectoryPrediction.UpdateTrajectory(Camera.main.ScreenToWorldPoint(baseMousePosition), jumpVelocity, angle, dashTime, maximumCancelDistance, maxVelocityVector);
                facingLeft = baseMousePosition.x < Input.mousePosition.x;
            }
        }
    }

    private IEnumerator TapCoroutine()
    {
        hopping = true;
        yield return new WaitForSecondsRealtime(0.1f);
        hopping = false;
    }

    private bool ShouldJumpCancel()
    {
        Vector3 firstMousePoint = Camera.main.ScreenToWorldPoint(baseMousePosition);
        firstMousePoint.z = transform.position.z;
        Vector3 lastMousePoint = Camera.main.ScreenToWorldPoint(lastMousePosition);
        lastMousePoint.z = transform.position.z;
        return Vector2.Distance(firstMousePoint, lastMousePoint) < maximumCancelDistance;
    }

    private void HandleRelease()
    {
        if (Input.GetMouseButton(0) || !dragging)
            return;

        Vector3 firstMousePoint = Camera.main.ScreenToWorldPoint(baseMousePosition);
        firstMousePoint.z = transform.position.z;
        Vector3 lastMousePoint = Camera.main.ScreenToWorldPoint(lastMousePosition);
        lastMousePoint.z = transform.position.z;

        if (ShouldJumpCancel())
        {
            if (hopping && IsGrounded())
            {
                jumpVelocity = new Vector2(((lastMousePoint.x < transform.position.x) ? -1 : 1) * hopVelocity.x, hopVelocity.y);
                Hop();
                return;
            }
            else
            {
                CancelJump();
                return;
            }
        }

        trajectoryPrediction.RemoveIndicators();

        if (slowMotionJumpAvailable)
        {
            slowMotionJumpAvailable = false;
            slowMotion.Cancel();
            Jump();
        }
        else if (grounded || playerManager.GetGodMode())
        {
            Jump();
        }

        slowMotionActivated = false;
        dragging = false;
    }

    public void SetSlowMotionJumpAvailable(bool slowMotionJumpAvailable)
    {
        this.slowMotionJumpAvailable = slowMotionJumpAvailable;
    }

    public void CancelJump()
    {
        if (slowMotionJumpAvailable && !grounded)
        {
            slowMotionJumpAvailable = false;
            slowMotion.Cancel();
        }

        jetpack.TurnOff();
        dragging = false;
        trajectoryPrediction.RemoveIndicators();
    }

    private void Hop()
    {
        grounded = false;
        KillVelocity();
        StartCoroutine(RemoveGravityTemporarily());
        rigidbody2d.AddForce(jumpVelocity, ForceMode2D.Impulse);
        dragging = false;
    }

    private void Jump()
    {
        grounded = false;
        jetpack.Launch(jumpVelocity, maxVelocityVector);
        notifiedJump = false;
        OnJump.Invoke();
        KillVelocity();
        dragging = false;
        trajectoryPrediction.RemoveIndicators();
        StartCoroutine(RemoveGravityTemporarily());
        rigidbody2d.AddForce(jumpVelocity, ForceMode2D.Impulse);
    }

    private IEnumerator RemoveGravityTemporarily()
    {
        gravityTemporarilyOff = true;
        rigidbody2d.gravityScale = 0;
        yield return new WaitForSeconds(dashTime * timeDiff);
        timeDiff = 1f;
        gravityTemporarilyOff = false;
        jetpack.TurnOff();
        if (!gravityOff)
            rigidbody2d.gravityScale = defaultGravityScale;
    }

    public void RemoveGravity()
    {
        gravityOff = true;
        rigidbody2d.gravityScale = 0;
    }

    public void RestoreGravity()
    {
        gravityOff = false;
        if (!gravityTemporarilyOff)
            rigidbody2d.gravityScale = defaultGravityScale;
    }

    public void KillVelocity()
    {
        if (rigidbody2d != null)
        {
            rigidbody2d.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fuel") && !slowMotionJumpAvailable)
        {
            slowMotionJumpAvailable = true;
            collision.GetComponent<Fuel>().PickUp(rigidbody2d);
        }
    }

    public bool IsGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(transform.position, new Vector2(transform.localScale.x * 0.7f, 0.1f), 0, Vector2.down, 1f + transform.localScale.y * 0.5f, LayerMask.GetMask("SafeGround"));

        if (!raycastHit2d)
            return false;

        return (raycastHit2d.collider.gameObject.CompareTag("SafeGround") && rigidbody2d.velocity.y <= 0);
    }

    bool CheckCancelSlowmotionJump()
    {
        return IsGrounded() && slowMotion.doingSlowmotion;
    }

    void CancelSlowmotionJump()
    {
        slowMotion.Cancel();
        SetSlowMotionJumpAvailable(true);
    }

    Vector2 CalculateMaxVelocity(float maxVelocity, float angle)
    {
        float x, y;
        float angleRadiant = Mathf.Abs(angle * Mathf.Deg2Rad);
        x = Mathf.Cos(angleRadiant) * maxVelocity;
        y = Mathf.Sin(angleRadiant) * maxVelocity;

        return new Vector2(x, y);
    }

    float CalculateAngle(Vector2 velocity)
    {
        float angle = Mathf.Rad2Deg * Mathf.Atan(velocity.y / velocity.x);

        if (velocity.x < 0 && velocity.y < 0)
            angle -= 180;
        else if (velocity.x < 0 && velocity.y >= 0)
            angle += 180;

        return angle;
    }
}
