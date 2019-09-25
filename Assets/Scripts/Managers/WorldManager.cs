using System;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [SerializeField] private Checkpoint initialCheckpoint = null;
    [SerializeField] private Checkpoint currentRoomCheckpoint = null;
    [SerializeField] private string worldName = null;
    public event Action<Room> OnCurrentRoomChanged = delegate { };
    private Room curRoom = null;

    public void SetCurrentRoom(Room roomInfo)
    {
        currentRoomCheckpoint = roomInfo.RoomInitialCheckpoint;

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

    public Checkpoint GetInitialCheckpoint()
    {
        return initialCheckpoint;
    }
    
    public string GetWorldName(){
        return worldName;
    }
}
