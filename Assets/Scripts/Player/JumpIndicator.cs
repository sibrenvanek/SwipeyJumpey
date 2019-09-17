using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class JumpIndicator : MonoBehaviour
{
    [SerializeField] private Sprite noJumpSprite = null;
    private SpriteRenderer spriteRenderer;
    private Sprite defaultSprite;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultSprite = spriteRenderer.sprite;
    }

    private void Start()
    {
        SetCanJump();

        playerMovement.OnCanJump += SetCanJump;
        playerMovement.OnJump += SetNoJump;
    }

    private void SetNoJump()
    {
        spriteRenderer.sprite = noJumpSprite;
    }

    private void SetCanJump()
    {
        spriteRenderer.sprite = defaultSprite;
    }
}
