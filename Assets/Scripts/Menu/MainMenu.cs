using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private string playScene = "World-1-Level-1";
    [SerializeField] private GameObject playButton = null;
    private void Awake()
    {
        Level latestLevel = ProgressionManager.Instance.GetLatestLevel();
        if (latestLevel != null)
        {
            playScene = latestLevel.sceneName;

            playButton.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
            GameObject.FindGameObjectWithTag("PlayText").GetComponentInChildren<RectTransform>().sizeDelta = new Vector2(270, GameObject.FindGameObjectWithTag("PlayText").GetComponentInChildren<RectTransform>().sizeDelta.y);
        }
    }

    private void Start()
    {
        AudioManager.Instance.StartMenuTrack();
    }

    public void PlayGame()
    {
        ProgressionManager.Instance.UseProgression = true;
        SceneManager.LoadScene(1);
    }

    public void SelectLevel()
    {
        ProgressionManager.Instance.UseProgression = false;
        LevelManager.Instance.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("SettingsScene");
    }
}
