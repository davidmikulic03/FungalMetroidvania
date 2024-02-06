using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SwapMusicOnEnter : MonoBehaviour
{
    [SerializeField] AssetReferenceAudioClip track1;
    [SerializeField] AssetReferenceAudioClip track2;
    [SerializeField] AssetReferenceSprite big;
    [SerializeField] float fadeTime = 1f;

    Sprite sprite;

    GameObject obj;

    private async void OnTriggerEnter2D(Collider2D other) {
        if (SoundManager.Instance)
            SoundManager.Instance.SwapTrack(track1, fadeTime / 2);

        sprite = await Utility.Load(big);

        obj = new GameObject(sprite.name);
        obj.AddComponent<SpriteRenderer>().sprite = sprite;
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if(SoundManager.Instance)
            SoundManager.Instance.SwapTrack(track2, fadeTime / 2);

        Destroy(obj);
        Utility.Release(big);
    }
}
