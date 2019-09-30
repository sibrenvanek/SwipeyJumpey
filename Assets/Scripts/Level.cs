using System.Collections.Generic;
using UnityEngine;

public class Level
{
    public string sceneName;
    public string worldName;
    public MinifiedCheckpoint latestCheckpoint;
    public int amountOfJumps = 0;
    public int amountOfDeaths = 0;
    public int amountOfMainCollectables = 0;
    public List<MainCollectable> mainCollectables = new List<MainCollectable>();
    public int amountOfSideCollectables = 0;
    public int amountOfFuelsGrabbed = 0;
    public int amountOfCheckpointsActivated = 0;
    public int amountOfBounces = 0;
    public bool completed;
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

public class MainCollectable
{
    public string name;
    public Vector3 position;
}
