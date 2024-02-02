using System.Collections.Generic;
using System.Threading.Tasks;
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
        MAINMENU,
        SETTINGS,
        CREDITS,
        PLAYING,
        PAUSE
    }

    [SerializeField] AssetReference asset;

    private Task<GameObject> assetLoadTask;

    private static GameState gameState = GameState.NONE;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }
    public void Initialize() {
        SetGameState(GameState.LOADING);
        assetLoadTask = Utility.LoadAsync<GameObject>(asset);
    }
    private void FixedUpdate() {

    }

    public static GameState GetGameState() {
        return gameState;
    }
    public static void SetGameState(GameState state) {
        gameState = state;
    }
}
