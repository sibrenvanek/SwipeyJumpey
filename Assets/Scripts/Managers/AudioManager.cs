using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioClip menuTrack = null;
    [SerializeField] private AudioClip ingameTrack = null;
    [SerializeField] private AudioMixer audioMixer = null;
    private float defaultPitch = 1f;
    private AudioSource audioSource = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        defaultPitch = audioSource.pitch;
    }

    public void StartMenuTrack()
    {
        if (audioSource.clip == menuTrack)
            return;

        audioSource.Stop();
        audioSource.clip = menuTrack;
        audioSource.Play();
    }

    public void StartIngameTrack()
    {
        if (audioSource.clip == ingameTrack)
            return;

        audioSource.Stop();
        audioSource.clip = ingameTrack;
        audioSource.Play();
    }

    public void ReduceAudioPitch(float minus)
    {
        float pitchChange = minus * Time.deltaTime;

        float audioMixerPitch = audioMixer.GetFloat("mixerPitch", out audioMixerPitch) ? audioMixerPitch : 0f;

        audioMixer.SetFloat("mixerPitch", audioMixerPitch + pitchChange);
        audioSource.pitch -= pitchChange;
    }

    public void ResetAudioPitch()
    {
        audioMixer.SetFloat("mixerPitch", 1f);
        audioSource.pitch = defaultPitch;
    }
}
