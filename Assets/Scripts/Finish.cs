using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        WorldManager worldManager = FindObjectOfType<WorldManager>();
        GetComponentInChildren<ParticleSystem>().Play();
        GameManager.Instance.SetLastActivatedCheckpoint(worldManager.GetInitialCheckpoint());
        GameManager.Instance.LoadNextLevel();
    }
}
