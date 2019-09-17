using UnityEngine;

public class Level
{
    public string sceneName;
    public string worldName;
    public MinifiedCheckpoint latestCheckpoint;
    public bool completed;
}

public class MinifiedCheckpoint
{
    public string name;
    public Vector3 position;
}
