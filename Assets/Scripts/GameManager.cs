using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Checkpoint latestCheckPoint = null;

    [SerializeField] private Player player = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void SetLatestCheckpoint(Checkpoint checkpoint)
    {
        latestCheckPoint = checkpoint;
    }

    public void ResetPlayerToCheckpoint()
    {
        player.transform.position = latestCheckPoint.transform.position;
    }
}
