using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private float fadeTime = 1f;

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

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FadeIn();
    }

    public void FadeIn()
    {
        DarthFader.Instance.FadeGameIn(fadeTime);
    }

    public void LoadScene(int sceneIndex, bool loadingIndicator = true, bool resetLevel = false)
    {
        if (resetLevel)
        {
            Level level = ProgressionManager.Instance.GetLevel(sceneIndex);

            if (level != null)
            {
                ProgressionManager.Instance.ResetLevel(level);
            }
        }

        DarthFader.Instance.FadeGameOut(fadeTime, loadingIndicator);
        StartCoroutine(LoadLevelAfterSeconds(sceneIndex, fadeTime));
    }

    public void LoadNextScene(bool loadingIndicator = true, bool resetLevel = false)
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (resetLevel)
        {
            Level level = ProgressionManager.Instance.GetLevel(sceneIndex);

            if (level != null)
            {
                ProgressionManager.Instance.ResetLevel(level);
            }
        }

        DarthFader.Instance.FadeGameOut(fadeTime, loadingIndicator);
        StartCoroutine(LoadLevelAfterSeconds(sceneIndex, fadeTime));
    }

    private IEnumerator LoadLevelAfterSeconds(int levelIndex, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(levelIndex);
    }
}
