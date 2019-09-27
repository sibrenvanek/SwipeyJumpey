using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FontManager : MonoBehaviour
{
    private string DYSLEXICPREF = "dyslecticFont";

    [SerializeField] private TMP_FontAsset defaultFont = null;
    [SerializeField] private TMP_FontAsset dyslexicFont = null;

    private bool dyslexicFontEnabled = false;
    private bool dyslexicfFontSet = false;

    private void Start()
    {
        CheckForFontChange();
    }

    private void Update()
    {
        CheckForFontChange();
    }

    private void CheckForFontChange()
    {
        dyslexicFontEnabled = PlayerPrefs.GetInt(DYSLEXICPREF) == 1;

        if (dyslexicFontEnabled && !dyslexicfFontSet)
            SetDystlexicFont();

        if (!dyslexicFontEnabled && dyslexicfFontSet)
            SetDefaultFont();
    }

    private void SetDystlexicFont()
    {
        SetFont(dyslexicFont);
        dyslexicfFontSet = true;
    }

    private void SetDefaultFont()
    {
        SetFont(defaultFont);
        dyslexicfFontSet = false;
    }

    private void SetFont(TMP_FontAsset font)
    {
        TextMeshProUGUI[] textComponents = FindObjectsOfType<TextMeshProUGUI>();
        foreach (TextMeshProUGUI component in textComponents)
            component.font = font;
    }

    public void ToggleDyslexicFont()
    {
        if (PlayerPrefs.HasKey(DYSLEXICPREF))
        {
            int value = PlayerPrefs.GetInt(DYSLEXICPREF) == 0 ? 1 : 0;
            PlayerPrefs.SetInt(DYSLEXICPREF, value);

            dyslexicFontEnabled = value == 1;
        }
        else
        {
            PlayerPrefs.SetInt(DYSLEXICPREF, 1);
            dyslexicFontEnabled = true;
        }
    }
}
