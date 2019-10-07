using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionStreakManager : MonoBehaviour
{
    [SerializeField] private float pitchIncreaseAmount = .1f;
    private int currentStreak = 0;
    [SerializeField] private float intervalToStreakEnd = 1f;
    private float lastPickUpTime = 0f;

    private PlayerManager player = null;

    private void Start() 
    {
        player = FindObjectOfType<PlayerManager>();
        player.OnCollectCoin += AddToStreak;
    }

    private void AddToStreak(Collectable collectable)
    {
        if(Time.time - lastPickUpTime > intervalToStreakEnd)
        {
            currentStreak = 0;
        }
        lastPickUpTime = Time.time;
        currentStreak++;

        float pitchAddition = currentStreak * pitchIncreaseAmount;
        collectable.PlaySFX(pitchAddition);
    }

    public void EndLevel() {
        player.OnCollectCoin -= AddToStreak;
    }
}
