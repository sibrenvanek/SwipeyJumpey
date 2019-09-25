using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSound : MonoBehaviour
{
    private PlayerDeath playerDeath;
    private AudioSource audioSource;
 
    private void Awake()
    {
        playerDeath = transform.root.GetComponent<PlayerDeath>();
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
