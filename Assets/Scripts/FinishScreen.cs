using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishScreen : MonoBehaviour
{
    [SerializeField] private Text mainCollectablesText = null;
    [SerializeField] private Text sideCollectablesText = null;

    public void Continue(bool endOfWorld = false)
    {
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        playerMovement.Enable();

        if (endOfWorld)
        {
            StarDestroyer.DestroyTheStars();
            LevelManager.Instance.LoadScene(1);
        }
        else
        {
            LevelManager.Instance.LoadNextScene(true, true);
        }

        ProgressionManager.Instance.ResetSideCollectablesAll();
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
