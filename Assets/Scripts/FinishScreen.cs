using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishScreen : MonoBehaviour
{
    [SerializeField] private Text mainCollectablesText = null;
    [SerializeField] private Text sideCollectablesText = null;

    public void Continue()
    {
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        playerMovement.Enable();
        LevelManager.Instance.LoadNextScene(true, true);
        gameObject.SetActive(false);
    }

    public void SetMainCollectables(string text)
    {
        mainCollectablesText.text = text;
    }

    public void SetSideCollectables(string text)
    {
        sideCollectablesText.text = text;
    }
}
