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

    [SerializeField] private GameObject soundManager;

    private GameObject audioManager;

    private static GameState gameState = GameState.NONE;

    public void Initialize() {
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
