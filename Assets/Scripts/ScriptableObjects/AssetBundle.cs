using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "AssetBundle", menuName = "Bundles/AssetBundle")]
public class AssetBundle : ScriptableObject {
    public List<BundleEntry> entries; 
    public AssetReference Find(string key)
    {
        for(int i = 0; i < entries.Count; i++) {
            if (entries[i].key == key)
                return entries[i].asset;
        }
        Debug.LogError("Could not find asset with key " + key);
        return null;
    }
}

[Serializable]
public struct BundleEntry {
    public AssetReference asset;
    public string key;
}