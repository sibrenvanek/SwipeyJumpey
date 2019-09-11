using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    /*************
     * VARIABLES *
     *************/

    /**General**/
    private Rigidbody2D rigidbody2d = null;
    private PlayerMovement playerMovement = null;

    /*************
     * FUNCTIONS *
     *************/

    /**General**/

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rigidbody2d = GetComponent<Rigidbody2D>();
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
        if(other.gameObject.layer == LayerMask.NameToLayer("Grid"))
        {
            GameManager.Instance.SetConfinerBoundingShape(other.gameObject.GetComponent<Collider2D>());
            other.GetComponent<Room>().OnEnterRoom();
        }
    }
}
