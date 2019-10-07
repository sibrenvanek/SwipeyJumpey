using System.Collections.Generic;
using UnityEngine;

public class Level
{
    public string sceneName;
    public int buildIndex;
    public string levelName;
    public string worldName;
    public MinifiedCheckpoint latestCheckpoint;
    public int amountOfJumps = 0;
    public int amountOfDeaths = 0;
    public int amountOfMainCollectables = 0;
    public List<MinifiedMainCollectable> mainCollectables = new List<MinifiedMainCollectable>();
    public int amountOfSideCollectables = 0;
    public int amountOfFuelsGrabbed = 0;
    public int amountOfCheckpointsActivated = 0;
    public int amountOfBounces = 0;
    public bool completed;
    public int totalAmountOfMainCollectables = 0;
    public int totalAmountOfSideCollectables = 0;
}

public class MinifiedCheckpoint
{
    public int id;
    public string name;
    public Vector3 position;
    public override string ToString()
    {
        return string.Format("\n    name: {0}\n    position: {1}", name, position.ToString());
    }
}

public class MinifiedMainCollectable
{
    public int id;
    public string name;
    public Vector3 position;
}
