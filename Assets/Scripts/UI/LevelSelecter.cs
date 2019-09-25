using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelSelecter : MonoBehaviour
{
    private LevelPreview[] levelPreviews;
    private int activePreviewIndex = 0;
    private Vector2 baseMousePosition = Vector2.zero;
    private Vector2 releaseMousePosition = Vector2.zero;
    private bool dragging = false;

    void Start()
    {
        levelPreviews = GetComponentsInChildren<LevelPreview>();
        levelPreviews[activePreviewIndex].SetActivated();
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
            Slide();
        }
    }

    private void Slide()
    {
        levelPreviews[activePreviewIndex].SetInActive();

        if (releaseMousePosition.x < baseMousePosition.x)
            activePreviewIndex = (activePreviewIndex < levelPreviews.Length - 1) ? activePreviewIndex + 1 : activePreviewIndex;
        else
            activePreviewIndex = (activePreviewIndex > 0) ? activePreviewIndex - 1 : activePreviewIndex;

        levelPreviews[activePreviewIndex].SetActivated();
    }

    public void GoTo()
    {
        SceneManager.LoadScene(levelPreviews[activePreviewIndex].GetSceneIndex());
    }

    public void Return()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void Left()
    {
        levelPreviews[activePreviewIndex].SetInActive();
        activePreviewIndex = (activePreviewIndex > 0) ? activePreviewIndex - 1 : activePreviewIndex;
        levelPreviews[activePreviewIndex].SetActivated();
    }

    public void Right()
    {
        levelPreviews[activePreviewIndex].SetInActive();
        activePreviewIndex = (activePreviewIndex < levelPreviews.Length - 1) ? activePreviewIndex + 1 : activePreviewIndex;
        levelPreviews[activePreviewIndex].SetActivated();
    }
}
