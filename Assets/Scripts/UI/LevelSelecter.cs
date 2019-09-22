using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelecter : MonoBehaviour
{
    private WorldPreview[] worldPreviews;
    private int activeWorldIndex = 0;
    private Vector2 baseMousePosition = Vector2.zero;
    private bool dragging = false;

    void Start()
    {
        worldPreviews = GetComponentsInChildren<WorldPreview>();
        worldPreviews[activeWorldIndex].SetActivated();
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
        worldPreviews[activeWorldIndex].SetInActive();
        activeWorldIndex = (activeWorldIndex > 0) ? activeWorldIndex - 1 : activeWorldIndex;
        worldPreviews[activeWorldIndex].SetActivated();
    }

    private void SlideRight()
    {
        worldPreviews[activeWorldIndex].SetInActive();
        activeWorldIndex = (activeWorldIndex < worldPreviews.Length - 1) ? activeWorldIndex + 1 : activeWorldIndex;
        worldPreviews[activeWorldIndex].SetActivated();
    }
}
