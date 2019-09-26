using UnityEngine;
using System;

public class Checkpoint : MonoBehaviour
{
    public event Action OnActivate = delegate { };

    private Animator animator = null;
    private new ParticleSystem particleSystem = null;
    private PlayerMovement playerMovement = null;

    [SerializeField] private Transform checkpointTransform = null;
    public Transform CheckpointTransform { get { return checkpointTransform; } private set { checkpointTransform = value; } }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    public void Check()
    {
        if (GameManager.Instance.LastCheckpoint != this)
        {
            OnActivate.Invoke();
            particleSystem.Play();
            ProgressionManager.Instance.IncreaseAmountOfCheckpointsActivated();
            ProgressionManager.Instance.SetLastActivatedCheckpoint(this);
        }
        GameManager.Instance.SetLastCheckpoint(this);
        animator.SetBool("isCollected", true);

    }

    public void DeActivate()
    {
        animator.SetBool("isCollected", false);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        playerMovement = collider.transform.root.GetComponent<PlayerMovement>();

        Check();
    }
}
