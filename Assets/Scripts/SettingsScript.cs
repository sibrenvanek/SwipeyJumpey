using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    public static string MUSICPREF = "musicPref";
    public static string SOUNDEFFECTSPREF = "soundEffectPref";
    [SerializeField] private GameObject settingsPanel = null;
    [SerializeField] private GameObject deleteProgressionPopUp = null;
    [SerializeField] private Toggle toggleMusic = null;
    [SerializeField] private Toggle toggleSoundEffects = null;
    [SerializeField] private Toggle toggleFont = null;


    public void Awake()
    {
        FontManager.canToggleFont = false;
        toggleFont.isOn = PlayerPrefs.GetInt(FontManager.DYSLEXICPREF) != 0;
        FontManager.canToggleFont = true;

        if (PlayerPrefs.HasKey(MUSICPREF))
        {
            toggleMusic.isOn = PlayerPrefs.GetInt(MUSICPREF) != 0;
        }
        else
        {
            PlayerPrefs.SetInt(MUSICPREF, 1);
        }

        if (PlayerPrefs.HasKey(SOUNDEFFECTSPREF))
        {
            toggleSoundEffects.isOn = PlayerPrefs.GetInt(SOUNDEFFECTSPREF) != 0;
        }
        else
        {
            PlayerPrefs.SetInt(SOUNDEFFECTSPREF, 1);
        }
    }

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
        ProgressionManager.Instance.DeleteProgression();
        settingsPanel.SetActive(true);
        deleteProgressionPopUp.SetActive(false);
    }

    public void CancelDeleteProgression()
    {
        settingsPanel.SetActive(true);
        deleteProgressionPopUp.SetActive(false);
    }

    public void ToggleMusic()
    {
        PlayerPrefs.SetInt(MUSICPREF, toggleMusic.isOn ? 1 : 0);
        if(toggleMusic.isOn)
            AudioManager.Instance.EnableMusic();
        else
            AudioManager.Instance.MuteMusic();
    }

    public void ToggleSoundEffects()
    {
        PlayerPrefs.SetInt(SOUNDEFFECTSPREF, toggleSoundEffects.isOn ? 1 : 0);
        if(toggleSoundEffects.isOn)
            AudioManager.Instance.EnableSFX();
        else
            AudioManager.Instance.MuteSFX();
    }
}
