using UnityEngine;
using System;

public class WorldSelecter : MonoBehaviour
{
    [SerializeField] private Sprite[] worldSprites;
    [SerializeField] private int[] sceneIndexes;
    [SerializeField] private WorldPreview worldPreviewOne, worldPreviewTwo, worldPreviewThree = null;
    private Vector2 baseMousePosition = Vector2.zero;
    private Vector2 releaseMousePosition = Vector2.zero;
    private int activeWorldIndex = 0;
    private bool dragging = false;

    void Start()
    {
        worldPreviewTwo.active = true;
        UpdatePreviews();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButton(0) && !dragging)
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
        if (releaseMousePosition.x < baseMousePosition.x)
            activeWorldIndex = (activeWorldIndex > 0) ? activeWorldIndex - 1 : activeWorldIndex;
        else
            activeWorldIndex = (activeWorldIndex < worldSprites.Length - 1) ? activeWorldIndex + 1 : activeWorldIndex;

        UpdatePreviews();
    }

    private void UpdatePreviews()
    {
        if (activeWorldIndex <= 0) {
            worldPreviewOne.gameObject.SetActive(false);
        }
        else
        {
            worldPreviewOne.gameObject.SetActive(true);
            worldPreviewOne.SetSprite(worldSprites[activeWorldIndex - 1]);
            worldPreviewOne.SetSceneIndex(sceneIndexes[activeWorldIndex - 1]);
        }

        if (activeWorldIndex >= worldSprites.Length - 1)
        {
            worldPreviewThree.gameObject.SetActive(false);
        }
        else
        {
            worldPreviewThree.gameObject.SetActive(true);
            worldPreviewThree.SetSprite(worldSprites[activeWorldIndex + 1]);
            worldPreviewThree.SetSceneIndex(sceneIndexes[activeWorldIndex + 1]);
        }

        worldPreviewTwo.SetSprite(worldSprites[activeWorldIndex]);
        worldPreviewTwo.SetSceneIndex(sceneIndexes[activeWorldIndex]);
    }
}
