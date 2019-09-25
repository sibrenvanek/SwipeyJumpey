using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSound : MonoBehaviour
{
    [SerializeField] private Fuel fuel = null;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        fuel.OnPickUp += PlaySound;
    }

    private void PlaySound()
    {
        audioSource.Play();
    }
}
