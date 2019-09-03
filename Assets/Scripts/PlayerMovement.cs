using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 velocityToApply = new Vector2(0, 0);
    private Vector2 baseMousePosition = new Vector2(0, 0);
    private bool direction = false;
    public bool canJump = true;
    private Rigidbody2D rigidbody2d;
    [SerializeField] GameObject indicator;
    [SerializeField] private TrajectoryPrediction trajectoryPrediction;
    private SpriteRenderer spriteRenderer;

    private HangingPoint currentHangingPoint = null;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
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
        EnablePhysics();
        DisableJump();
    }

    public void EnableJump()
    {
        canJump = true;
    }

    private void DisableJump()
    {
        canJump = false;
    }

    private void EnablePhysics()
    {
        rigidbody2d.bodyType = RigidbodyType2D.Dynamic;
    }

    private void DisablePhysics()
    {
        rigidbody2d.bodyType = RigidbodyType2D.Kinematic;
        rigidbody2d.velocity = Vector3.zero;
    }

    // OnMouseDown is called when the player taps on the gameobject
    private void OnMouseDown()
    {
        if(canJump)
        {
            baseMousePosition = Input.mousePosition;
        }
    }

    // OnMouseDrag is called while the player is dragging across the screen
    private void OnMouseDrag()
    {
        if(canJump)
        {
            velocityToApply.x = (baseMousePosition.x - Input.mousePosition.x) / 5;
            velocityToApply.y = (baseMousePosition.y - Input.mousePosition.y) / 5;

            trajectoryPrediction.UpdateTrajectory(new Vector2(transform.position.x, transform.position.y), velocityToApply, Physics2D.gravity, 20);

            direction = baseMousePosition.x < Input.mousePosition.x;
        }
    }

    // OnMouseUp is called when the player stops holding the screen
    private void OnMouseUp()
    {
        if(canJump)
        {
            if(currentHangingPoint != null)
            {
                currentHangingPoint.TurnOff();
                currentHangingPoint = null;
            }
            rigidbody2d.velocity = Vector2.zero;
            trajectoryPrediction.RemoveIndicators();
            rigidbody2d.AddForce(velocityToApply, ForceMode2D.Impulse);
            DisableJump();
        }
    }
}
