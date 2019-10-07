using System.Collections.Generic;
using UnityEngine;

public class Level
{
    public string sceneName;
    public int buildIndex;
    public string levelName;
    public int totalAmountOfMainCollectables = 0;
    public int totalAmountOfSideCollectables = 0;

    public List<MinifiedMainCollectable> mainCollectables = new List<MinifiedMainCollectable>();
    public MinifiedCheckpoint latestCheckpoint;
    public int amountOfMainCollectables = 0;
    public int amountOfSideCollectables = 0;
    public int amountOfJumps = 0;
    public int amountOfDeaths = 0;
    public bool completed;

    public void ResetSideCollectables()
    {
        amountOfSideCollectables = 0;
    }
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
