using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private string roomName = "LevelName";
    public string RoomName { get { return roomName; } set { roomName = value; } }
    [SerializeField] private Checkpoint roomInitialCheckpoint = null;
    public Checkpoint RoomInitialCheckpoint { get { return roomInitialCheckpoint; } private set { roomInitialCheckpoint = value; } }
    [SerializeField] private WorldManager worldManager = null;
    private void Awake()
    {
        worldManager = FindObjectOfType<WorldManager>();
    }

    public void OnEnterRoom()
    {
        worldManager.SetCurrentRoom(this);
    }
}
