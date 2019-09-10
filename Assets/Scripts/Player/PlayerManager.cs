using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    /*************
     * VARIABLES *
     *************/

    /**General**/
    private Rigidbody2D rigidbody2d = null;
    private PlayerMovement playerMovement = null;
    private float defaultScale = 0f;

    /*************
     * FUNCTIONS *
     *************/

    /**General**/

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        defaultScale = rigidbody2d.gravityScale;
    }

    /**Physics**/

    // Enable the physics calculations
    public void EnablePhysics()
    {
        rigidbody2d.gravityScale = defaultScale;
    }

    // Disable the physics calculations
    public void DisablePhysics()
    {
        rigidbody2d.gravityScale = 0;
    }

    public void Fall()
    {
        playerMovement.SetSlowMotionJumpAvailable(false);
    }

    /**Collisions**/

    // Handle collisions with other gameobjects
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("DeadZone"))
        {
            playerMovement.CancelJump();
            playerMovement.KillVelocity();
            GameManager.Instance.SendPlayerToLastCheckpoint();
        }
        else if (other.gameObject.CompareTag("SafeGround"))
        {
            playerMovement.KillVelocity();
        }
    }

    // Handle passing through triggers
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            other.gameObject.GetComponent<Checkpoint>().Check();
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("Grid"))
        {
            GameManager.Instance.SetConfinerBoundingShape(other.gameObject.GetComponent<Collider2D>());
            other.GetComponent<Room>().OnEnterRoom();
        }
    }
}
