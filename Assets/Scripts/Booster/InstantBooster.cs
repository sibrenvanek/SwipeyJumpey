using UnityEngine;
using System.Collections;

public class InstantBooster : Booster
{
    [Header("Instant booster options")]
    [SerializeField] private float respawnTime = 1f;

    protected override void Activate(GameObject player)
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.KillVelocity();
        
        Boost(player.GetComponent<Rigidbody2D>());
        StartCoroutine(DisableAndRespawn());
    }

    private IEnumerator DisableAndRespawn()
    {
        Disable();
        yield return new WaitForSeconds(respawnTime);
        Enable();
    }
}