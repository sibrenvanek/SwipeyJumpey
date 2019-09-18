using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private SpriteRenderer spriteRenderer = null;
    [SerializeField] private Sprite active = null;
    [SerializeField] private Sprite inActive = null;
    private new ParticleSystem particleSystem = null;

    /*************
     * FUNCTIONS *
     *************/

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    // Set the lastcheckpoint variable in the gamemanager to this checkpoint
    public void Check()
    {
        if (GameManager.Instance.LastCheckpoint != this)
        {
            particleSystem.Play();
            GameManager.IncreaseAmountOfCheckpointsActivated();
            GameManager.SetLastActivatedCheckpoint(this);
        }
        GameManager.Instance.SetLastCheckpoint(this);
        spriteRenderer.sprite = active;

    }

    public void DeActivate()
    {
        spriteRenderer.sprite = inActive;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.Instance.LastCheckpoint != this)
        {
            particleSystem.Play();
        }
        GameManager.Instance.SetLastCheckpoint(this);
        spriteRenderer.sprite = active;
    }
}
