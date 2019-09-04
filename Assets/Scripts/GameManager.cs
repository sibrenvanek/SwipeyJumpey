using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Checkpoint latestCheckPoint = null;

    [SerializeField] private PlayerManager player = null;
    [SerializeField] private float respawnYOffset = 0.2f;

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
        player = FindObjectOfType<PlayerManager>();
    }

    public void SetLatestCheckpoint(Checkpoint checkpoint)
    {
        latestCheckPoint = checkpoint;
    }

    public void ResetPlayerToCheckpoint()
    {
        HangingPoint[] hangingpoints = FindObjectsOfType<HangingPoint>();
        foreach(HangingPoint fuel in hangingpoints) {
            fuel.ResetPoint();
        }
        player.transform.position = new Vector3(latestCheckPoint.transform.position.x, latestCheckPoint.transform.position.y + respawnYOffset);
    }
}
