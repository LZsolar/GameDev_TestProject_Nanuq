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

    [Header("UI")]
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject DuringGame;
    [SerializeField] private GameObject EndMenu;
    [SerializeField] private GameObject ScoreMenu;
    [SerializeField] private GameObject Tutorial;


    [Header("CONFIG")]
    [SerializeField] public float PlayerJumpForce;
    [SerializeField] public float PlayerGravity;
 
    [SerializeField] List<GameDifficulty> _gameDifficulty;
    int currentGameDifficulty=0;

    public GameState currentGameState { get; private set; }

    public static event System.Action<GameState> OnGameStateChanged;
    public static event System.Action<GameDifficulty> OnGameDifficultyChanged;

    [Header("FOR TWEEN")]
    [SerializeField] GameObject TitleLogo;
    [SerializeField] Image fadeBlack;

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
                return;
            case GameState.End: 
                setUIonGameEnd();
                ScoreManager.Instance.resetScore();
                SoundManager.Instance.playAudio((int)audioName.GameEnd);
                return;
            default: setUImainmenu(); return;
        }
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
        changeScreen(
            () => MainMenu.SetActive(false),
            () => Tutorial.SetActive(true),
            () => DuringGame.SetActive(true)
        );

        currentGameDifficulty = 0;
        ToggleGameDifficulty(0);
    }
    private void setUIonGameStart()
    {
        Tutorial.SetActive(false);
        DuringGame.SetActive(true);
    }
    private void setUIonGameEnd()
    {
        DuringGame.SetActive(false);
        EndMenu.SetActive(true);
    }
    public void setUIhighscore()
    {
        changeScreen(
           () => MainMenu.SetActive(false),
           () => ScoreMenu.SetActive(true)
        );
    }
    private void setUImainmenu()
    {
        changeScreen(
           () => MainMenu.SetActive(true),
           () => EndMenu.SetActive(false),
           () => ScoreMenu.SetActive(false)
        );
    }

    private void changeScreen(params Action[] actions)
    {
        fadeBlack.gameObject.SetActive(true);
        Sequence Blackseq = Sequence.Create();
        Blackseq.Chain(Tween.Alpha(fadeBlack, 1, 0.2f));
        Blackseq.ChainCallback(() => {
            foreach (var act in actions)
                act?.Invoke();
        });
        Blackseq.Chain(Tween.Alpha(fadeBlack, 0, 0.2f));
        Blackseq.ChainCallback(() => {
            fadeBlack.gameObject.SetActive(false);
        });
        Blackseq.OnComplete (()=> Blackseq.Complete());
    }

}
