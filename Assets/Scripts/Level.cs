using UnityEngine;

public class Level
{
    public string sceneName;
    public string worldName;
    public MinifiedCheckpoint latestCheckpoint;
    public int amountOfJumps = 0;
    public int amountOfDeaths = 0;
    public int amountOfCollectables = 0;
    public int amountOfFuelsGrabbed = 0;
    public int amountOfCheckpointsActivated = 0;
    public int amountOfBounces = 0;

    public bool completed;
    public override string ToString()
    {
        return string.Format("sceneName: {0}\nworldName: {1}\nlatestCheckpoint: {2}\ncompleted: {3}\njumps: {4}", sceneName, worldName, latestCheckpoint.ToString(), completed, amountOfJumps);
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
