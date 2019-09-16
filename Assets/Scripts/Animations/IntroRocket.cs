using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroRocket : MonoBehaviour
{
    [SerializeField] private GameObject onEngine;
    [SerializeField] private GameObject offEngine;
    [SerializeField] private GameObject explosion;

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
}