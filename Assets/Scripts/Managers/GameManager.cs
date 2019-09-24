using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Checkpoint lastCheckpoint = null;
    [SerializeField] private PlayerManager player = null;
    [SerializeField] private CanvasManager canvas = null;
    [SerializeField] private PauseMenu pauseMenu = null;
    [SerializeField] private float respawnYOffset = 0.2f;
    [SerializeField] private AudioMixer audioMixer = null;
    public Checkpoint LastCheckpoint { get { return lastCheckpoint; } }
    private AudioSource audioSource = null;
    private float defaultPitch = 1f;
    [SerializeField] private float timeBeforeLoadingScene = 1f;
    private Coroutine displayLoadingScreen;
    private PlayerMovement playerMovement = null;
    [SerializeField] private GameObject PlayerPrefab = null;
    [SerializeField] private GameObject CanvasPrefab = null;
    [SerializeField] private GameObject ProgressionManagerPrefab = null;
    [SerializeField] private bool UseProgression = true;

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

        Input.multiTouchEnabled = false;

        if (ProgressionManager.Instance == null)
        {
            Instantiate(ProgressionManagerPrefab, Vector3.zero, Quaternion.identity);
        }

        if (player == null)
        {
            GameObject playerObject = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
            player = playerObject.GetComponent<PlayerManager>();
        }

        playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.OnJump += ProgressionManager.Instance.IncreaseAmountOfJumps;

        if (canvas == null)
        {
            GameObject canvasObject = Instantiate(CanvasPrefab, Vector3.zero, Quaternion.identity);
            canvas = canvasObject.GetComponent<CanvasManager>();
            canvasObject.GetComponentInChildren<FuelUI>().SetSlowMotion(player.GetComponent<SlowMotion>());
            UIManager uiManager = GameObject.FindObjectOfType<UIManager>();
            CanvasGroup[] canvasGroups = canvasObject.GetComponentsInChildren<CanvasGroup>();
            CanvasGroup slowMotionGroup;
            CanvasGroup dialogGroup;
            if (canvasGroups[0].tag == "SlowMotionGroup")
            {
                slowMotionGroup = canvasGroups[0];
                dialogGroup = canvasGroups[1];
            }
            else
            {
                slowMotionGroup = canvasGroups[1];
                dialogGroup = canvasGroups[0];
            }
            uiManager.SetSlowMotionGroup(slowMotionGroup);
            DialogManager dialogManager = FindObjectOfType<DialogManager>();
            dialogManager.SetButton(dialogGroup.GetComponentInChildren<Button>());
            playerMovement.SetPowerBarUI(canvasObject.GetComponentInChildren<PowerBarUI>());
            DialogUIManager dialogUIManager = FindObjectOfType<DialogUIManager>();
            dialogUIManager.SetDialogGroup(dialogGroup);
        }

        pauseMenu = FindObjectOfType<PauseMenu>();

        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        defaultPitch = audioSource.pitch;
        player = FindObjectOfType<PlayerManager>();
    }

    public void SetLastCheckpoint(Checkpoint checkpoint)
    {
        if (lastCheckpoint != null)
        {
            lastCheckpoint.DeActivate();
        }
        lastCheckpoint = checkpoint;
    }

    public void SendPlayerToLastCheckpoint()
    {
        player.transform.position = new Vector3(lastCheckpoint.CheckpointTransform.position.x, lastCheckpoint.CheckpointTransform.position.y + respawnYOffset);
    }

    public void SendPlayerToCheckpoint(Checkpoint checkpoint)
    {
        player.transform.position = new Vector3(checkpoint.CheckpointTransform.position.x, checkpoint.CheckpointTransform.position.y + respawnYOffset);
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
        int levelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        ProgressionManager.Instance.HandleProgression();

        FindObjectOfType<DarthFader>().FadeGameOut(timeBeforeLoadingScene);
        StartCoroutine(LoadLevelAfterSeconds(levelIndex, timeBeforeLoadingScene));
    }

    private IEnumerator LoadLevelAfterSeconds(int levelIndex, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(levelIndex);
    }

    public void DisableUI()
    {
        canvas.DisableCanvas();
        pauseMenu.DisablePauseMenu();
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        WorldManager worldManager = FindObjectOfType<WorldManager>();
        if (worldManager != null)
        {
            FindObjectOfType<DarthFader>().FadeGameIn(timeBeforeLoadingScene);
            canvas.EnableCanvas();
            pauseMenu.EnablePauseMenu();

            ProgressionManager.Instance.AddLevel(new Level
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
            ProgressionManager.Instance.SaveProgression();
            CinemachineVirtualCamera Vcam = FindObjectOfType<CinemachineVirtualCamera>();
            Vcam.Follow = player.transform;
            Checkpoint checkpoint;
            if (UseProgression)
            {
                Level level = ProgressionManager.Instance.GetLevel(SceneManager.GetActiveScene().name);
                if (level != null)
                {
                    checkpoint = ProgressionManager.GetCheckpointFromMinified(level.latestCheckpoint);
                    if (checkpoint == null)
                    {
                        checkpoint = worldManager.GetInitialCheckpoint();
                    }
                }
                else
                {
                    checkpoint = worldManager.GetInitialCheckpoint();
                }
            }
            else
            {
                checkpoint = worldManager.GetInitialCheckpoint();
            }

            if (checkpoint != worldManager.GetInitialCheckpoint())
            {
                DialogOnStart dialog = FindObjectOfType<DialogOnStart>();
                if (dialog != null)
                    dialog.BlockDialog();
            }
            SendPlayerToCheckpoint(checkpoint);

            FuelUI fuelUI = FindObjectOfType<FuelUI>();
            fuelUI.SetSlowMotion(player.GetComponent<SlowMotion>());

            pauseMenu.SetPlayerMovement(player.GetComponent<PlayerMovement>());
            pauseMenu.SetPlayerManager(player);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        ProgressionManager.Instance.SaveProgression();
    }
}
