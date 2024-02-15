using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Audio;

public class SoundManager : PersistentSingleton<SoundManager> {
    [SerializeField] private AssetReferenceT<AudioMixer> mixer;
    [SerializeField, Range(1, 64)] int maxSounds = 32;
    [SerializeField, Range(0f, 1f)] private float masterVolume = 1f;
    [SerializeField, Range(0f, 1f)] private float musicVolume = 1f;
    [SerializeField, Range(0f, 1f)] private float sfxVolume = 1f;


    private AudioMixer audioMixer = null;
    private AudioSource musicSource = null;
    private List<AudioSource> SFXSources = new List<AudioSource>();
    private CancellationTokenSource cancellationSource = new CancellationTokenSource();
    
    private AssetReferenceAudioClip currentSoundTrackRef = null;
    private Dictionary<AssetReferenceAudioClip, float> trackTimePairs = new Dictionary<AssetReferenceAudioClip, float>();

    private bool initialized;

    protected override async void Awake() {
        base.Awake();

        if (initialized) return;

        audioMixer = await AssetManager.Load(mixer);
        musicSource = transform.Find("Music").GetComponent<AudioSource>();
        Transform SFXParent = transform.Find("SFX");


        for (int i = 0; i < maxSounds; i++) {
            var obj = new GameObject("SFX " + (i + 1));
            obj.transform.parent = SFXParent;

            AudioSource source = obj.AddComponent<AudioSource>();

            var groups = audioMixer.FindMatchingGroups("SFX");
            if (groups.Length == 1)
                source.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0];
            else
                Debug.LogError("Keyword \"SFX\" must return only 1 group");

            source.gameObject.SetActive(false);

            SFXSources.Add(source);
        }

        initialized = true;
    }
    #region Music
    public void SetTrack(AudioClip track) {
        musicSource.clip = track;
    }

    public async void SwapTrack(AssetReferenceAudioClip trackReference, float fadeTime = 0) {
        if (string.IsNullOrEmpty(trackReference.AssetGUID)) 
            return;

        bool isPlaying = IsTrackPlaying && musicSource.clip;
        Task<AudioClip> trackLoadTask = AssetManager.Load(trackReference, AssetManager.PrintMode.ERROR);
        Task fadeOutTask = FadeOut(fadeTime);
        if (isPlaying)
            await Task.WhenAll(fadeOutTask, trackLoadTask);
        else
            await trackLoadTask;

        bool contains = currentSoundTrackRef != null && trackTimePairs.ContainsKey(currentSoundTrackRef);
        if (currentSoundTrackRef != null && !contains) {
            trackTimePairs.Add(currentSoundTrackRef, musicSource.time);
        } else if (contains)
            trackTimePairs[currentSoundTrackRef] = musicSource.time;

        if (!trackLoadTask.Result) {
            Debug.LogWarning("Track could not be found. Playing nothing.");
            return;
        } else {
            if(currentSoundTrackRef != null && currentSoundTrackRef != trackReference)
                AssetManager.Release(currentSoundTrackRef);
            musicSource.clip = trackLoadTask.Result;
            currentSoundTrackRef = trackReference;
            if (trackTimePairs.ContainsKey(currentSoundTrackRef))
                musicSource.time = trackTimePairs[currentSoundTrackRef];

            _ = FadeIn(musicVolume * masterVolume, fadeTime);
        }
    }

    public bool IsTrackPlaying { get { return Instance && musicSource.isPlaying; } }

    public async Task ChangeMusicVolume(float endVolume, float duration) {
        
        if (duration == 0) {
            musicSource.volume = endVolume;
            return;
        }
        var end = Time.time + duration;
        float startVolume = musicSource.volume;

        while (Time.time < end) {
            musicSource.volume = Mathf.Lerp(endVolume, startVolume, (end - Time.time) / duration);
            await Task.Yield();
        }
        musicSource.volume = endVolume;
    }
    public async Task FadeIn(float newVolume, float duration) {
        if (musicSource.isPlaying) {
            await ChangeMusicVolume(newVolume, duration);
        }
        musicSource.volume = 0;
        musicSource.Play();
        await ChangeMusicVolume(newVolume, duration);
    }
    public async Task FadeOut(float duration) {
        await ChangeMusicVolume(0f, duration);
        musicSource.Pause();
    }


    #endregion

    #region SFX
    public void PlaySound(AudioClip sound, Vector3 at, float volume = 1, float pitch = 1) {
        AudioSource source = GetAvailableSource();
        if (source) {
            source.gameObject.SetActive(true);
            float duration = Mathf.Abs(sound.length / pitch);
            source.clip = sound; source.pitch = pitch; source.volume = volume; source.transform.position = at;
            source.Play();
            DisableSource(source, duration);
        }
    }
    private async void DisableSource(AudioSource source, float delay) {
        var end = Time.time + delay;
        while(Time.time < end) {
            await Task.Yield();
        }
        source.gameObject.SetActive(false);
    }
    private AudioSource GetAvailableSource() {
        foreach (AudioSource source in SFXSources) {
            if (!source.isActiveAndEnabled || !source.isPlaying) {
                return source;
            }
        }
        return null;
    }
    #endregion
}
