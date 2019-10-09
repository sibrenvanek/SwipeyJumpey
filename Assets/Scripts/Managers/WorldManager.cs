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
    private SideCollectable[] sideCollectables = null;

    void Start()
    {
        DisableMainCollectables();
        DisableSideCollectables();
    }

    private void DisableMainCollectables()
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

    private void DisableSideCollectables()
    {
        sideCollectables = FindObjectsOfType<SideCollectable>();
        List<int> collectables = ProgressionManager.Instance.GetSideCollectables();
        foreach (SideCollectable sideCollectable in sideCollectables)
        {
            if (collectables.Exists(collectableId => collectableId == sideCollectable.GetId()))
            {
                sideCollectable.SetCollected();
            }
        }

        PlayerManager playerManager = FindObjectOfType<PlayerManager>();
        playerManager.SetSidePickups(collectables);
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

    public Checkpoint GetInitialCheckpoint()
    {
        return initialCheckpoint;
    }

    public string GetWorldName()
    {
        return worldName;
    }
}
