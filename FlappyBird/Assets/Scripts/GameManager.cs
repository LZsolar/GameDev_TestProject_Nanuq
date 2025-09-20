using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public enum GameState
{
    Waiting,
    Start,
    End
}
[Serializable]
public struct GameDifficulty
{
    public int ScoreToTrigger;
    public float PipeSpawnRange;
    public float PipeSpeed;
    public float PipeSpawnInterval;
}

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    [Header("UI")]
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject DuringGame;
    [SerializeField] private GameObject EndMenu;
    [SerializeField] private GameObject ScoreMenu;


    [Header("CONFIG")]
    [SerializeField] public float PlayerJumpForce;
    [SerializeField] public float PlayerGravity;
 
    [SerializeField] List<GameDifficulty> _gameDifficulty;
    int currentGameDifficulty=0;

    public GameState currentGameState { get; private set; }

    public static event System.Action<GameState> OnGameStateChanged;
    public static event System.Action<GameDifficulty> OnGameDifficultyChanged;


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
        currentGameState = state;
        OnGameStateChanged?.Invoke(currentGameState);

        switch (state)
        {
            case GameState.Start: 
                setUIonGameStart();
                currentGameDifficulty = 0;
                ToggleGameDifficulty(0);
                return;
            case GameState.End: 
                setUIonGameEnd();
                ScoreManager.Instance.resetScore(); 
                return;
            default: setUImainmenu(); return;
        }
    }
    public void ToggleGameDifficulty(int currentScore)
    {
        if (_gameDifficulty.Count <= currentGameDifficulty+1) { return; }
        if (currentScore < _gameDifficulty[currentGameDifficulty].ScoreToTrigger) { return; }

        currentGameDifficulty += 1;
        OnGameDifficultyChanged?.Invoke(_gameDifficulty[currentGameDifficulty]);
    }

    public void clickToStartGame(){ToggleGameState(GameState.Start); }
    public void clickToRestartGame(){ToggleGameState(GameState.Waiting); }

    private void setUIonGameStart()
    {
        MainMenu.SetActive(false);
        DuringGame.SetActive(true);
    }
    private void setUIonGameEnd()
    {
        DuringGame.SetActive(false);
        EndMenu.SetActive(true);
    }
    public void setUIhighscore()
    {
        MainMenu.SetActive(false);
        ScoreMenu.SetActive(true);
    }
    private void setUImainmenu()
    {
        MainMenu.SetActive(true);
        EndMenu.SetActive(false);
        ScoreMenu.SetActive(false);
    }
}
