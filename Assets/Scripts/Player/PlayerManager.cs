using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerDeath))]
public class PlayerManager : MonoBehaviour
{
    private Rigidbody2D rigidbody2d = null;
    private PlayerMovement playerMovement = null;
    private float defaultScale = 0f;
    private bool godMode = false;

    public event Action<bool> OnGodMode = delegate {};
    public event Action<Collectable> OnCollectCoin = delegate {};
    public event Action<int> OnSideCollectableIncreased = delegate {};

    private List<MinifiedMainCollectable> minifiedMainCollectables = new List<MinifiedMainCollectable>();
    private List<int> sideCollectables = new List<int>();
    private int mainPickups = 0;
    private int sidePickups = 0;
    private int deaths = 0;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        defaultScale = rigidbody2d.gravityScale;
    }

    public MinifiedMainCollectable[] GetMinifiedMainCollectables()
    {
        return minifiedMainCollectables.ToArray();
    }

    public int GetMainPickups()
    {
        return mainPickups;
    }

    public int GetSidePickups()
    {
        return sidePickups;
    }
    
    public List<int> GetSideCollectables()
    {
        return sideCollectables;
    }

    public void SetSidePickups(List<int> ids)
    {
        sideCollectables = ids;
        sidePickups = ids.Count;
        OnSideCollectableIncreased.Invoke(sidePickups);
    }

    public int GetDeaths()
    {
        return deaths;
    }

    public void Collect(Collectable collectable)
    {
        OnCollectCoin(collectable);

        if (collectable is MainCollectable)
        {
            mainPickups++;
            MainCollectable mainCollectable = (MainCollectable)collectable;
            if (mainCollectable.HasBeenCollectedBefore())
                return;

            minifiedMainCollectables.Add(MainCollectable.Minify(mainCollectable));
        }
        else
        {
            sidePickups++;
            sideCollectables.Add(collectable.GetId());
            OnSideCollectableIncreased.Invoke(sidePickups);
        }
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
                deaths++;
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
