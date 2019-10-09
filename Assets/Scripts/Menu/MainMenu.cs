using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private int firstSceneIndex = 0;
    [SerializeField] private int settingsSceneIndex = 0;
    [SerializeField] private GameObject playButton = null;

    private void Start()
    {
        AudioManager.Instance.StartMenuTrack();

        firstSceneIndex = ProgressionManager.Instance.GetFirstUnfinishedLevel().buildIndex;
    }

    public void PlayGame()
    {
        LevelManager.Instance.LoadScene(firstSceneIndex, true, true);
    }

    public void SelectLevel()
    {
        LevelManager.Instance.LoadScene(1, false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenSettings()
    {
        LevelManager.Instance.LoadScene(settingsSceneIndex, false, false, false);
    }
}
