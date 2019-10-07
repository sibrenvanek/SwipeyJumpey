using UnityEngine;
using UnityEngine.UI;

public class FinishScreen : MonoBehaviour
{
    [SerializeField] private Text mainCollectablesText = null;
    [SerializeField] private Text sideCollectablesText = null;
    [SerializeField] private Text deathCounterText = null;
    [SerializeField] private Text levelNameText = null;

    public void Continue(bool endOfWorld = false)
    {
        ProgressionManager.Instance.ResetSideCollectablesAll();

        if (endOfWorld)
        {
            StarDestroyer.DestroyTheStars();
            LevelManager.Instance.LoadScene(1);
        }
        else
        {
            LevelManager.Instance.LoadNextScene(true, true);
        }
    }

    public void SetLevelName(string text)
    {
        levelNameText.text = text;
    }

    public void SetMainCollectables(string text)
    {
        mainCollectablesText.text = text;
    }

    public void SetSideCollectables(string text)
    {
        sideCollectablesText.text = text;
    }

    public void SetDeathCounter(string text)
    {
        deathCounterText.text = text;
    }
}
