using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Android;

public class Progression
{
    public List<Level> unlockedLevels { get; private set; } = new List<Level>();
    private static readonly string path = Application.persistentDataPath + "/data.json";

    public void SaveProgression()
    {
        if (ProgressionManager.CheckAndForcePermission(Permission.ExternalStorageWrite))
        {
            string json = JsonConvert.SerializeObject(this);
            File.WriteAllText(path, json);
        }
    }

    public static Progression LoadProgression()
    {
        if (ProgressionManager.CheckAndForcePermission(Permission.ExternalStorageRead))
        {
            if (System.IO.File.Exists(path))
            {
                string data = File.ReadAllText(path);
                Progression progression = JsonConvert.DeserializeObject<Progression>(data);
                return progression;
            }
        }
        return new Progression();
    }

    public void IncreaseAmountOfJumps(string sceneName)
    {
        GetLevel(sceneName).amountOfJumps++;
    }

    public void IncreaseAmountOfDeaths(string sceneName)
    {
        GetLevel(sceneName).amountOfDeaths++;
    }

    public void IncreaseAmountOfMainCollectables(string sceneName, MinifiedMainCollectable collectable)
    {
        Level level = GetLevel(sceneName);
        level.amountOfMainCollectables++;
        level.mainCollectables.Add(collectable);
    }

    public void IncreaseAmountOfSideCollectables(string sceneName)
    {
        GetLevel(sceneName).amountOfSideCollectables++;
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

    public List<MinifiedMainCollectable> GetMainCollectables(string sceneName)
    {
        return GetLevel(sceneName).mainCollectables;
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

    public void DeleteProgression()
    {
        unlockedLevels = new List<Level>();
        SaveProgression();
    }

    public List<Level> GetUnlockedLevels()
    {
        return unlockedLevels;
    }

    public bool CheckIfLevelExists(int sceneIndex)
    {
        Level level = unlockedLevels.Find(level => level.buildIndex == sceneIndex);
        if (level != null)
            return true;
        return false;
    }
}
