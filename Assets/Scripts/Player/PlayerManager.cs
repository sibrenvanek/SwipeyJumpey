using System;
using UnityEngine;

[RequireComponent(typeof(PlayerDeath))]
public class PlayerManager : MonoBehaviour
{
    private Rigidbody2D rigidbody2d = null;
    private PlayerMovement playerMovement = null;
    private Jetpack jetpack = null;
    private float defaultScale = 0f;
    private bool godMode = false;

    public event Action<bool> OnGodMode = delegate { };

    /**Singleton**/
    public static PlayerManager Instance;

    /*************
     * FUNCTIONS *
     *************/

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        jetpack = GetComponentInChildren<Jetpack>();
        playerMovement = GetComponent<PlayerMovement>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        defaultScale = rigidbody2d.gravityScale;
    }

    public void EnablePhysics()
    {
        rigidbody2d.gravityScale = defaultScale;
    }

    public void DisablePhysics()
    {
        rigidbody2d.gravityScale = 0;
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
                playerMovement.CancelJump();
                playerMovement.KillVelocity();
                GameManager.Instance.IncreaseAmountOfDeaths();
                GetComponent<PlayerDeath>().Die();
                break;
            }

            if (contactPoint2D.otherCollider.name == "Feet" && contactPoint2D.rigidbody.CompareTag("SafeGround"))
            {
                playerMovement.KillVelocity();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Checkpoint") && playerMovement.IsGrounded())
        {
            other.gameObject.GetComponent<Checkpoint>().Check();
        }

        if (other.CompareTag("RoomEntrance"))
        {
            playerMovement.KillVelocity();
            other.GetComponent<RoomEntrance>().Enter(transform);
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
}
