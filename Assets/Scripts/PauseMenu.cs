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

    public void Start()
    {
        currentLevelPlaceholder.text = levelText;
    }

    public void Resume()
    {
        pauseButton.SetActive(true);
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseButton.SetActive(false);
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
