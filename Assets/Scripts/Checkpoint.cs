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
            GameManager.IncreaseAmountOfCheckpointsActivated();
            GameManager.SetLastActivatedCheckpoint(this);
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
        if (GameManager.Instance.LastCheckpoint != this)
        {
            particleSystem.Play();
        }
        GameManager.Instance.SetLastCheckpoint(this);
        animator.SetBool("isCollected", true);
    }
}
