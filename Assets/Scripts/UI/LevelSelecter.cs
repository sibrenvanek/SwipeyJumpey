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
        SetStats();
        enabledColor = leftButton.color;
        SetColors();
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
        levelNameDisplay.text = levelPreviews[activePreviewIndex].GetLevel().levelName;
        if (updateStats)
        {
            SetStats();
        }
    }

    private void SetStats()
    {
        if (levelPreviews[activePreviewIndex].GetLevel().completed)
            amountOfCollectablesDisplay.text += levelPreviews[activePreviewIndex].GetLevel().amountOfMainCollectables + "/" + levelPreviews[activePreviewIndex].GetLevel().totalAmountOfMainCollectables;
        else
            amountOfCollectablesDisplay.text = levelPreviews[activePreviewIndex].GetLevel().totalAmountOfMainCollectables.ToString();

        amountOfDeathsDisplay.text = levelPreviews[activePreviewIndex].GetLevel().amountOfDeaths.ToString();
    }

    public void GoTo()
    {
        int sceneIndex = levelPreviews[activePreviewIndex].GetLevel().buildIndex;

        LevelManager.Instance.LoadScene(sceneIndex, true, true);
    }

    public void Return()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void Left()
    {
        int newIndex = (activePreviewIndex > 0) ? activePreviewIndex - 1 : activePreviewIndex;
        if (levelPreviews[newIndex].GetLevel().unlocked)
        {
            SetActiveIndex(newIndex);
        }

        SetColors();
    }

    public void Right()
    {
        int newIndex = (activePreviewIndex < levelPreviews.Length - 1) ? activePreviewIndex + 1 : activePreviewIndex;
        if (levelPreviews[newIndex].GetLevel().unlocked)
        {
            SetActiveIndex(newIndex);
        }

        SetColors();
    }

    private void SetColors()
    {
        leftButton.color = enabledColor;
        rightButton.color = enabledColor;

        if (activePreviewIndex == 0)
        {
            leftButton.color = disabledColor;
        }
        if (activePreviewIndex >= levelPreviews.Length - 1)
        {
            rightButton.color = disabledColor;
        }
        else if (!levelPreviews[activePreviewIndex + 1].GetLevel().unlocked)
        {
            rightButton.color = disabledColor;
        }
    }

    public void SetActiveIndexByScene(int sceneIndex)
    {
        for (int i = 0; i < levelPreviews.Length; i++)
        {
            if (levelPreviews[i].GetLevel().buildIndex == sceneIndex)
            {
                SetActiveIndex(i);
            }
        }
    }
}
