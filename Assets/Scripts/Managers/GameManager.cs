using Cinemachine;
using UnityEngine;
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
    [SerializeField] private PlayerManager player = null;
    [SerializeField] private float respawnYOffset = 0.2f;
    [SerializeField] private CinemachineConfiner cinemachineConfiner = null;
    private AudioSource audioSource = null;
    private float defaultPitch = 1f;

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

    public void SetConfinerBoundingShape(Collider2D collider)
    {
        cinemachineConfiner.m_BoundingShape2D = collider;
    }

    public void ReduceAudioPitch(float minus)
    {
        audioSource.pitch = Mathf.Clamp(audioSource.pitch - minus * Time.deltaTime, 0.5f, 1);
    }

    public void ResetAudioPitch()
    {
        audioSource.pitch = defaultPitch;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
