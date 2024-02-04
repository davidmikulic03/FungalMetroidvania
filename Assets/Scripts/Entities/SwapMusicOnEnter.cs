using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapMusicOnEnter : MonoBehaviour
{
    [SerializeField] AssetReferenceAudioClip track1;
    [SerializeField] AssetReferenceAudioClip track2;
    [SerializeField] float fadeTime = 1f;

    private void OnTriggerEnter2D(Collider2D other) {
        if (SoundManager.Instance)
            SoundManager.Instance.SwapTrack(track1, fadeTime / 2);
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if(SoundManager.Instance)
            SoundManager.Instance.SwapTrack(track2, fadeTime / 2);
    }
}
