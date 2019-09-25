using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIWorldSelecter : MonoBehaviour
{
    [SerializeField] private WorldSelecter worldSelecter = null;
    [SerializeField] private Button goToButton = null;
    [SerializeField] private TextMeshProUGUI worldTextMesh = null;

    private WorldPreview activeWorld;

    private void Awake()
    {
        worldSelecter.OnWorldChanged += WorldChanged;
    }

    private void WorldChanged(WorldPreview world)
    {
        activeWorld = world;

        ChangeGoToButton();
        ChangeWorldTextMesh();
    }

    private void ChangeGoToButton()
    {
        goToButton.enabled = !activeWorld.IsInDevelopment;
    }

    private void ChangeWorldTextMesh()
    {
        worldTextMesh.text = activeWorld.WorldName;
    }

    public void GoToClick()
    {
        SceneManager.LoadScene(activeWorld.SceneIndex);
    }

    public void ReturnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void LeftClick()
    {
        worldSelecter.Left();
    }

    public void RightClick()
    {
        worldSelecter.Right();
    }
}
