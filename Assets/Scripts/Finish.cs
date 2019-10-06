using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField]private FinishScreen finishScreen = null;
    private bool finished = false; 
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            if(!finished)
                FinishLevel();
        }
    }

    private void FinishLevel()
    {
        WorldManager worldManager = FindObjectOfType<WorldManager>();
        ProgressionManager.Instance.SetLastActivatedCheckpoint(worldManager.GetInitialCheckpoint());
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        playerMovement.Disable();
        GetComponentInChildren<ParticleSystem>().Play();
        Level level = ProgressionManager.Instance.GetLevel(SceneManager.GetActiveScene().name);
        level.completed = true;
        finishScreen.SetMainCollectables(level.amountOfMainCollectables + "/" + level.totalAmountOfMainCollectables);
        finishScreen.SetSideCollectables(level.amountOfSideCollectables + "/" + level.totalAmountOfSideCollectables);
        finishScreen.gameObject.SetActive(true);
    }
}
