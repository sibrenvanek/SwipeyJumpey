using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSound : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private AudioSource audioSource;

    private void Awake()
    {
        playerMovement = transform.root.GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        playerMovement.OnJump += PlaySound;
    }

    private void PlaySound()
    {
        audioSource.Play();
    }
}
