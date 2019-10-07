using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField]private FinishScreen finishScreen = null;
    private bool finished = false;
    private PlayerMovement playerMovement = null;
    private PlayerManager playerManager = null;
    private Level level = null;

    [Header("Only set finish rocket if it is a finish rocket!")]
    [SerializeField]private FinishRocket finishRocket = null;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            if (!finished)
            {
                playerMovement = collider.GetComponent<PlayerMovement>();
                playerManager = collider.GetComponent<PlayerManager>();
                FinishLevel();
            }
        }
    }

    private void FinishLevel()
    {
        WorldManager worldManager = FindObjectOfType<WorldManager>();
        ProgressionManager.Instance.SetLastActivatedCheckpoint(worldManager.GetInitialCheckpoint());

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
        FindObjectOfType<CollectionStreakManager>().EndLevel();
        finishScreen.SetLevelName(level.levelName);
        finishScreen.SetMainCollectables(playerManager.GetMainPickups(), level.totalAmountOfMainCollectables);
        finishScreen.SetSideCollectables(playerManager.GetSidePickups(), level.totalAmountOfSideCollectables);
        finishScreen.SetDeathCounter(playerManager.GetDeaths());
        finishScreen.gameObject.SetActive(true);
    }
}
