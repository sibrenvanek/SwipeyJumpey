using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(ProgressionManager.Instance.GetNextSceneBuildIndex());
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
