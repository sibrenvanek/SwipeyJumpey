using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator animator = null;
    private new ParticleSystem particleSystem = null;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    public void Check()
    {
        if (GameManager.Instance.LastCheckpoint != this)
        {
            particleSystem.Play();
            GameManager.Instance.IncreaseAmountOfCheckpointsActivated();
            GameManager.Instance.SetLastActivatedCheckpoint(this);
        }
        GameManager.Instance.SetLastCheckpoint(this);
        animator.SetBool("isCollected", true);

    }

    public void DeActivate()
    {
        animator.SetBool("isCollected", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();

        if (!playerMovement)
            return;

        if (!playerMovement.IsGrounded())
            return;

        if (GameManager.Instance.LastCheckpoint != this)
        {
            particleSystem.Play();
        }
        GameManager.Instance.SetLastCheckpoint(this);
        animator.SetBool("isCollected", true);
    }
}
