using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSound : MonoBehaviour
{
    [SerializeField] private Checkpoint checkpoint = null;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        checkpoint.OnActivate += PlaySound;
    }

    private void PlaySound()
    {
        audioSource.Play();
    }
}
