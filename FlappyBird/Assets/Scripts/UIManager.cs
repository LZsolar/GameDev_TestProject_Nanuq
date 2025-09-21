using PrimeTween;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;
using System;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance => _instance;

    [Header("UI")]
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject DuringGame;
    [SerializeField] private GameObject EndMenu;
    [SerializeField] private GameObject ScoreMenu;
    [SerializeField] private GameObject Tutorial;


    [Header("FOR TWEEN")]
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

        GameManager.OnGameStateChanged += ToggleUIwithGameState;
    }
    public void ToggleUIwithGameState(GameState state)
    {
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

    public void setUIonGameStart()
    {
        closeAllUI();
        DuringGame.SetActive(true);
    }
    public void setUIonGameEnd()
    {
        closeAllUI();
        EndMenu.SetActive(true);
    }
    public void setUIhighscore()
    {
        changeScreen(() => ScoreMenu.SetActive(true));
    }
    public void setUImainmenu()
    {
        changeScreen(() => MainMenu.SetActive(true));
    }
    public void setUITutorial()
    {
        changeScreen(() => Tutorial.SetActive(true));
    }
    private void closeAllUI()
    {
        MainMenu.SetActive(false);
        EndMenu.SetActive(false);
        ScoreMenu.SetActive(false);
        Tutorial.SetActive(false);
        DuringGame.SetActive(false);
    }

    private void changeScreen(params Action[] actions)
    {
        fadeBlack.gameObject.SetActive(true);
        Sequence Blackseq = Sequence.Create();
        Blackseq.Chain(Tween.Alpha(fadeBlack, 1, 0.2f));
        Blackseq.ChainCallback(() => {
            closeAllUI();
            foreach (var act in actions)
                act?.Invoke();
        });
        Blackseq.Chain(Tween.Alpha(fadeBlack, 0, 0.2f));
        Blackseq.ChainCallback(() => {
            fadeBlack.gameObject.SetActive(false);
        });
        Blackseq.OnComplete(() => Blackseq.Complete());
    }
}
