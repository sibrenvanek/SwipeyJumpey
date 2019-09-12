using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheatUIManager : MonoBehaviour
{

    [Header("God mode labels")]
    [SerializeField] private string godModeText = "God mode";

    private TextMeshProUGUI cheatTextObject;
    private PlayerManager playerManager;

    private void Awake()
    {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        cheatTextObject = GameObject.FindGameObjectWithTag("CheatText").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        playerManager.OnGodMode += SetGodMode;
    }

    private void SetGodMode(bool isEnabled)
    {
        if (isEnabled)
        {
            cheatTextObject.text = godModeText;
        }
        else
        {
            cheatTextObject.text = "";
        }
    }
}
