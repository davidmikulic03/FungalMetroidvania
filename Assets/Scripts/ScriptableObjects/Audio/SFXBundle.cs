using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;

[Serializable]
public class SFX {
    public AssetReferenceAudioClip audioReference;
    public string key;
    [Tooltip("Represents the minimum volume")]
    [Range(0f, 1f)] public float randomizeVolume = 0.5f;
    [Range(0f, 2f)] public float minPitch = 1f;
    [Range(0f, 2f)] public float maxPitch = 1f;
}
[CreateAssetMenu(fileName = "SFXBundle", menuName = "Bundles/SFXBundle")]
public class SFXBundle : ScriptableObject {
    public List<SFX> entries;
    public SFX Find(string key) {
        for (int i = 0; i < entries.Count; i++) {
            if (entries[i].key == key)
                return entries[i];
        }
        Debug.LogError("Could not find asset with key " + key);
        return null;
    }
}