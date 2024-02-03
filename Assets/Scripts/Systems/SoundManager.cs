using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Audio;

public class SoundManager : PersistentSingleton<SoundManager> {
    [SerializeField] private AssetReferenceT<AudioMixer> mixer;
    [SerializeField, Range(1, 64)] int maxSounds = 32;

    private AudioMixer audioMixer = null;

    private AudioSource musicSource = null;
    private List<AudioSource> SFXSources = new List<AudioSource>();

    protected override async void Awake() {
        base.Awake();
        audioMixer = await Utility.Load(mixer);

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
    }

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

    private async void DisableSource(AudioSource source, float duration) {
        var end = Time.time + duration;
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
}
