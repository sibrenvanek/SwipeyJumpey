using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSelecter : MonoBehaviour
{
    [SerializeField] private Sprite[] worldSprites;
    [SerializeField] private int[] sceneIndexes;
    [SerializeField] private WorldPreview worldPreviewOne, worldPreviewTwo, worldPreviewThree;
    private int activeWorldIndex = 0;
    private Vector2 baseMousePosition = Vector2.zero;
    private bool dragging = false;

    void Start()
    {
        if (worldSprites.Length != sceneIndexes.Length)
            Debug.LogError("worldsprites length != sceneindexes length");
        
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
            Vector2 releasePosition = Input.mousePosition;

            if (releasePosition.x < baseMousePosition.x)
                SlideLeft();
            else
                SlideRight();
        }
    }

    private void SlideLeft()
    {
        activeWorldIndex = (activeWorldIndex > 0) ? activeWorldIndex - 1 : activeWorldIndex;
        UpdatePreviews();
    }

    private void SlideRight()
    {
        activeWorldIndex = (activeWorldIndex < worldSprites.Length) ? activeWorldIndex + 1 : activeWorldIndex;
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
            worldPreviewOne.sceneIndex = sceneIndexes[activeWorldIndex - 1];
        }

        if (activeWorldIndex >= sceneIndexes.Length - 1)
        {
            worldPreviewThree.gameObject.SetActive(false);
        }
        else
        {
            worldPreviewThree.gameObject.SetActive(true);
            worldPreviewThree.SetSprite(worldSprites[activeWorldIndex + 1]);
            worldPreviewThree.sceneIndex = sceneIndexes[activeWorldIndex + 1];
        }

        worldPreviewTwo.SetSprite(worldSprites[activeWorldIndex]);
        worldPreviewTwo.sceneIndex = sceneIndexes[activeWorldIndex];
    }
}
