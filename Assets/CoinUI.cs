using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinCounterText;

    private void Start()
    {
        FindObjectOfType<PlayerManager>().OnSideCollectableIncreased += IncreaseCoinText;
        coinCounterText.text = 0 + "";
    }

    private void IncreaseCoinText(int coinsCollected)
    {
        coinCounterText.text = coinsCollected + "";
    }
}
