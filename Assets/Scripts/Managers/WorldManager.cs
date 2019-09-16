using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public event Action<Room> OnCurrentRoomChanged = delegate {};
    [SerializeField] private Checkpoint initialCheckpoint = null;
    [SerializeField] private Checkpoint currentRoomCheckpoint = null;
    [SerializeField] private string currentRoom;
    [SerializeField] private string worldName;

    private Room curRoom = null;

    public void SetCurrentRoom(Room roomInfo)
    {
        currentRoomCheckpoint = roomInfo.RoomInitialCheckpoint;
        currentRoom = roomInfo.RoomName;

        if (curRoom != null)
            curRoom.Open();

        curRoom = roomInfo;
        curRoom.Close();
        OnCurrentRoomChanged(roomInfo);
    }

    public void ResetToRoomCheckpoint()
    {
        if (currentRoomCheckpoint != null)
            GameManager.Instance.SendPlayerToCheckpoint(currentRoomCheckpoint);
    }

    public void ResetToInitialCheckpoint()
    {
        if (initialCheckpoint != null)
            GameManager.Instance.SendPlayerToCheckpoint(initialCheckpoint);
    }

    public void ResetWorld()
    {
        GameManager.Instance.ResetWorld();
    }

    public Checkpoint GetInitialCheckpoint()
    {
        return initialCheckpoint;
    }
}
