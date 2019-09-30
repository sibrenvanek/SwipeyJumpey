using UnityEngine;
using DG.Tweening;

public class CanonicBooster : Booster
{
    [Header("Canonic booster options")]
    [SerializeField] private float dragSpeed = 2f;
    [SerializeField] private SpriteRenderer aimingArrow = null;
    private bool holdingPlayer = false;
    public bool HoldingPlayer { get { return holdingPlayer; } private set { holdingPlayer = value; } }
    private PlayerMovement playerMovement = null;
    private PlayerManager playerManager = null;
    private CanonicBoosterAnimation canonicBoosterAnimation = null;

    protected override void Awake() 
    {
        base.Awake();

        canonicBoosterAnimation = GetComponent<CanonicBoosterAnimation>();    
    }

    private void Update()
    {
        if (holdingPlayer && Input.GetMouseButton(0))
        {
            holdingPlayer = false;
            Boost(playerMovement.GetComponent<Rigidbody2D>(), aimingArrow.transform.parent);

            EnablePlayerMovement();

            canonicBoosterAnimation.Launch();
            canonicBoosterAnimation.ToggleCharging();
            Disable();
        }
    }

    private void FixedUpdate()
    {
        if (holdingPlayer)
        {
            playerMovement.transform.position = Vector2.Lerp(playerMovement.transform.position, transform.position, dragSpeed * Time.fixedDeltaTime);
        }
    }

    protected override void Enable()
    {
        base.Enable();
        aimingArrow.DOKill();
        aimingArrow.DOFade(1, .3f);
    }

    protected override void Disable()
    {
        base.Disable();
        aimingArrow.DOKill();
        aimingArrow.DOFade(0, 0);
    }

    protected override void Activate(GameObject player)
    {
        if (playerMovement == null)
            playerMovement = player.GetComponent<PlayerMovement>();

        if (playerManager == null)
            playerManager = player.GetComponent<PlayerManager>();

        DisablePlayerMovement();

        holdingPlayer = true;
        canonicBoosterAnimation.ToggleCharging();
    }

    private void DisablePlayerMovement()
    {
        playerMovement.KillVelocity();
        playerMovement.Disable();
        playerMovement.RemoveGravity();
    }

    private void EnablePlayerMovement()
    {
        playerMovement.RestoreGravity();
        playerMovement.Enable();
    }

}