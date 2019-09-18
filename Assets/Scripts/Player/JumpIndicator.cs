using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class JumpIndicator : MonoBehaviour
{

    [SerializeField] private Animator jetpackAnimator;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        SetCanJump();

        playerMovement.OnCanJump += SetCanJump;
        playerMovement.OnJump += SetNoJump;
    }

    private void SetNoJump()
    {
        jetpackAnimator.SetBool("isEmpty", true);
    }

    private void SetCanJump()
    {
        jetpackAnimator.SetBool("isEmpty", false);
    }
}
