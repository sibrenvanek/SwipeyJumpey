using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    /**Singleton**/
    public static PauseMenu Instance;

    [SerializeField] private static bool isPaused = false;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private TextMeshProUGUI currentLevelPlaceholder;
    [SerializeField] private Room currentRoom;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private WorldManager worldManager;

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
        isPaused = false;
    }

    public void Pause()
    {
        pauseButton.SetActive(false);
        pauseUI.SetActive(true);
        playerMovement.Disable();
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void BackToMenu()
    {
        Destroy(GameManager.Instance.gameObject);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
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
}
