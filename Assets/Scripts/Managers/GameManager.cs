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
    [SerializeField] private PlayerManager player = null;
    [SerializeField] private float respawnYOffset = 0.2f;
    [SerializeField] private AudioMixer audioMixer = null;
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


    public void ReduceAudioPitch(float minus)
    {
        float pitchChange = minus * Time.deltaTime;

        if (audioSource.pitch - pitchChange < 0.5f)
            return;

        float audioMixerPitch = audioMixer.GetFloat("mixerPitch", out audioMixerPitch) ? audioMixerPitch : 0f;

        audioMixer.SetFloat("mixerPitch", audioMixerPitch + pitchChange);
        audioSource.pitch -= pitchChange;
    }

    public void ResetAudioPitch()
    {
        audioMixer.SetFloat("mixerPitch", 1f);
        audioSource.pitch = defaultPitch;
    }
}
