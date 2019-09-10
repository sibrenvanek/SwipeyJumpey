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
    public string levelText;
    public PlayerMovement playerMovement;

    private bool playerCanJump = false;
    private bool playerCanSlowMotionJump = false;

    public void Start()
    {
        currentLevelPlaceholder.text = levelText;
    }

    public void Resume()
    {
        pauseButton.SetActive(true);
        pauseUI.SetActive(false);
        playerMovement.SetJumpAvailable(playerCanJump);
        playerMovement.SetSlowMotionJumpAvailable(playerCanSlowMotionJump);
        playerCanJump = false;
        playerCanSlowMotionJump = false;
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseButton.SetActive(false);
        pauseUI.SetActive(true);
        playerCanJump = playerMovement.jumpAvailable;
        playerCanSlowMotionJump = playerMovement.slowMotionJumpAvailable;
        playerMovement.SetJumpAvailable(false);
        playerMovement.SetSlowMotionJumpAvailable(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void ChangeLevelText(string newText)
    {
        currentLevelPlaceholder.text = newText;
    }

    public void ResetLevel()
    {
        GameManager.Instance.ResetPlayerToCheckpoint(PickCheckpoint.initialCheckpointLevel);
        Resume();
    }

    public void ResetWorld()
    {
        GameManager.Instance.ResetPlayerToCheckpoint(PickCheckpoint.initialCheckpointWorld);
        Resume();
    }
}
