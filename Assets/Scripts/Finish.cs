using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        WorldManager worldManager = FindObjectOfType<WorldManager>();
        GameManager.SetLastActivatedCheckpoint(worldManager.GetInitialCheckpoint());
        GameManager.Instance.LoadNextLevel();
    }
}
