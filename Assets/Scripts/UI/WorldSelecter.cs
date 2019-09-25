using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class WorldSelecter : MonoBehaviour
{
    [Header("Points")]
    [SerializeField] private GameObject selectedPoint = null;
    [SerializeField] private GameObject previousPoint = null;
    [SerializeField] private GameObject nextPoint = null;

    [Header("Available worlds")]
    [SerializeField] private WorldPreview[] worlds = null;

    public event Action<WorldPreview> OnWorldChanged = delegate { };
    private int activeWorldIndex = 0;

    private bool dragging = false;
    private Vector2 baseMousePosition = Vector2.zero;
    private Vector2 releaseMousePosition = Vector2.zero;

    private void Start()
    {
        WorldChanged(0);
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
        if (releaseMousePosition.x < baseMousePosition.x)
            Left();
        else
            Right();
    }

    public void Left()
    {
        int newIndex = (activeWorldIndex > 0) ? activeWorldIndex - 1 : activeWorldIndex;

        if (newIndex != activeWorldIndex)
            WorldChanged(newIndex);
    }

    public void Right()
    {
        int newIndex = (activeWorldIndex < worlds.Length - 1) ? activeWorldIndex + 1 : activeWorldIndex;

        if (newIndex != activeWorldIndex)
            WorldChanged(newIndex);
    }

    private void WorldChanged(int newActiveIndex)
    {
        for (int i = 0; i < worlds.Length; i++)
        {
            WorldPreview world = worlds[i];

            if (i == newActiveIndex)
            {
                world.gameObject.SetActive(true);
                world.transform.position = selectedPoint.transform.position;
            }
            else if (i == newActiveIndex - 1)
            {
                world.gameObject.SetActive(true);
                world.transform.position = previousPoint.transform.position;
            }
            else if (i == newActiveIndex + 1)
            {
                world.gameObject.SetActive(true);
                world.transform.position = nextPoint.transform.position;
            }
            else
            {
                world.gameObject.SetActive(false);
            }
        }

        activeWorldIndex = newActiveIndex;
        OnWorldChanged.Invoke(worlds[activeWorldIndex]);
    }
}
