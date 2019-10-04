using UnityEngine;

public class IntroRocket : MonoBehaviour
{
    [SerializeField] private GameObject onEngine = null;
    [SerializeField] private GameObject offEngine = null;
    [SerializeField] private GameObject explosion = null;

    public void TurnOnEngine()
    {
        explosion.SetActive(false);
        onEngine.SetActive(true);
        offEngine.SetActive(false);
    }

    public void TurnOffEngine()
    {
        onEngine.SetActive(false);
        offEngine.SetActive(true);
    }

    public void Explosion()
    {
        explosion.SetActive(true);
    }

    public void FinishIntro()
    {
        LevelManager.Instance.LoadNextScene(false);
    }
}
