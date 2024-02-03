using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;
using static Utility;

[Serializable]
public class AssetReferenceAudioClip : AssetReferenceT<AudioClip> {
    public AssetReferenceAudioClip(string guid) : base(guid) {
    }
}

public class Utility
{
    public enum PrintMode {
        ALL = 0,
        ERROR,
        NONE
    }

    private static long loadingQueue = 0;
    public static bool Loading { get { return loadingQueue > 0; } }
    public static long ItemsLoading { get { return loadingQueue; } }


    private static async Task<T> Load<T>(string key, PrintMode printmode = PrintMode.ALL) where T : UnityEngine.Object {
        if (string.IsNullOrEmpty(key)) {
            if ((printmode == PrintMode.ALL || printmode == PrintMode.ERROR))
                Debug.LogError("Could not load asset because key was null");
            return null;
        }
        T obj = await Addressables.LoadAssetAsync<T>(key).Task;

        if (obj != null && printmode == PrintMode.ALL)
            Debug.Log("Successfully loaded asset " + obj);
        else if(obj == null && (printmode == PrintMode.ALL || printmode == PrintMode.ERROR))
            Debug.LogWarning("Asset " + key + " loaded as null");
        return obj;
    }
    public static async Task<T> Load<T>(AssetReferenceT<T> asset, PrintMode printmode = PrintMode.ALL) where T : UnityEngine.Object {
        return await Load<T>(asset.AssetGUID, printmode);
    }
    public static async Task<List<T>> Load<T>(AssetLabelReference label, Action<T> callback = null) where T : UnityEngine.Object {
        if (string.IsNullOrEmpty(label.labelString)) {
            Debug.LogError("Could not load assets because label was null or empty");
            return null;
        }
        List<T> output = await Addressables.LoadAssetsAsync<T>(label, callback).Task as List<T>;
        long successCount = 0;
        long failureCount = 0;
        foreach (T obj in output) { 
            if(obj != null)
                successCount++;
            else 
                failureCount++;
        }
        if (successCount > 0)
            Debug.Log("Successfully loaded " + successCount + " " + typeof(T).Name + "(s) from label " + label.labelString);
        if (failureCount > 0)
            Debug.LogError("Failed to load " + failureCount + " " + typeof(T).Name + "(s) from label " + label.labelString);
        return output;
    }


    public static async Task<List<T>> LoadFromList<T>(AssetReferenceT<T>[] assets) where T : UnityEngine.Object {
        List<Task<T>> tasks = new List<Task<T>>();
        for (int i = 0; i < assets.Length; i++)
            tasks.Add(Load(assets[i], PrintMode.NONE));
        await Task.WhenAll(tasks);
        long successCount = 0; 
        long failureCount = 0;
        List<T> output = new List<T>();
        for (int i = 0; i < tasks.Count; i++) {
            if (tasks[i].Result != null)
                successCount++;
            else
                failureCount++;
            output.Add(tasks[i].Result);
        }
        if (successCount > 0) 
            Debug.Log("Successfully loaded " + successCount + typeof(T).Name + "(s)");
        if (failureCount > 0)
            Debug.LogError("Failed to load " + failureCount + typeof(T).Name + "(s)");
        return output;
    }


    public static async Task<GameObject> LoadInstance(AssetReferenceGameObject prefab, PrintMode printmode = PrintMode.ALL) {
        if (string.IsNullOrEmpty(prefab.AssetGUID)) {
            if ((printmode == PrintMode.ALL || printmode == PrintMode.ERROR))
                Debug.LogError("Could not load asset because reference was null");
            return null;
        }
        GameObject obj = await Addressables.InstantiateAsync(prefab).Task;

        if (obj != null && printmode == PrintMode.ALL)
            Debug.Log("Successfully loaded and instantiated asset " + obj);
        else if (obj == null && (printmode == PrintMode.ALL || printmode == PrintMode.ERROR))
            Debug.LogWarning(prefab + " loaded as null");
        return obj;
    }
    public static async Task<List<GameObject>> LoadInstance(AssetLabelReference label, Action<GameObject> callback = null) {
        List<GameObject> results = await Load<GameObject>(label, callback);
        for (int i = 0; i < results.Count; i++) {
            if (results[i])
                results[i] = GameObject.Instantiate(results[i]);
        }
        return results;
    }

    public static async Task DownloadDependencies(AssetReference asset) {
        await Addressables.DownloadDependenciesAsync(asset).Task;
    }

    public static void Release<T>(T obj) where T : UnityEngine.Object {

    }


    /*Addressables.InstantiateAsync(asset).Completed += handle => {
        if(handle.Status == AsyncOperationStatus.Succeeded) {
            GameObject.Instantiate((GameObject)asset.OperationHandle.Result);
            Debug.Log("Successfully loaded and instantiated asset " + handle.Result);
        } else {
            Debug.LogError("Failed to load asset " + asset);
        }
    };*/

    /*public static async Task<T> LoadAsync<T>(AssetReference asset) {
        AsyncOperationHandle handle = Addressables.LoadAssetAsync<T>(asset);
        await handle.Task;
        if (handle.Status == AsyncOperationStatus.Succeeded)
            Debug.Log("Successfully loaded asset " + handle.Result);
        else
            Debug.LogError("Failed to load asset " + asset);

        return (T)handle.Result;
    }*/
}
