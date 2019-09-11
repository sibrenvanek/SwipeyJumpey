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

    [SerializeField] private RoomEntrance[] entrances = null;
    public RoomEntrance[] Entrances { get {return entrances;} private set{} }

    private void Awake()
    {
        worldManager = FindObjectOfType<WorldManager>();
    }

    public void OnEnterRoom()
    {
        worldManager.SetCurrentRoom(this);
    }

    public void Open()
    {
        foreach (var item in entrances)
        {
            item.Open();
        }
    }

    public void Close()
    {
        foreach (var item in entrances)
        {
            item.Close();
        }
    }
}
