using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelecter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelNameDisplay = null;
    [SerializeField] private TextMeshProUGUI amountOfCollectablesDisplay = null;
    [SerializeField] private TextMeshProUGUI amountOfDeathsDisplay = null;
    [SerializeField] private Image leftButton = null;
    [SerializeField] private Image rightButton = null;
    [SerializeField] private Color disabledColor = Color.grey;
    private Color enabledColor = Color.white;
    private LevelPreview[] levelPreviews;
    private int activePreviewIndex = 0;

    void Start()
    {
        levelPreviews = GetComponentsInChildren<LevelPreview>();
        SetActiveIndex(activePreviewIndex);
        SetLevelColors();
        SetStats();
        enabledColor = leftButton.color;
        leftButton.color = disabledColor;
    }

    private void SetActiveIndex(int index)
    {
        bool updateStats = true;
        levelPreviews[activePreviewIndex].SetInActive();
        if (activePreviewIndex == index)
        {
            updateStats = false;
        }
        activePreviewIndex = index;
        levelPreviews[activePreviewIndex].SetActivated();
        levelNameDisplay.text = levelPreviews[activePreviewIndex].GetName();
        if (updateStats)
        {
            SetStats();
        }
    }

    private void SetStats()
    {
        Level selectedLevel = ProgressionManager.Instance.GetLevel(levelPreviews[activePreviewIndex].GetSceneIndex());
        selectedLevel.totalAmountOfMainCollectables = levelPreviews[activePreviewIndex].GetAmountOfMainCollectables();
        selectedLevel.totalAmountOfSideCollectables = levelPreviews[activePreviewIndex].GetAmountOfSideCollectables();
        selectedLevel.levelName = levelPreviews[activePreviewIndex].GetLevelName();
        amountOfCollectablesDisplay.text = selectedLevel.amountOfMainCollectables.ToString();
        if (selectedLevel.completed)
            amountOfCollectablesDisplay.text += "/" + levelPreviews[activePreviewIndex].GetAmountOfMainCollectables();
        amountOfDeathsDisplay.text = selectedLevel.amountOfDeaths.ToString();
    }

    public void GoTo()
    {
        int sceneIndex = levelPreviews[activePreviewIndex].GetSceneIndex();

        LevelManager.Instance.LoadScene(sceneIndex, true, true);
    }

    public void Return()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void Left()
    {
        int newIndex = (activePreviewIndex > 0) ? activePreviewIndex - 1 : activePreviewIndex;
        if (levelPreviews[newIndex].GetUnlocked())
        {
            SetActiveIndex(newIndex);
        }

        if (newIndex == 0)
        {
            leftButton.color = disabledColor;
        }
        else
        {
            rightButton.color = enabledColor;
        }
    }

    public void Right()
    {
        int newIndex = (activePreviewIndex < levelPreviews.Length - 1) ? activePreviewIndex + 1 : activePreviewIndex;
        if (levelPreviews[newIndex].GetUnlocked())
        {
            SetActiveIndex(newIndex);
        }

        if (newIndex >= levelPreviews.Length - 1)
        {
            rightButton.color = disabledColor;
        }
        else if (!levelPreviews[newIndex + 1].GetUnlocked())
        {
            rightButton.color = disabledColor;
        }
        else
        {
            leftButton.color = enabledColor;
        }
    }

    public void SetActiveIndexByScene(int sceneIndex)
    {
        for (int i = 0; i < levelPreviews.Length; i++)
        {
            if (levelPreviews[i].GetSceneIndex() == sceneIndex)
            {
                SetActiveIndex(i);
            }
        }
    }

    private void SetLevelColors()
    {
        foreach (LevelPreview levelPreview in levelPreviews)
        {
            Level level = ProgressionManager.Instance.GetLevel(levelPreview.GetSceneIndex());

            if (level == null)
                levelPreview.SetColors(false, false);
            else
                levelPreview.SetColors(true, level.completed);
        }
    }
}
