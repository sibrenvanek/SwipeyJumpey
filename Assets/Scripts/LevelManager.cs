using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private float fadeTime = 1f;
    private DarthFader darthFader = null;
    private int levelLoadingScene = 0;

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
        darthFader = FindObjectOfType<DarthFader>();
        darthFader.gameObject.SetActive(false);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //FadeIn();
    }

    public void FadeIn()
    {
        darthFader.FadeGameIn(fadeTime);
    }

    public void LoadNextLevel(int levelIndex)
    {
        darthFader.gameObject.SetActive(true);
        darthFader.FadeGameOut(fadeTime);
        StartCoroutine(LoadLevelAfterSeconds(levelIndex, fadeTime));
    }

    public void LoadNextLevel()
    {
        darthFader.gameObject.SetActive(true);
        int levelIndex = SceneManager.GetActiveScene().buildIndex + 1;

        darthFader.FadeGameOut(fadeTime);
        StartCoroutine(LoadLevelAfterSeconds(levelIndex, fadeTime));
    }

    private IEnumerator LoadLevelAfterSeconds(int levelIndex, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(levelIndex);
    }
}
