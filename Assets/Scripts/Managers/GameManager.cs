using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Checkpoint lastCheckpoint = null;
    [SerializeField] private PlayerManager player = null;
    [SerializeField] private float respawnYOffset = 0.2f;
    [SerializeField] private AudioMixer audioMixer = null;
    [SerializeField] private int LoadSceneDuration = 0;
    [SerializeField] private GameObject PlayerPrefab = null;
    public Checkpoint LastCheckpoint { get { return lastCheckpoint; } }
    private AudioSource audioSource = null;
    private float defaultPitch = 1f;

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
        int levelIndex = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public IEnumerator ShowLoadingScreenBeforeNextLevel(int levelIndex)
    {
        SceneManager.LoadScene("LoadScene");
        yield return new WaitForSeconds(LoadSceneDuration);
        SceneManager.LoadScene(levelIndex);
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        WorldManager worldManager = FindObjectOfType<WorldManager>();
        if (worldManager != null)
        {
            CinemachineVirtualCamera Vcam = FindObjectOfType<CinemachineVirtualCamera>();
            Vcam.Follow = player.transform;

            SendPlayerToCheckpoint(worldManager.GetInitialCheckpoint());

            FuelUI fuelUI = FindObjectOfType<FuelUI>();
            fuelUI.SetSlowMotion(player.GetComponent<SlowMotion>());

            PauseMenu pauseMenu = FindObjectOfType<PauseMenu>();
            pauseMenu.SetPlayerMovement(player.GetComponent<PlayerMovement>());
            pauseMenu.SetPlayerManager(player);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }
}
