using UnityEngine;
using Cinemachine;

public class PlayerManager : MonoBehaviour
{
    /*************
     * VARIABLES *
     *************/

    /**General**/
    private Rigidbody2D rigidbody2d = null;
    private PlayerMovement playerMovement = null;
    private float defaultScale = 0f;
    private CameraManager cameraManager = null;
    /*************
     * FUNCTIONS *
     *************/

    /**General**/

    // Awake is called before the first frame update
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        cameraManager = Camera.main.GetComponent<CameraManager>();
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
        ContactPoint2D[] contactPoints = new ContactPoint2D[3];
        other.GetContacts(contactPoints);

        foreach (ContactPoint2D contactPoint2D in contactPoints)
        {
            if (!contactPoint2D.rigidbody)
                break;

            if (contactPoint2D.rigidbody.CompareTag("DeadZone"))
            {
                playerMovement.CancelJump();
                playerMovement.KillVelocity();
                GameManager.Instance.SendPlayerToLastCheckpoint();
                break;
            }

            if (contactPoint2D.otherCollider.name == "Feet" && contactPoint2D.rigidbody.CompareTag("SafeGround"))
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
        if (other.gameObject.layer == LayerMask.NameToLayer("Grid"))
        {
            cameraManager.SetConfinerBoundingShape(other.gameObject.GetComponent<Collider2D>());
            other.GetComponent<Room>().OnEnterRoom();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Grid"))
        {
            cameraManager.OnExitCollider(other.gameObject.GetComponent<Collider2D>());
        }
    }
}
