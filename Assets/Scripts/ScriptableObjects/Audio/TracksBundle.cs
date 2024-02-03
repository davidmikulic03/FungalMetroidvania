using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;

[Serializable]
public struct Soundtrack {
    public AssetReferenceAudioClip audioReference;
    public string key;
}

[CreateAssetMenu(fileName = "TracksBundle", menuName = "Bundles/TracksBundle")]
public class TracksBundle : ScriptableObject {
    public List<Soundtrack> entries; 
    public AssetReference Find(string key)
    {
        for (int i = 0; i < entries.Count; i++) {
            if (entries[i].key == key)
                return entries[i].audioReference;
        }
        Debug.LogError("Could not find asset with key " + key);
        return null;
    }
}