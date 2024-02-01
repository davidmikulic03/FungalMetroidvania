using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Utility
{
    private AssetReference _assetReference;
    private AssetReferenceGameObject _assetReferenceGameObject;
    private AssetReferenceSprite _assetReferenceSprite;
    private AssetReferenceTexture2D _assetReferenceTexture2D;

    public static T LoadAsync<T>(AssetReference asset) {
        T result = default(T);
        Addressables.LoadAssetAsync<T>(asset).Completed += (a) => {
                if (a.Status == AsyncOperationStatus.Succeeded) {
                    result = a.Result;
                    Debug.Log("Successfully loaded asset " + result.ToString());
                } else
                    Debug.LogError("Failed to load asset " + asset);
            }; return result;
    }

    public static GameObject LoadInstantiateAsync(AssetReferenceGameObject asset, Transform parent = null) {
        GameObject result = null;
        Addressables.InstantiateAsync(asset).Completed += (a) => {
            if (a.Status == AsyncOperationStatus.Succeeded) {
                result = a.Result;
                Debug.Log("Successfully loaded and instantiated asset " + result.ToString());
            }
            else
                Debug.LogError("Failed to load and instantiate asset " + asset);
        };
        if(result)
            result.transform.parent = parent;
        return result;
    }
    public static void Unload(AssetReference asset) {
        Addressables.Release(asset);
    }
}
