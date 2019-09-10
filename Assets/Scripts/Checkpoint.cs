using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private SpriteRenderer spriteRenderer = null;
    [SerializeField] private Sprite active = null;
    [SerializeField] private Sprite inActive = null;
    [SerializeField] private bool isMainCheckpoint = true;

    /*************
     * FUNCTIONS *
     *************/

    private void Awake() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /**Checkpoint**/

    // Set the lastcheckpoint variable in the gamemanager to this checkpoint
    public void Check()
    {
        GameManager.Instance.SetLastCheckpoint(this);
        spriteRenderer.sprite = active;
    }

    public void DeActivate()
    {
        spriteRenderer.sprite = inActive;
    }
}
