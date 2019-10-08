using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private float musicVolume = -9f;
    [SerializeField] private float sfxVolume = 0f;
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

    private void Start()
    {
        LoadAudioOptions();
    }

    private void LoadAudioOptions()
    {
        if (PlayerPrefs.HasKey(SettingsScript.MUSICPREF))
        {
            if (PlayerPrefs.GetInt(SettingsScript.MUSICPREF) == 1)
                EnableMusic();
            else
                MuteMusic();
        }

        if (PlayerPrefs.HasKey(SettingsScript.SOUNDEFFECTSPREF))
        {
            if (PlayerPrefs.GetInt(SettingsScript.SOUNDEFFECTSPREF) == 1)
                EnableSFX();
            else
                MuteSFX();
        }
    }

    public void StartMenuTrack()
    {
        if (audioSource.clip == menuTrack)
            return;

        audioSource.Stop();
        audioSource.clip = menuTrack;
        audioSource.Play();
    }

    public void EnableMusic()
    {
        audioMixer.SetFloat("musicVolume", musicVolume);
    }

    public void MuteMusic()
    {
        audioMixer.SetFloat("musicVolume", -80);
    }

    public void EnableSFX()
    {
        audioMixer.SetFloat("sfxVolume", sfxVolume);
    }

    public void MuteSFX()
    {
        audioMixer.SetFloat("sfxVolume", -80);
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
