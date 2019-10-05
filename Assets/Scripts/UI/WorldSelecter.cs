using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldSelecter : MonoBehaviour
{
    [Header("Points")]
    [SerializeField] private GameObject selectedPoint = null;
    [SerializeField] private GameObject previousPoint = null;
    [SerializeField] private GameObject nextPoint = null;

    [Header("Available worlds")]
    [SerializeField] private WorldPreview[] worlds = null;

    [SerializeField] private Image leftButton = null;
    [SerializeField] private Image rightButton = null;
    [SerializeField] private Color disabledColor = Color.grey;
    private Color enabledColor = Color.white;

    public event Action<WorldPreview> OnWorldChanged = delegate {};
    private int activeWorldIndex = 0;

    private Vector2 baseMousePosition = Vector2.zero;
    private Vector2 releaseMousePosition = Vector2.zero;

    private void Start()
    {
        WorldChanged(0);
        enabledColor = leftButton.color;
        leftButton.color = disabledColor;
    }

    private void Update()
    {}

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

        if (newIndex == 0)
        {
            leftButton.color = disabledColor;
        }
        else
        {
            rightButton.color = enabledColor;
        }
    }

    public void Right()
    {
        int newIndex = (activeWorldIndex < worlds.Length - 1) ? activeWorldIndex + 1 : activeWorldIndex;

        if (newIndex != activeWorldIndex)
            WorldChanged(newIndex);

        if (newIndex >= worlds.Length - 1)
        {
            rightButton.color = disabledColor;
        }
        else
        {
            leftButton.color = enabledColor;
        }
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
