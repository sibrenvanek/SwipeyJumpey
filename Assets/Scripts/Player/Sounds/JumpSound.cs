using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSound : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement = null;
    private AudioSource audioSource;

    private void Awake()
    {
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
