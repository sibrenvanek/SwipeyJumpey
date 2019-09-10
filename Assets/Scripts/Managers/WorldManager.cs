using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WorldManager : MonoBehaviour
{
    [SerializeField] private Checkpoint initialCheckpoint = null;
    [SerializeField] private Checkpoint currentRoomCheckpoint = null;
    [SerializeField] private string currentRoom;
    [SerializeField] private string worldName;
    public event Action<Room> OnCurrentRoomChanged = delegate{};
    public void SetCurrentRoom(Room roomInfo)
    {
        currentRoomCheckpoint = roomInfo.RoomInitialCheckpoint;
        currentRoom = roomInfo.RoomName;
        OnCurrentRoomChanged(roomInfo);
    }

    public void ResetToRoomCheckpoint()
    {
        if(currentRoomCheckpoint != null)
            GameManager.Instance.SendPlayerToCheckpoint(currentRoomCheckpoint);
    }

    public void ResetToInitialCheckpoint()
    {
        if(initialCheckpoint != null)
            GameManager.Instance.SendPlayerToCheckpoint(initialCheckpoint);
    }

    public void ResetWorld()
    {
        GameManager.Instance.ResetWorld();
    }
}
