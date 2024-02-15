using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


namespace Initialization
{
    public class Initialization
    {
        [RuntimeInitializeOnLoadMethod]
        public static void CreateGameInstance()
        {
            var resource = Resources.Load<GameObject>("GameManager");
            var gameInstance = GameObject.Instantiate(resource);
            var Comp = gameInstance.GetComponent<GameManager>();
            Comp.Initialize();
        }
    }
}

public class GameManager : PersistentSingleton<GameManager> {
    public enum GameState {
        NONE = 0,
        LOADING,
        MAINMENU,
        SETTINGS,
        CREDITS,
        PLAYING,
        PAUSE
    }

    private static GameState gameState = GameState.NONE;

    public async void Initialize() {

        GameObject soundManager = await AssetManager.Load<GameObject>("Singletons/AudioManager");

        if(soundManager)
            Instantiate(soundManager);

    }
    private void Update() {
        
    }

    public static GameState GetGameState() {
        return gameState;
    }
    public static void SetGameState(GameState state) {
        gameState = state;
    }
}
