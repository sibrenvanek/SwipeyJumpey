using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelecter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelNameDisplay = null;
    private LevelPreview[] levelPreviews;
    private int activePreviewIndex = 0;
    private Vector2 baseMousePosition = Vector2.zero;
    private Vector2 releaseMousePosition = Vector2.zero;
    private bool dragging = false;

    void Start()
    {
        levelPreviews = GetComponentsInChildren<LevelPreview>();
        SetActiveIndex(activePreviewIndex);
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButton(0) && !dragging && !EventSystem.current.IsPointerOverGameObject())
        {
            dragging = true;
            baseMousePosition = Input.mousePosition;
        }
        else if (!Input.GetMouseButton(0) && dragging)
        {
            dragging = false;
            releaseMousePosition = Input.mousePosition;

            if (Mathf.Abs(releaseMousePosition.x - baseMousePosition.x) < 2)
                return;

            Slide();
        }
    }

    private void Slide()
    {
        int newIndex;

        if (releaseMousePosition.x < baseMousePosition.x)
            newIndex = (activePreviewIndex > 0) ? activePreviewIndex - 1 : activePreviewIndex;
        else
            newIndex = (activePreviewIndex < levelPreviews.Length - 1) ? activePreviewIndex + 1 : activePreviewIndex;

        SetActiveIndex(newIndex);
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
