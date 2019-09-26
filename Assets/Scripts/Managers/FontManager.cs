using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FontManager : MonoBehaviour
{
    private string DYSLECTICPREF = "dyslecticFont";

    [SerializeField] private TMP_FontAsset defaultFont = null;
    [SerializeField] private TMP_FontAsset dyslecticFont = null;

    private bool dyslecticFontEnabled = false;
    private bool dyslectifFontSet = false;

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
        dyslecticFontEnabled = PlayerPrefs.GetInt(DYSLECTICPREF) == 1;

        if (dyslecticFontEnabled && !dyslectifFontSet)
            SetDystlecticFont();

        if (!dyslecticFontEnabled && dyslectifFontSet)
            SetDefaultFont();
    }

    private void SetDystlecticFont()
    {
        SetFont(dyslecticFont);
        dyslectifFontSet = true;
    }

    private void SetDefaultFont()
    {
        SetFont(defaultFont);
        dyslectifFontSet = false;
    }

    private void SetFont(TMP_FontAsset font)
    {
        var textComponents = Component.FindObjectsOfType<TextMeshProUGUI>();
        foreach (TextMeshProUGUI component in textComponents)
            component.font = font;
    }

    public void ToggleDyslecticFont()
    {
        if (PlayerPrefs.HasKey(DYSLECTICPREF))
        {
            int value = PlayerPrefs.GetInt(DYSLECTICPREF) == 0 ? 1 : 0;
            PlayerPrefs.SetInt(DYSLECTICPREF, value);

            dyslecticFontEnabled = value == 1;
        }
        else
        {
            PlayerPrefs.SetInt(DYSLECTICPREF, 1);
            dyslecticFontEnabled = true;
        }
    }
}
