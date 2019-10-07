using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [SerializeField] private float fadeTime = 1f;
    [SerializeField] private int introSceneIndex = 0;

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
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement)
            playerMovement.Enable();
        FadeIn();
    }

    public void FadeIn()
    {
        DarthFader.Instance.FadeGameIn(fadeTime);
    }

    private int GetIndexWithIntro(int sceneIndex)
    {
        if (sceneIndex == introSceneIndex && !ProgressionManager.Instance.GetIntroPlayed())
        {
            ProgressionManager.Instance.SetIntroPlayed();
            sceneIndex--;
        }

        return sceneIndex;
    }

    public void LoadScene(int sceneIndex, bool loadingIndicator = true, bool resetLevel = false, bool fade = true)
    {
        sceneIndex = GetIndexWithIntro(sceneIndex);
        Level level = ProgressionManager.Instance.GetLevel(sceneIndex);

        if (level != null)
        {
            if (resetLevel)
            {
                ProgressionManager.Instance.ResetLevel(level);
            }

            ProgressionManager.Instance.SetLatestLevel(level);
        }

        if (fade)
        {
            DarthFader.Instance.FadeGameOut(fadeTime, loadingIndicator);
            StartCoroutine(LoadLevelAfterSeconds(sceneIndex, fadeTime));
            return;
        }

        StartCoroutine(LoadLevelAfterSeconds(sceneIndex, 0));
    }

    public void LoadNextScene(bool loadingIndicator = true, bool resetLevel = false, bool fade = true)
    {
        int sceneIndex = GetIndexWithIntro(SceneManager.GetActiveScene().buildIndex + 1);
        Level level = ProgressionManager.Instance.GetLevel(sceneIndex);

        if (resetLevel)
        {

            if (level != null)
            {
                ProgressionManager.Instance.ResetLevel(level);
            }

            ProgressionManager.Instance.SetLatestLevel(level);
        }

        if (fade)
        {
            DarthFader.Instance.FadeGameOut(fadeTime, loadingIndicator);
            StartCoroutine(LoadLevelAfterSeconds(sceneIndex, fadeTime));
            return;
        }

        StartCoroutine(LoadLevelAfterSeconds(sceneIndex, 0));
    }

    private IEnumerator LoadLevelAfterSeconds(int levelIndex, float seconds)
    {
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement)
            playerMovement.Disable();
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(levelIndex);
    }
}
