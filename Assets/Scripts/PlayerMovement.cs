using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 velocityToApply = new Vector2(0, 0);
    private Vector2 baseMousePosition = new Vector2(0, 0);
    private bool direction = false;
    private Rigidbody2D rigidbody2d;
    [SerializeField] GameObject indicator;
    private TrajectoryPrediction trajectoryPrediction;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        trajectoryPrediction = new TrajectoryPrediction(indicator);
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.flipX = direction;
    }

    // OnMouseDown is called when the player taps on the gameobject
    private void OnMouseDown()
    {
        baseMousePosition = Input.mousePosition;
    }

    // OnMouseDrag is called while the player is dragging across the screen
    private void OnMouseDrag()
    {
        velocityToApply.x = (baseMousePosition.x - Input.mousePosition.x) / 5;
        velocityToApply.y = (baseMousePosition.y - Input.mousePosition.y) / 5;

        trajectoryPrediction.UpdateTrajectory(new Vector2(transform.position.x, transform.position.y), velocityToApply, Physics2D.gravity, 20);

        direction = baseMousePosition.x < Input.mousePosition.x;
    }

    // OnMouseUp is called when the player stops holding the screen
    private void OnMouseUp()
    {
        rigidbody2d.velocity = Vector2.zero;
        trajectoryPrediction.RemoveIndicators();
        rigidbody2d.AddForce(velocityToApply, ForceMode2D.Impulse);
    }
}
