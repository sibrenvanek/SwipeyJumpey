using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [SerializeField] private Checkpoint initialCheckpoint = null;
    [SerializeField] private Checkpoint currentRoomCheckpoint = null;
    [SerializeField] private string worldName = null;
    public event Action<Room> OnCurrentRoomChanged = delegate {};
    private Room curRoom = null;
    private MainCollectable[] mainCollectables = null;

    void Start()
    {
        mainCollectables = FindObjectsOfType<MainCollectable>();
        List<MinifiedMainCollectable> collectables = ProgressionManager.Instance.GetMainCollectables();
        foreach (MainCollectable mainCollectable in mainCollectables)
        {
            if (collectables.Exists(collectable => collectable.id == mainCollectable.GetId()))
            {
                mainCollectable.SetCollected();
            }
        }
    }

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
        Checkpoint lastCheckpoint = ProgressionManager.Instance.GetLastActivatedCheckpoint();
        if (lastCheckpoint != null)
            GameManager.Instance.SendPlayerToCheckpoint(lastCheckpoint);
        else
            Debug.Log("AW");
    }

    public Checkpoint GetInitialCheckpoint()
    {
        return initialCheckpoint;
    }

    public string GetWorldName()
    {
        return worldName;
    }
}
