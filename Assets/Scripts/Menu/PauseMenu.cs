using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI = null;
    [SerializeField] private GameObject pauseButton = null;
    [SerializeField] private TextMeshProUGUI currentLevelPlaceholder = null;
    [SerializeField] private Room currentRoom = null;
    [SerializeField] private PlayerMovement playerMovement = null;
    [SerializeField] private PlayerManager playerManager = null;
    [SerializeField] private WorldManager worldManager = null;

    public void Start()
    {
        currentLevelPlaceholder.text = currentRoom.RoomName;
        worldManager.OnCurrentRoomChanged += ChangeLevelText;
    }

    public void Resume()
    {
        pauseButton.SetActive(true);
        pauseUI.transform.localScale = new Vector3(0, 0, 0);
        playerMovement.Enable();
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        pauseButton.SetActive(false);
        pauseUI.transform.localScale = new Vector3(1, 1, 1);
        playerMovement.CancelJump();
        playerMovement.Disable();
        Time.timeScale = 0f;
    }

    public void BackToMenu()
    {
        ProgressionManager.Instance.IncreaseAmountOfMainCollectables(playerManager.GetMinifiedMainCollectables());
        ProgressionManager.Instance.IncreaseAmountOfSideCollectables(playerManager.GetSidePickups());
        ProgressionManager.Instance.IncreaseAmountOfDeaths(playerManager.GetDeaths());
        Destroy(GameManager.Instance.gameObject);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        AudioManager.Instance.StartMenuTrack();
    }

    public void ChangeLevelText(Room newRoom)
    {
        currentRoom = newRoom;
        currentLevelPlaceholder.text = currentRoom.RoomName;
    }

    public void ResetWorld()
    {
        ProgressionManager.Instance.ResetSideCollectables();
        Resume();
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        LevelManager.Instance.LoadScene(activeSceneIndex, true, true, true);
    }

    public void ResetCheckpoint()
    {
        Resume();
        GameManager.Instance.SendPlayerToLastCheckpoint();
    }

    public void ToggleGodmode()
    {
        playerManager.SetGodMode(!playerManager.GetGodMode());
    }

    public void SetPlayerMovement(PlayerMovement newPlayerMovement)
    {
        playerMovement = newPlayerMovement;
    }

    public void SetPlayerManager(PlayerManager newPlayerManager)
    {
        playerManager = newPlayerManager;
    }

    public void DisablePauseMenu()
    {
        gameObject.SetActive(false);
    }

    public void EnablePauseMenu()
    {
        gameObject.SetActive(true);
    }
}
