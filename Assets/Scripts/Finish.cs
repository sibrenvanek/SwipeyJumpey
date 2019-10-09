using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField] private FinishScreen finishScreen = null;
    private bool finished = false;
    private PlayerMovement playerMovement = null;
    private PlayerManager playerManager = null;
    private Level level = null;
    private CanvasGroup coinUI = null;

    private void Start()
    {
        coinUI = FindObjectOfType<CoinUI>().GetComponent<CanvasGroup>();
    }

    [Header("Only set finish rocket if it is a finish rocket!")]
    [SerializeField] private FinishRocket finishRocket = null;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (!finished)
            {
                playerMovement = collider.GetComponentInParent<PlayerMovement>();
                playerManager = collider.GetComponentInParent<PlayerManager>();
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
        coinUI.alpha = 0;
        UpdateProgression();

        if (finishRocket != null)
        {
            finishRocket.Fly(OpenFinishScreen);
        }
        else
        {
            OpenFinishScreen();
        }

    }

    private void UpdateProgression()
    {
        ProgressionManager.Instance.IncreaseAmountOfMainCollectables(playerManager.GetMinifiedMainCollectables());
        ProgressionManager.Instance.IncreaseAmountOfSideCollectables(playerManager.GetSideCollectables());
        ProgressionManager.Instance.IncreaseAmountOfDeaths(playerManager.GetDeaths());
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
