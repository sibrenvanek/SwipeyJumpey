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
        spriteRenderer = GetComponent<SpriteRenderer>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    /**Checkpoint**/

    // Set the lastcheckpoint variable in the gamemanager to this checkpoint
    public void Check()
    {
        if(GameManager.Instance.LastCheckpoint != this)
        {
            particleSystem.Play();
        }
        GameManager.Instance.SetLastCheckpoint(this);
        spriteRenderer.sprite = active;

    }

    public void DeActivate()
    {
        spriteRenderer.sprite = inActive;
    }
}
