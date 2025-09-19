using UnityEngine;

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
    [SerializeField] private float PlayerJumpForce;
    [SerializeField] private float PlayerGravity;
    [SerializeField] private float PipeSpeed;
    [SerializeField] private float PipeSpawnInterval;
    public float _PlayerJumpForce => PlayerJumpForce;
    public float _PlayerGravity => PlayerGravity;
    public float _PipeSpeed => PipeSpeed;
    public float _PipeSpawnInterval => PipeSpawnInterval;

    [SerializeField] private bool CurrentGameState = false;


    private void OnEnable()
    {
        setUImainmenu();
    }

    void Update()
    {
        
    }
    public void ToggleGameState(bool state)
    {
        CurrentGameState = state;
        if(CurrentGameState ) { setUIonGameStart(); }
        else { setUIonGameEnd(); }
    }

    void setUIonGameStart()
    {
        MainMenu.SetActive(false);
        _PlayerController.OnGameStart();
    }
    public void setUIonGameEnd()
    {
        EndMenu.SetActive(true);
        _PlayerController.OnGameEnd();
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
        _PlayerController.resetGame();
    }

    enum Gamestate
    {
        Start,
        End
    }
}
