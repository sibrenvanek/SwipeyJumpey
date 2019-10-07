using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Android;

public class Progression
{
    private static readonly string path = Application.persistentDataPath + "/data.json";
    private static readonly string levelsPath = Application.dataPath + "/data/levels.json";
    public List<Level> levels { get; private set; } = new List<Level>();
    public bool pickedUpJetpack { get; private set; } = false;
    public bool displayedTutorial = false;
    public bool introPlayed = false;
    public Level latestLevel = null;
    public int ID = 0;

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
#if UNITY_EDITOR
        return LoadFile();
#else
        if (ProgressionManager.CheckAndForcePermission(Permission.ExternalStorageRead))
        {
            return LoadFile();
        }

        Debug.LogError("No permission");
        return null;
#endif
    }

    private static Progression LoadFile()
    {
        Progression progression = new Progression();
        if (File.Exists(path))
        {
            string data = File.ReadAllText(path);
            if (data != "")
            {
                progression = JsonConvert.DeserializeObject<Progression>(data);
                progression.CheckLevels();
                return progression;
            }
        }

        progression.LoadLevels();

        return progression;
    }

    private void CheckLevels()
    {
        string json = File.ReadAllText(levelsPath);

        if (json != null)
        {
            List<Level> newLevels = JsonConvert.DeserializeObject<List<Level>>(json);

            for (int i = 0; i < newLevels.Count; i++)
            {
                if (newLevels[i].sceneName != levels[i].sceneName)
                {
                    DeleteProgression();
                }
                else
                {
                    levels[i].levelName = newLevels[i].levelName;
                    levels[i].totalAmountOfMainCollectables = newLevels[i].totalAmountOfMainCollectables;
                    levels[i].totalAmountOfSideCollectables = newLevels[i].totalAmountOfSideCollectables;
                    levels[i].buildIndex = newLevels[i].buildIndex;
                }
            }
        }
    }

    private void LoadLevels()
    {
        string json = File.ReadAllText(levelsPath);

        if (json != null)
        {
            levels = JsonConvert.DeserializeObject<List<Level>>(json);
            levels[0].unlocked = true;
        }
    }

    public bool GetDisplayedTutorial()
    {
        return displayedTutorial;
    }

    public void SetDisplayedTutorial()
    {
        displayedTutorial = true;
    }

    public void IncreaseAmountOfDeaths(string sceneName, int deaths)
    {
        GetLevel(sceneName).amountOfDeaths += deaths;
    }

    public void IncreaseAmountOfMainCollectables(string sceneName, MinifiedMainCollectable[] collectables)
    {
        Level level = GetLevel(sceneName);

        foreach (MinifiedMainCollectable collectable in collectables)
        {
            level.amountOfMainCollectables++;
            level.mainCollectables.Add(collectable);
        }
    }

    public void IncreaseAmountOfSideCollectables(string sceneName, int amount)
    {
        GetLevel(sceneName).amountOfSideCollectables += amount;
    }

    public void MarkLevelAsCompleted(string sceneName)
    {
        foreach (Level level in levels)
        {
            if (level.sceneName == sceneName)
            {
                level.completed = true;
            }
        }
    }

    public void SetLastActivatedCheckpoint(string sceneName, MinifiedCheckpoint checkpoint)
    {
        foreach (Level level in levels)
        {
            if (level.sceneName == sceneName)
            {
                level.latestCheckpoint = checkpoint;
            }
        }
    }

    public Level GetLevel(string sceneName)
    {
        foreach (Level level in levels)
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

    public void ResetCheckpoints()
    {
        foreach (Level level in levels)
        {
            level.latestCheckpoint = null;
        }
    }

    public void RemoveLevelsProgression()
    {
        foreach (Level level in levels)
        {
            level.latestCheckpoint = null;
            level.completed = false;
        }
    }
    public void RemoveLevelProgression(Level level)
    {
        level.latestCheckpoint = null;
        level.completed = false;
    }

    public Level GetFirstUnfinishedLevel()
    {
        foreach (Level level in levels)
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
        if (ProgressionManager.CheckAndForcePermission(Permission.ExternalStorageRead))
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }

    public bool CheckIfLevelExists(int sceneIndex)
    {
        Level level = levels.Find(unlockedLevel => unlockedLevel.buildIndex == sceneIndex);
        if (level != null)
            return true;
        return false;
    }

    public void SetPickedUpJetpack(bool value)
    {
        pickedUpJetpack = value;
    }

    public void SetLevelUnlocked(Level level)
    {
        foreach (Level existingLevel in levels)
        {
            if (existingLevel.sceneName == level.sceneName)
            {
                existingLevel.unlocked = true;
                return;
            }
        }
    }

    public void ResetSideCollectables(string sceneName)
    {
        Level level = GetLevel(sceneName);
        level.ResetSideCollectables();
    }

    public void ResetSideCollectablesAll()
    {
        levels.ForEach(level => level.ResetSideCollectables());
    }
}
