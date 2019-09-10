using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /*************
     * VARIABLES *
     *************/

    /**Singleton**/
    public static GameManager Instance;

    /**General**/
    [SerializeField] private Checkpoint initialCheckpointWorld = null;
    [SerializeField] private Checkpoint initialCheckpointLevel = null;
    [SerializeField] private Checkpoint lastCheckpoint = null;
    [SerializeField] private PlayerManager player = null;
    [SerializeField] private float respawnYOffset = 0.2f;
    [SerializeField] private CinemachineConfiner cinemachineConfiner = null;

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
    public void ResetPlayerToCheckpoint(PickCheckpoint pickCheckpoint = PickCheckpoint.lastCheckpoint)
    {
        Vector3 newPosition = new Vector3();
        if (pickCheckpoint == PickCheckpoint.lastCheckpoint)
        {
            newPosition = new Vector3(lastCheckpoint.transform.position.x, lastCheckpoint.transform.position.y + respawnYOffset);
        }
        else if (pickCheckpoint == PickCheckpoint.initialCheckpointWorld)
        {
            newPosition = new Vector3(initialCheckpointWorld.transform.position.x, initialCheckpointWorld.transform.position.y + respawnYOffset);
        }
        else if (pickCheckpoint == PickCheckpoint.initialCheckpointLevel)
        {
            newPosition = new Vector3(initialCheckpointLevel.transform.position.x, initialCheckpointLevel.transform.position.y + respawnYOffset);
        }
        player.transform.position = newPosition;
    }

    public void SetConfinerBoundingShape(Collider2D collider)
    {
        cinemachineConfiner.m_BoundingShape2D = collider;
    }
}

public enum PickCheckpoint
{
    lastCheckpoint,
    initialCheckpointWorld,
    initialCheckpointLevel
}
