using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsScript : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel = null;
    [SerializeField] private GameObject deleteProgressionPopUp = null;

    public void BackToMenu()
    {
        ProgressionManager.Instance.SaveProgression();
        SceneManager.LoadScene(0);
    }

    public void ShowDeleteProgressionPopUp()
    {
        settingsPanel.SetActive(false);
        deleteProgressionPopUp.SetActive(true);
    }

    public void DeleteProgression()
    {
        //ProgressionManager.Instance.DeleteProgression();
    }

    public void CancelDeleteProgression()
    {
        settingsPanel.SetActive(true);
        deleteProgressionPopUp.SetActive(false);
    }

    public void ToggleMusic()
    {
        //ProgressionManager.Instance.ToggleMusic();
    }

    public void ToggleSoundEffects()
    {
        //ProgressionManager.Instance.ToggleSoundEffects();
    }
}
