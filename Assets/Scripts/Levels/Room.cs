using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private string roomName = "LevelName";
    [SerializeField] private Checkpoint roomInitialCheckpoint = null;
    [SerializeField] private WorldManager worldManager = null;
    [SerializeField] private RoomEntrance[] entrances = null;
    public string RoomName { get { return roomName; } set { roomName = value; } }
    public Checkpoint RoomInitialCheckpoint { get { return roomInitialCheckpoint; } private set { roomInitialCheckpoint = value; } }

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
