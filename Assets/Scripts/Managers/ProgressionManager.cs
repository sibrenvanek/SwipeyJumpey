using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressionManager : MonoBehaviour
{
    public static ProgressionManager Instance;
    private Progression progression;
    [SerializeField] public bool UseProgression = true;

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
    }

    public void HandleProgression()
    {
        progression.MarkLevelAsCompleted(SceneManager.GetActiveScene().name);
        progression.SaveProgression();
    }

    public void IncreaseAmountOfJumps()
    {
        progression.IncreaseAmountOfJumps(SceneManager.GetActiveScene().name);
    }

    public void IncreaseAmountOfDeaths()
    {
        progression.IncreaseAmountOfDeaths(SceneManager.GetActiveScene().name);
    }

    public void IncreaseAmountOfFuelsGrabbed()
    {
        progression.IncreaseAmountOfFuelsGrabbed(SceneManager.GetActiveScene().name);
    }

    public void IncreaseAmountOfCheckpointsActivated()
    {
        progression.IncreaseAmountCheckpointsActivated(SceneManager.GetActiveScene().name);
    }

    public void SetLastActivatedCheckpoint(Checkpoint checkpoint)
    {
        progression.SetLastActivatedCheckpoint(SceneManager.GetActiveScene().name, new MinifiedCheckpoint { name = checkpoint.name, position = checkpoint.CheckpointTransform.position });
    }

    public void ResetLevels()
    {
        progression.ResetLevels();
    }

    public void AddLevel(Level level)
    {
        progression.AddLevel(level);
    }

    public Level GetLevel(string sceneName)
    {
        return progression.GetLevel(sceneName);
    }

    public Level GetLatestLevel()
    {
        return progression.GetFirstUnfinishedLevel();
    }

    public static Checkpoint GetCheckpointFromMinified(MinifiedCheckpoint minCheckpoint)
    {
        Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();
        foreach (Checkpoint checkpoint in checkpoints)
        {
            if (checkpoint.name == minCheckpoint.name)
            {
                return checkpoint;
            }
        }
        return null;
    }

    public void SaveProgression()
    {
        progression.SaveProgression();
    }
}
