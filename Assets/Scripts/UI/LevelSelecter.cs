using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelSelecter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelNameDisplay = null;
    [SerializeField] private TextMeshProUGUI amountOfCollectablesDisplay = null;
    [SerializeField] private TextMeshProUGUI amountOfDeathsDisplay = null;
    private LevelPreview[] levelPreviews;
    private int activePreviewIndex = 0;

    void Start()
    {
        levelPreviews = GetComponentsInChildren<LevelPreview>();
        SetActiveIndex(activePreviewIndex);
        CheckUnlockedLevels();
        SetStats();
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
        amountOfCollectablesDisplay.text = selectedLevel.amountOfMainCollectables.ToString();
        if (selectedLevel.completed)
            amountOfCollectablesDisplay.text += "/" + levelPreviews[activePreviewIndex].GetAmountCollectables();
        amountOfDeathsDisplay.text = selectedLevel.amountOfDeaths.ToString();
    }

    public void GoTo()
    {
        int sceneIndex = levelPreviews[activePreviewIndex].GetSceneIndex();
        Level level = ProgressionManager.Instance.GetLevel(sceneIndex);

        if (level != null)
        {
            ProgressionManager.Instance.ResetLevel(level);
        }

        LevelManager.Instance.LoadScene(sceneIndex);
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
    }

    public void Right()
    {
        int newIndex = (activePreviewIndex < levelPreviews.Length - 1) ? activePreviewIndex + 1 : activePreviewIndex;
        if (levelPreviews[newIndex].GetUnlocked())
        {
            SetActiveIndex(newIndex);
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

    private void CheckUnlockedLevels()
    {
        int unlockedLevelsCount = ProgressionManager.Instance.GetUnlockedLevels().Count;
        if (unlockedLevelsCount <= 0)
        {
            ProgressionManager.Instance.AddLevel(new Level
            {
                buildIndex = levelPreviews[0].GetSceneIndex(),
                sceneName = ProgressionManager.GetSceneNameFromIndex(levelPreviews[0].GetSceneIndex())
            });
        }
        foreach (LevelPreview levelPreview in levelPreviews)
        {
            levelPreview.SetUnlocked(ProgressionManager.Instance.CheckIfLevelExists(levelPreview.GetSceneIndex()));
        }
    }
}
