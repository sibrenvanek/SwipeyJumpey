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
    private static string path = Application.dataPath + "/data/data.json";

    public void SaveProgression()
    {
        string json = JsonConvert.SerializeObject(this);
        File.WriteAllText(path, json);
    }

    public override string ToString()
    {
        return string.Format("unlockedLevel: {0}\njumps: {1}\ndeaths: {2}\nfuels: {3}\ncheckpoints: {4}", unlockedLevels[0].ToString(), amountOfJumps, amountOfDeaths, amountOfFuelsGrabbed, amountOfCheckpointsActivated);
    }

    public static Progression LoadProgression()
    {
        string data;
        try
        {
            data = File.ReadAllText(path);
        }
        catch
        {
            return new Progression();
        }
        Progression progression = JsonConvert.DeserializeObject<Progression>(data);
        dynamic parsedData = JsonConvert.DeserializeObject(data);
        Progression newProgression = new Progression
        {
            amountOfJumps = (int)parsedData["amountOfJumps"],
            amountOfDeaths = (int)parsedData["amountOfDeaths"],
            amountOfCollectables = (int)parsedData["amountOfCollectables"],
            amountOfBounces = (int)parsedData["amountOfBounces"],
            amountOfCheckpointsActivated = (int)parsedData["amountOfCheckpointsActivated"],
            amountOfFuelsGrabbed = (int)parsedData["amountOfFuelsGrabbed"],
            unlockedLevels = progression.unlockedLevels
        };
        return newProgression;
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
            CreateListAndAddLevel(level);
        }
        else
        {
            AddLevelToList(level);
        }
    }

    private void CreateListAndAddLevel(Level level)
    {
        unlockedLevels = new List<Level>();
        unlockedLevels.Add(level);
    }

    private void AddLevelToList(Level level)
    {
        bool levelExistsInList = false;
        foreach (Level l in unlockedLevels)
        {
            if (level.sceneName == l.sceneName)
            {
                levelExistsInList = true;
            }
        }
        if (!levelExistsInList)
        {
            unlockedLevels.Add(level);
        }
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

    public void SetLastActivatedCheckpoint(string sceneName, MinifiedCheckpoint checkpoint)
    {
        foreach (Level level in unlockedLevels)
        {
            if (level.sceneName == sceneName)
            {
                level.latestCheckpoint = checkpoint;
            }
        }
    }

    public Level GetLevel(string sceneName)
    {
        foreach (Level level in unlockedLevels)
        {
            if (level.sceneName == sceneName)
            {
                return level;
            }
        }
        return null;
    }
}
