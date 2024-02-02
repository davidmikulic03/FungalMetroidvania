using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Utility
{
    private AssetReference _assetReference;
    private AssetReferenceGameObject _assetReferenceGameObject;
    private AssetReferenceSprite _assetReferenceSprite;
    private AssetReferenceTexture2D _assetReferenceTexture2D;

    public static async Task<T> LoadAsync<T>(AssetReference asset) {
        AsyncOperationHandle handle = Addressables.LoadAssetAsync<T>(asset);
        await handle.Task;
        if (handle.Status == AsyncOperationStatus.Succeeded)
            Debug.Log("Successfully loaded asset " + handle.Result);
        else
            Debug.LogError("Failed to load asset " + asset);

        return (T)handle.Result;
    }
    public static async Task<GameObject> LoadAsync(AssetReferenceGameObject asset) {
        AsyncOperationHandle handle = Addressables.LoadAssetAsync<GameObject>(asset);
        await handle.Task;
        if (handle.Status == AsyncOperationStatus.Succeeded)
            Debug.Log("Successfully loaded asset " + handle.Result);
        else
            Debug.LogError("Failed to load asset " + asset);

        return (GameObject)handle.Result;
    }


    public static void Unload(AssetReference asset) {
        Addressables.Release(asset);
    }
}
