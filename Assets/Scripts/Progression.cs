using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class Progression
{
    public List<Level> unlockedLevels { get; private set; } = new List<Level>();
    public int currentLevelIndex { get; private set; } = 0;
    private static readonly string path = Application.persistentDataPath + "/data.json";

    public void SaveProgression()
    {
        string json = JsonConvert.SerializeObject(this);
        File.WriteAllText(path, json);
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
        return progression;
    }

    public void IncreaseAmountOfJumps(string sceneName)
    {
        GetLevel(sceneName).amountOfJumps++;
    }

    public void IncreaseAmountOfDeaths(string sceneName)
    {
        GetLevel(sceneName).amountOfDeaths++;
    }

    public void IncreaseAmountOfCollectables(string sceneName)
    {
        GetLevel(sceneName).amountOfCollectables++;
    }

    public void IncreaseAmountOfFuelsGrabbed(string sceneName)
    {
        GetLevel(sceneName).amountOfFuelsGrabbed++;
    }

    public void IncreaseAmountOfBounces(string sceneName)
    {
        GetLevel(sceneName).amountOfBounces++;
    }

    public void IncreaseAmountCheckpointsActivated(string sceneName)
    {
        GetLevel(sceneName).amountOfCheckpointsActivated++;
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

    public void ResetLevels()
    {
        foreach (Level level in unlockedLevels)
        {
            level.completed = false;
        }
    }

    public Level GetFirstUnfinishedLevel()
    {
        foreach (Level level in unlockedLevels)
        {
            if (level.completed == false)
            {
                return level;
            }
        }
        return null;
    }
}
