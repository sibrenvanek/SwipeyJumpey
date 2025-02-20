using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class ProgressionManager : MonoBehaviour
{
    public static ProgressionManager Instance;

    private Progression progression;
    private readonly int ID = 17;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        progression = Progression.LoadProgression();

        if (progression.ID != ID)
        {
            DeleteProgression();
            progression = Progression.CreateProgression(ID);
        }
    }

    public void HandleProgression()
    {
        progression.MarkLevelAsCompleted(SceneManager.GetActiveScene().name);
        progression.SaveProgression();
    }

    public void SetIntroPlayed()
    {
        progression.introPlayed = true;
    }

    public bool GetIntroPlayed()
    {
        return progression.introPlayed;
    }

    public void SetDisplayedTutorial()
    {
        progression.SetDisplayedTutorial();
    }

    public bool GetDisplayedTutorial()
    {
        return progression.GetDisplayedTutorial();
    }

    public void IncreaseAmountOfDeaths(int deaths = 1)
    {
        progression.IncreaseAmountOfDeaths(SceneManager.GetActiveScene().name, deaths);
    }

    public void IncreaseAmountOfMainCollectables(MinifiedMainCollectable[] collectables)
    {
        progression.IncreaseAmountOfMainCollectables(SceneManager.GetActiveScene().name, collectables);
    }

    public List<MinifiedMainCollectable> GetMainCollectables()
    {
        return progression.GetMainCollectables(SceneManager.GetActiveScene().name);
    }

    public void IncreaseAmountOfSideCollectables(int amount = 1)
    {
        progression.IncreaseAmountOfSideCollectables(SceneManager.GetActiveScene().name, amount);
    }

    public void SetLastActivatedCheckpoint(Checkpoint checkpoint)
    {
        progression.SetLastActivatedCheckpoint(SceneManager.GetActiveScene().name, new MinifiedCheckpoint { id = checkpoint.GetId(), name = checkpoint.name, position = checkpoint.CheckpointTransform.position });
    }

    public void ResetCheckpoints()
    {
        progression.ResetCheckpoints();
    }

    public void ResetLevels()
    {
        progression.RemoveLevelsProgression();
    }

    public Level GetLevel(string sceneName)
    {
        return progression.GetLevel(sceneName);
    }

    public Level GetLevel(int sceneIndex)
    {
        return progression.GetLevel(GetSceneNameFromIndex(sceneIndex));
    }

    public Level GetFirstUnfinishedLevel()
    {
        return progression.GetFirstUnfinishedLevel();
    }

    public void ResetLevel(Level level)
    {
        progression.RemoveLevelProgression(level);
    }

    public void SetLatestLevel(Level level)
    {
        if (level != null)
        {
            progression.latestLevel = level;
        }
    }

    public static string GetSceneNameFromIndex(int sceneIndex)
    {
        return Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(sceneIndex));
    }

    public Level GetLatestLevel()
    {
        return progression.latestLevel;
    }

    public static Checkpoint GetCheckpointFromMinified(MinifiedCheckpoint minCheckpoint)
    {
        if (minCheckpoint == null)
            return null;

        Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();
        return Array.Find(checkpoints, checkpoint => checkpoint.GetId() == minCheckpoint.id);
    }

    public static bool CheckAndForcePermission(string permission)
    {
        while (!Permission.HasUserAuthorizedPermission(permission))
        {
            Permission.RequestUserPermission(permission);
        }
        return true;
    }

    public void SaveProgression()
    {
        progression.SaveProgression();
    }

    public void DeleteProgression()
    {
        progression.DeleteProgression();
        progression = Progression.CreateProgression(ID);
    }

    public bool CheckIfLevelExists(int sceneIndex)
    {
        return progression.CheckIfLevelExists(sceneIndex);
    }

    public void SetLevelUnlocked(Level level)
    {
        progression.SetLevelUnlocked(level);
    }

    public bool GetPickedUpJetpack()
    {
        return progression.pickedUpJetpack;
    }

    public void SetPickedUpJetpack(bool value)
    {
        progression.SetPickedUpJetpack(value);
    }

    public void ResetSideCollectables()
    {
        progression.ResetSideCollectables(SceneManager.GetActiveScene().name);
    }

    public void ResetSideCollectablesAll(){
        progression.ResetSideCollectablesAll();
    }
}
