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

        Level latestLevel = ProgressionManager.Instance.GetLatestLevel();

        if (latestLevel != null)
        {
            Debug.Log("THERE IS A LATEST LEVEL");
            firstSceneIndex = latestLevel.buildIndex;

            playButton.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
            GameObject.FindGameObjectWithTag("PlayText").GetComponentInChildren<RectTransform>().sizeDelta = new Vector2(270, GameObject.FindGameObjectWithTag("PlayText").GetComponentInChildren<RectTransform>().sizeDelta.y);
        }
    }

    public void PlayGame()
    {
        ProgressionManager.Instance.UseProgression = true;
        LevelManager.Instance.LoadScene(firstSceneIndex, true);
    }

    public void SelectLevel()
    {
        ProgressionManager.Instance.UseProgression = false;
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
