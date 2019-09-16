using System;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(PlayerDeath))]
public class PlayerManager : MonoBehaviour
{
    /*************
     * VARIABLES *
     *************/

    /**General**/
    private Rigidbody2D rigidbody2d = null;
    private PlayerMovement playerMovement = null;
    private float defaultScale = 0f;
    private bool godMode = false;

    public event Action<bool> OnGodMode = delegate { };

    /*************
     * FUNCTIONS *
     *************/

    /**General**/

    // Awake is called before the first frame update
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        defaultScale = rigidbody2d.gravityScale;
        DontDestroyOnLoad(gameObject);
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

            if (contactPoint2D.rigidbody.CompareTag("DeadZone") && !godMode)
            {
                GetComponent<PlayerDeath>().Die();
                break;
            }

            if (contactPoint2D.otherCollider.name == "Feet" && contactPoint2D.rigidbody.CompareTag("SafeGround"))
                playerMovement.KillVelocity();
        }
    }

    // Handle passing through triggers
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Checkpoint") && playerMovement.IsGrounded())
        {
            other.gameObject.GetComponent<Checkpoint>().Check();
        }
        if (other.gameObject.CompareTag("Finish") && playerMovement.IsGrounded())
        {
            GameManager.Instance.LoadNextLevel();
        }
        if (other.CompareTag("RoomEntrance"))
        {
            other.GetComponent<RoomEntrance>().Enter(transform);
            playerMovement.KillVelocity();
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Grid"))
        {
            Camera.main.GetComponent<CameraManager>().SetConfinerBoundingShape(other.gameObject.GetComponent<Collider2D>());
            other.GetComponent<Room>().OnEnterRoom();
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Grid"))
        {
            Camera.main.GetComponent<CameraManager>().OnExitCollider(other.gameObject.GetComponent<Collider2D>());
        }
    }

    public void SetGodMode(bool godMode)
    {
        this.godMode = godMode;
        OnGodMode.Invoke(godMode);
    }

    public bool GetGodMode()
    {
        return godMode;
    }
}