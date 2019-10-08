using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinishScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI mainCollectablesText = null;
    [SerializeField] private TextMeshProUGUI sideCollectablesText = null;
    [SerializeField] private TextMeshProUGUI deathCounterText = null;
    [SerializeField] private TextMeshProUGUI levelNameText = null;

    [SerializeField] private Slider mainSlider = null;
    [SerializeField] private Slider sideSlider = null;

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

    public void SetMainCollectables(int collected, int total)
    {
        mainCollectablesText.text = collected + " / " + total;
        sideSlider.value = (float) collected / total;
    }

    public void SetSideCollectables(int collected, int total)
    {
        sideCollectablesText.text = collected + " / " + total;
        sideSlider.value = (float) collected / total;
    }

    public void SetDeathCounter(int deaths)
    {
        deathCounterText.text = deaths + "";
    }
}
