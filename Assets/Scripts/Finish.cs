using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        WorldManager worldManager = FindObjectOfType<WorldManager>();
        GetComponentInChildren<ParticleSystem>().Play();
        ProgressionManager.Instance.SetLastActivatedCheckpoint(worldManager.GetInitialCheckpoint());
        LevelManager.Instance.LoadNextScene();
    }
}
