using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;
using UnityEngine.ResourceManagement.AsyncOperations;
using static AssetManager;

[Serializable]
public class AssetReferenceAudioClip : AssetReferenceT<AudioClip> {
    public AssetReferenceAudioClip(string guid) : base(guid) {
    }
}

public class AssetManager
{
    public enum PrintMode {
        ALL = 0,
        ERROR,
        NONE
    }

    public static readonly Dictionary<object, AsyncOperationHandle> LoadedAssets = new Dictionary<object, AsyncOperationHandle>();
    public static readonly Dictionary<object, AsyncOperationHandle> LoadingAssets = new Dictionary<object, AsyncOperationHandle>();

    private static long loadingQueue = 0;
    public static bool Loading { get { return loadingQueue > 0; } }
    public static long ItemsLoading { get { return loadingQueue; } }


    public static async Task<T> Load<T>(string key, PrintMode printmode = PrintMode.ALL) where T : UnityEngine.Object {
        if (string.IsNullOrEmpty(key)) {
            if ((printmode == PrintMode.ALL || printmode == PrintMode.ERROR))
                Debug.LogError("Could not load asset because key was null");
            return null;
        }

        AsyncOperationHandle<T> handle;
        if (LoadedAssets.ContainsKey(key)) {
            handle = LoadedAssets[key].Convert<T>();
            return await handle.Task;
        }
        if (LoadingAssets.ContainsKey(key)) {
            handle = LoadingAssets[key].Convert<T>();
            return await handle.Task;
        }

        handle = Addressables.LoadAssetAsync<T>(key);
        LoadingAssets.Add(key, handle);

        T obj = await handle.Task;

        LoadingAssets.Remove(key);
        LoadedAssets.Add(key, handle);
        Debug.Log(LoadedAssets.Count + " assets currently loaded into memory");

        if (obj != null && printmode == PrintMode.ALL)
            Debug.Log("Successfully loaded asset " + obj);
        else if(obj == null && (printmode == PrintMode.ALL || printmode == PrintMode.ERROR))
            Debug.LogWarning("Asset " + key + " loaded as null");
        return obj;
    }
    public static async Task<T> Load<T>(AssetReferenceT<T> asset, PrintMode printmode = PrintMode.ALL) where T : UnityEngine.Object {
        return await Load<T>(asset.AssetGUID, printmode);
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

    public static async Task DownloadDependencies(AssetReference asset) {
        await Addressables.DownloadDependenciesAsync(asset).Task;
    }

    public static void Release<T>(string key) where T : UnityEngine.Object {
        if (IsLoaded(key)) {
            Addressables.Release(LoadedAssets[key]);
            LoadedAssets.Remove(key);
            Debug.Log(LoadedAssets.Count + " assets currently loaded into memory");
        } else if (IsLoading(key)) {
            Addressables.Release(LoadingAssets[key]);
            LoadingAssets.Remove(key);
        }
    }
    public static void Release<T>(AssetReferenceT<T> assetReference) where T : UnityEngine.Object {
        Release<T>(assetReference.AssetGUID);
    }

    public static bool IsLoaded(string key) {
        return LoadedAssets.ContainsKey(key);
    }
    public static bool IsLoaded(AssetReference aRef) {
        return LoadedAssets.ContainsKey(aRef.AssetGUID);
    }
    public static bool IsLoading(string key) {
        return LoadingAssets.ContainsKey(key);
    }
    public static bool IsLoading(AssetReference aRef) {
        return LoadingAssets.ContainsKey(aRef.AssetGUID);
    }
}
