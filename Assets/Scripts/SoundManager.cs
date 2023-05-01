using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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