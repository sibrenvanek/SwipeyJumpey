using UnityEngine;
using System.Collections;

public class InstantBooster : Booster
{
    [Header("Instant booster options")]
    [SerializeField] private float respawnTime = 1f;
    [SerializeField] private CirclingEnergy[] circlingEnergies = null;

    protected override void Awake() {
        base.Awake();
        foreach (var energy in circlingEnergies)
        {
            energy.StartCircling();
        }
    }

    protected override void Activate(GameObject player)
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.KillVelocity();
        
        Boost(player.GetComponent<Rigidbody2D>());
        StartCoroutine(DisableAndRespawn());
    }

    private IEnumerator DisableAndRespawn()
    {
        foreach (var energy in circlingEnergies)
        {
            energy.StopCircling();
        }
        Disable();
        yield return new WaitForSeconds(respawnTime);
        Enable();
        
        foreach (var energy in circlingEnergies)
        {
            energy.StartCircling();
        }
    }


}