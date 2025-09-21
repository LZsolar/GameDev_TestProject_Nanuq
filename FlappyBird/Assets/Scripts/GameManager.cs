using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine.UI;

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

    }
    public void ToggleGameDifficulty(int currentScore)
    {
        if (_gameDifficulty.Count <= currentGameDifficulty) { return; }
        if (currentScore < _gameDifficulty[currentGameDifficulty].ScoreToTrigger) { return; }

        OnGameDifficultyChanged?.Invoke(_gameDifficulty[currentGameDifficulty]);
        currentGameDifficulty += 1;
    }

    public void clickToStartGame(){ToggleGameState(GameState.Start);}
    public void clickToRestartGame(){ToggleGameState(GameState.Waiting); }

    public void setUpTutorial()
    {
        UIManager.Instance.setUITutorial();
        currentGameDifficulty = 0;
        ToggleGameDifficulty(0);
    }
}
