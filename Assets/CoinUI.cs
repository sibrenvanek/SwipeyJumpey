using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinCounterText;

    private int coinCounter = 0;

    private void Start()
    {
        coinCounterText.text = coinCounter + "";
        FindObjectOfType<ProgressionManager>().OnSideCollectableIncreased += IncreaseCoinText;
    }

    private void IncreaseCoinText(int amount)
    {
        coinCounter = coinCounter + amount;
        coinCounterText.text = coinCounter + "";
    }
}
