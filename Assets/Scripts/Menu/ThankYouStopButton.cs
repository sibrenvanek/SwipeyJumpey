using UnityEngine;
using UnityEngine.SceneManagement;

public class ThankYouStopButton : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
