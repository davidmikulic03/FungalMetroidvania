using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;


namespace Initialization
{
    public class Initialization
    {
        [RuntimeInitializeOnLoadMethod]
        public static void CreateGameInstance()
        {
            var resource = Resources.Load<GameObject>("GameInstance");
            var gameInstance = GameObject.Instantiate(resource);
            gameInstance.name = "GameInstance";
            var Comp = gameInstance.GetComponent<GameInstance>();
            Comp.Initialize();
        }
    }
}

public class GameInstance : MonoBehaviour
{
    public enum GameState {
        NONE = 0,
        LOADING,
        MAIN_MENU,
        SETTINGS,
        CREDITS,
        PLAYING,
        PAUSE
    }

    [SerializeField] AssetReferenceGameObject asset;

    private GameObject exampleAssetResource;

    private static GameState gameState = GameState.NONE;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }
    public void Initialize() {
        SetGameState(GameState.LOADING);
        exampleAssetResource = Utility.LoadAsync<GameObject>(asset);
    }
    private void Update()
    {
        Debug.Log(exampleAssetResource);
        switch (gameState) {
            case GameState.LOADING: {
                    if(exampleAssetResource != null) {
                        Instantiate(exampleAssetResource);
                    } 
                    break;
                }
        }
    }

    public static GameState GetGameState() {
        return gameState;
    }
    public static void SetGameState(GameState state) {
        gameState = state;
    }
}
