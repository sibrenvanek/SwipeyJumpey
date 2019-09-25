using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSound : MonoBehaviour
{
    [SerializeField] private PlayerDeath playerDeath = null;
    private AudioSource audioSource;
 
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        playerDeath.OnDeath += PlaySound;
    }

    private void PlaySound()
    {
        audioSource.Play();
    }
}
