using UnityEngine;
using UnityEngine.SceneManagement;

public class ThankYouStopButton : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
        Destroy(PlayerManager.Instance.gameObject);
        Destroy(GameManager.Instance.gameObject);
        Destroy(CanvasManager.Instance.gameObject);
    }
}
