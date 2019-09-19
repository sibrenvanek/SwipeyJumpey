using UnityEngine;

public class Level
{
    public string sceneName;
    public string worldName;
    public MinifiedCheckpoint latestCheckpoint;
    public bool completed;
    public override string ToString()
    {
        return string.Format("  sceneName: {0}\n  worldName: {1}\n  latestCheckpoint: {2}\n  completed: {3}", sceneName, worldName, latestCheckpoint.ToString(), completed);
    }
}

public class MinifiedCheckpoint
{
    public string name;
    public Vector3 position;
    public override string ToString()
    {
        return string.Format("\n    name: {0}\n    position: {1}", name, position.ToString());
    }
}
