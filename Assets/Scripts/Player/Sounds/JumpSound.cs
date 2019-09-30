using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSound : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement = null;

    [Header("Pitch")]
    [SerializeField] private float minPitch = 0.8f;
    [SerializeField] private float maxPitch = 1.3f;

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
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.Play();
    }
}
