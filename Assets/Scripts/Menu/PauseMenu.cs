using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseUI;
    public GameObject pauseButton;
    public TextMeshProUGUI currentLevelPlaceholder;
    public Room currentRoom;
    public PlayerMovement playerMovement;
    public WorldManager worldManager;

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
}
