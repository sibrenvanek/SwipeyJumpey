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
    [SerializeField] private Scene[] scenes;
    private LevelPreview[] levelPreviews;
    private int activePreviewIndex = 0;

    void Start()
    {
        levelPreviews = GetComponentsInChildren<LevelPreview>();
        SetActiveIndex(activePreviewIndex);
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
        int sceneNumber = SceneManager.sceneCountInBuildSettings;
        string[] arrayOfNames;
        arrayOfNames = new string[sceneNumber];
        for (int index = 0; index < sceneNumber; index++)
        {
            arrayOfNames[index] = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(index));
        }
        Level selectedLevel = ProgressionManager.Instance.GetLevel(arrayOfNames[levelPreviews[activePreviewIndex].GetSceneIndex()]);
        amountOfCollectablesDisplay.text = selectedLevel.amountOfMainCollectables.ToString();
        amountOfDeathsDisplay.text = selectedLevel.amountOfDeaths.ToString();
    }

    public void GoTo()
    {
        LevelManager.Instance.LoadScene(levelPreviews[activePreviewIndex].GetSceneIndex());
    }

    public void Return()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void Left()
    {
        int newIndex = (activePreviewIndex > 0) ? activePreviewIndex - 1 : activePreviewIndex;
        SetActiveIndex(newIndex);
    }

    public void Right()
    {
        int newIndex = (activePreviewIndex < levelPreviews.Length - 1) ? activePreviewIndex + 1 : activePreviewIndex;
        SetActiveIndex(newIndex);
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
}
