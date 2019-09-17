using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class Progression
{
    public int amountOfJumps { get; private set; } = 0;
    public int amountOfDeaths { get; private set; } = 0;
    public List<Level> unlockedLevels { get; private set; } = new List<Level>();
    public int amountOfCollectables { get; private set; } = 0;
    public int amountOfFuelsGrabbed { get; private set; } = 0;
    public int amountOfBounces { get; private set; } = 0;
    public int amountOfCheckpointsActivated { get; private set; } = 0;
    private static string path = Application.dataPath + "/data.json";

    public void SaveProgression()
    {
        string json = JsonConvert.SerializeObject(this);
        File.WriteAllText(path, json);
    }

    public static Progression LoadProgression()
    {
        dynamic parsedData = JsonConvert.DeserializeObject(File.ReadAllText(path));
        if (parsedData == null)
        {
            return new Progression();
        }
        return new Progression
        {
            amountOfJumps = (int)parsedData["amountOfJumps"],
            amountOfDeaths = (int)parsedData["amountOfDeaths"],
            amountOfCollectables = (int)parsedData["amountOfCollectables"],
            amountOfBounces = (int)parsedData["amountOfBounces"],
            amountOfCheckpointsActivated = (int)parsedData["amountOfCheckpointsActivated"],
            amountOfFuelsGrabbed = (int)parsedData["amountOfFuelsGrabbed"],
            unlockedLevels = parsedData["unlockedLevels"] as List<Level>
        };
    }

    public void IncreaseAmountOfJumps()
    {
        amountOfJumps++;
    }

    public void IncreaseAmountOfDeaths()
    {
        amountOfDeaths++;
    }

    public void IncreaseAmountOfCollectables()
    {
        amountOfCollectables++;
    }

    public void IncreaseAmountOfFuelsGrabbed()
    {
        amountOfFuelsGrabbed++;
    }

    public void IncreaseAmountOfBounces()
    {
        amountOfBounces++;
    }

    public void IncreaseAmountCheckpointsActivated()
    {
        amountOfCheckpointsActivated++;
    }

    public void AddLevel(Level level)
    {
        if (unlockedLevels == null)
        {
            unlockedLevels = new List<Level>();
        }
        unlockedLevels.Add(level);
    }

    public void MarkLevelAsCompleted(string sceneName)
    {
        foreach (Level level in unlockedLevels)
        {
            if (level.sceneName == sceneName)
            {
                level.completed = true;
            }
        }
    }
}
