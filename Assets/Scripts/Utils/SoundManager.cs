using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// The SoundManager is a useful class that can control background music and
// sound effects in a scene. This script is a variation of a script I've used
// in previous projects - it is simplified to fit the needs of this project.
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private bool fadeInMusic;
    [SerializeField] private float fadeInTime;
    [SerializeField] private float musicVolumeMax;

    private List<AudioSource> effectSources = new List<AudioSource>();

    private void Awake()
    {
        if (fadeInMusic)
        {
            StartCoroutine(FadeInMusic(fadeInTime));
        }
    }

    public void PlayClip(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("Specified AudioClip was null. Cannot play this sound.");
            return;
        }

        // The SoundManager can play any number of sound effects simultaneously.
        // It keeps track of AudioSource components that have been added and if
        // there is one that isn't currently playing, that one is used to play
        // the new effect. If none are available, it creates a new AudioSource.
        AudioSource nextSource = null;
        foreach (AudioSource existingSource in effectSources)
        {
            if (!existingSource.isPlaying)
            {
                nextSource = existingSource;

                // Reset playback time
                nextSource.time = 0;
                nextSource.timeSamples = 0;
                break;
            }
        }

        if (nextSource == null)
        {
            nextSource = gameObject.AddComponent<AudioSource>();
            effectSources.Add(nextSource);
        }

        nextSource.clip = clip;
        nextSource.Play();
    }

    public IEnumerator FadeInMusic(float fadeTime)
    {
        musicSource.volume = 0;
        musicSource.Play();

        while (musicSource.volume < musicVolumeMax)
        {
            musicSource.volume +=  Time.deltaTime / fadeTime;

            yield return null;
        }
    }

    public IEnumerator FadeOutMusic(float fadeTime)
    {
        float startVolume = musicSource.volume;

        while (musicSource.volume > 0)
        {
            musicSource.volume -= startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }
    }
}