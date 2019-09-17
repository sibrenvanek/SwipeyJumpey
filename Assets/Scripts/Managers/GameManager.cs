using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /*************
     * VARIABLES *
     *************/

    /**Singleton**/
    public static GameManager Instance;

    /**General**/
    [SerializeField] private Checkpoint lastCheckpoint = null;
    public Checkpoint LastCheckpoint { get { return lastCheckpoint; } }

    [SerializeField] private PlayerManager player = null;
    [SerializeField] private CanvasManager canvas = null;
    [SerializeField] private PauseMenu pauseMenu = null;
    [SerializeField] private float respawnYOffset = 0.2f;
    [SerializeField] private AudioMixer audioMixer = null;
    [SerializeField] private int LoadSceneDuration = 0;
    private AudioSource audioSource = null;
    private float defaultPitch = 1f;
    private Coroutine displayLoadingScreen;
    private PlayerMovement playerMovement = null;
    private Progression progression;
    [SerializeField] private GameObject PlayerPrefab = null;
    [SerializeField] private GameObject CanvasPrefab = null;

    /*************
     * FUNCTIONS *
     *************/

    /**General**/

    // Awake is called before other functionality
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (player == null)
        {
            GameObject playerObject = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
            player = playerObject.GetComponent<PlayerManager>();
        }

        playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.OnJump += IncreaseAmountOfJumps;

        if (canvas == null)
        {
            GameObject canvasObject = Instantiate(CanvasPrefab, Vector3.zero, Quaternion.identity);
            canvas = canvasObject.GetComponent<CanvasManager>();
            canvasObject.GetComponentInChildren<FuelUI>().SetSlowMotion(player.GetComponent<SlowMotion>());
        }

        pauseMenu = FindObjectOfType<PauseMenu>();

        progression = Progression.LoadProgression();

        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        defaultPitch = audioSource.pitch;
        player = FindObjectOfType<PlayerManager>();
    }

    /**Checkpoint**/

    // Set the value for the lastCheckpoint variable
    public void SetLastCheckpoint(Checkpoint checkpoint)
    {
        if (lastCheckpoint != null)
        {
            lastCheckpoint.DeActivate();
        }
        lastCheckpoint = checkpoint;
    }

    // Set the player position equal to the last checkpoint
    public void SendPlayerToLastCheckpoint()
    {
        player.transform.position = new Vector3(lastCheckpoint.transform.position.x, lastCheckpoint.transform.position.y + respawnYOffset);
    }

    public void SendPlayerToCheckpoint(Checkpoint checkpoint)
    {
        player.transform.position = new Vector3(checkpoint.transform.position.x, checkpoint.transform.position.y + respawnYOffset);
    }

    public void ResetWorld()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReduceAudioPitch(float minus)
    {
        float pitchChange = minus * Time.deltaTime;

        float audioMixerPitch = audioMixer.GetFloat("mixerPitch", out audioMixerPitch) ? audioMixerPitch : 0f;

        audioMixer.SetFloat("mixerPitch", audioMixerPitch + pitchChange);
        audioSource.pitch -= pitchChange;
    }

    public void ResetAudioPitch()
    {
        audioMixer.SetFloat("mixerPitch", 1f);
        audioSource.pitch = defaultPitch;
    }

    public void LoadNextLevel()
    {
        progression.MarkLevelAsCompleted(SceneManager.GetActiveScene().name);
        progression.SaveProgression();
        int levelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        canvas.DisableCanvas();
        pauseMenu.DisablePauseMenu();
        displayLoadingScreen = StartCoroutine(ShowLoadingScreenBeforeNextLevel(levelIndex));
    }

    public IEnumerator ShowLoadingScreenBeforeNextLevel(int levelIndex)
    {
        SceneManager.LoadScene("Intro");
        yield return new WaitForSeconds(LoadSceneDuration);
        SceneManager.LoadScene(levelIndex);
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        WorldManager worldManager = FindObjectOfType<WorldManager>();
        if (worldManager != null)
        {
            canvas.EnableCanvas();
            pauseMenu.EnablePauseMenu();
            progression.AddLevel(new Level
            {
                completed = false,
                    worldName = worldManager.GetWorldName(),
                    sceneName = scene.name,
                    latestCheckpoint = new MinifiedCheckpoint
                    {
                        name = worldManager.GetInitialCheckpoint().name,
                            position = worldManager.GetInitialCheckpoint().transform.position
                    }
            });
            progression.SaveProgression();
            CinemachineVirtualCamera Vcam = FindObjectOfType<CinemachineVirtualCamera>();
            Vcam.Follow = player.transform;

            SendPlayerToCheckpoint(worldManager.GetInitialCheckpoint());

            FuelUI fuelUI = FindObjectOfType<FuelUI>();
            fuelUI.SetSlowMotion(player.GetComponent<SlowMotion>());

            pauseMenu.SetPlayerMovement(player.GetComponent<PlayerMovement>());
            pauseMenu.SetPlayerManager(player);
        }
    }

    void IncreaseAmountOfJumps()
    {
        progression.IncreaseAmountOfJumps();
    }

    public static void IncreaseAmountOfDeaths()
    {
        Instance.progression.IncreaseAmountOfDeaths();
    }

    public static void IncreaseAmountOfFuelsGrabbed()
    {
        Instance.progression.IncreaseAmountOfFuelsGrabbed();
    }

    public static void IncreaseAmountOfCheckpointsActivated()
    {
        Instance.progression.IncreaseAmountCheckpointsActivated();
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        playerMovement.OnJump -= IncreaseAmountOfJumps;
        progression.SaveProgression();
    }
}
