using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField]private FinishScreen finishScreen = null;
    private bool finished = false; 
    private Level level = null;

    [Header("Only set finish rocket if it is a finish rocket!")]
    [SerializeField]private FinishRocket finishRocket = null;
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
        level = ProgressionManager.Instance.GetLevel(SceneManager.GetActiveScene().name);
        level.completed = true;
        
        if(finishRocket != null)
        {
            finishRocket.Fly(OpenFinishScreen);
        }
        else
        {
            OpenFinishScreen();
        }
        
    }

    private void OpenFinishScreen()
    {
        finishScreen.SetLevelName(level.levelName);
        finishScreen.SetMainCollectables(level.amountOfMainCollectables + "/" + level.totalAmountOfMainCollectables);
        finishScreen.SetSideCollectables(level.amountOfSideCollectables + "/" + level.totalAmountOfSideCollectables);
        finishScreen.SetDeathCounter(level.amountOfDeaths + "/" + level.amountOfDeaths);
        finishScreen.gameObject.SetActive(true);
    }
}
