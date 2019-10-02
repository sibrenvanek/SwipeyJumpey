using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelecter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelNameDisplay = null;
    private LevelPreview[] levelPreviews;
    private int activePreviewIndex = 0;

    void Start()
    {
        levelPreviews = GetComponentsInChildren<LevelPreview>();
        SetActiveIndex(activePreviewIndex);
    }

    private void SetActiveIndex (int index)
    {
        levelPreviews[activePreviewIndex].SetInActive();
        activePreviewIndex = index;
        levelPreviews[activePreviewIndex].SetActivated();
        levelNameDisplay.text = levelPreviews[activePreviewIndex].GetName();
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
