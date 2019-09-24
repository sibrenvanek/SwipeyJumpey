using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;
    [SerializeField] private GameObject pauseUI = null;
    [SerializeField] private GameObject pauseButton = null;
    [SerializeField] private TextMeshProUGUI currentLevelPlaceholder = null;
    [SerializeField] private Room currentRoom = null;
    [SerializeField] private PlayerMovement playerMovement = null;
    [SerializeField] private PlayerManager playerManager = null;
    [SerializeField] private WorldManager worldManager = null;

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

    public void Start()
    {
        currentLevelPlaceholder.text = currentRoom.RoomName;
        worldManager.OnCurrentRoomChanged += ChangeLevelText;
    }

    public void Resume()
    {
        pauseButton.SetActive(true);
        pauseUI.SetActive(false);
        playerMovement.Enable();
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        pauseButton.SetActive(false);
        pauseUI.SetActive(true);
        playerMovement.CancelJump();
        playerMovement.Disable();
        Time.timeScale = 0f;
    }

    public void BackToMenu()
    {
        Destroy(PlayerManager.Instance.gameObject);
        Destroy(CanvasManager.Instance.gameObject);
        Destroy(GameManager.Instance.gameObject);
        Time.timeScale = 1f;
        SceneManager.LoadScene(3);
        Destroy(Instance.gameObject);
    }

    public void ChangeLevelText(Room newRoom)
    {
        currentRoom = newRoom;
        currentLevelPlaceholder.text = currentRoom.RoomName;
    }

    public void ResetLevel()
    {
        Resume();
        worldManager.ResetToRoomCheckpoint();
    }

    public void ResetWorld()
    {
        Resume();
        worldManager.ResetToInitialCheckpoint();
    }

    public void ResetCheckpoint()
    {
        Resume();
        GameManager.Instance.SendPlayerToLastCheckpoint();
    }

    public void ToggleGodmode()
    {
        playerManager.SetGodMode(!playerManager.GetGodMode());
    }

    public void SetPlayerMovement(PlayerMovement newPlayerMovement)
    {
        playerMovement = newPlayerMovement;
    }

    public void SetPlayerManager(PlayerManager newPlayerManager)
    {
        playerManager = newPlayerManager;
    }

    public void DisablePauseMenu()
    {
        gameObject.SetActive(false);
    }

    public void EnablePauseMenu()
    {
        gameObject.SetActive(true);
    }
}
