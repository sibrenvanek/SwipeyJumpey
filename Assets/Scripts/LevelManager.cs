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

    public void LoadScene(int levelIndex)
    {
        DarthFader.Instance.FadeGameOut(fadeTime);
        StartCoroutine(LoadLevelAfterSeconds(levelIndex, fadeTime));
    }

    public void LoadNextScene()
    {
        int levelIndex = SceneManager.GetActiveScene().buildIndex + 1;

        DarthFader.Instance.FadeGameOut(fadeTime);
        StartCoroutine(LoadLevelAfterSeconds(levelIndex, fadeTime));
    }

    private IEnumerator LoadLevelAfterSeconds(int levelIndex, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(levelIndex);
    }
}
