using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private float timeBeforeLoadingScene = 1f;
    public void FadeOut()
    {
        FindObjectOfType<DarthFader>().FadeGameOut(timeBeforeLoadingScene);
    }

    public void FinishIntro()
    {
        FadeOut();
        StartCoroutine(LoadNextLevel());
    }

    public IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(timeBeforeLoadingScene);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
