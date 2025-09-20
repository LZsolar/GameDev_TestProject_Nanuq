using UnityEngine;
using UnityEngine.Events;

public enum GameState
{
    Waiting,
    Start,
    End
}
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    [Header("UI")]
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject EndMenu;
    [SerializeField] private GameObject ScoreMenu;


    [Header("CONFIG")]
    [SerializeField] private PlayerController _PlayerController;
    [SerializeField] public float PlayerJumpForce;
    [SerializeField] public float PlayerGravity;
    [SerializeField] public float PipeSpeed;
    [SerializeField] public float PipeSpawnInterval;

    public GameState currentGameState { get; private set; }

    public static event System.Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);

        ToggleGameState(GameState.Waiting);
    }
    public void ToggleGameState(GameState state)
    {
        Debug.Log("Current Game State = "+state);
        currentGameState = state;
        OnGameStateChanged?.Invoke(currentGameState);

        switch (state)
        {
            case GameState.Start: setUIonGameStart(); return;
            case GameState.End: setUIonGameEnd(); return;
            default: setUImainmenu(); return;
        }
    }
    public void clickToStartGame(){ToggleGameState(GameState.Start); }
    public void clickToRestartGame(){ToggleGameState(GameState.Waiting); }

    void setUIonGameStart()
    {
        MainMenu.SetActive(false);
    }
    public void setUIonGameEnd()
    {
        EndMenu.SetActive(true);
    }
    public void setUIhighscore()
    {
        MainMenu.SetActive(false);
        ScoreMenu.SetActive(true);
    }
    public void setUImainmenu()
    {
        MainMenu.SetActive(true);
        EndMenu.SetActive(false);
        ScoreMenu.SetActive(false);
    }
}
