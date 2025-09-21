using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.InputSystem.Interactions;
using PrimeTween;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference _inputPress;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private SpriteRenderer playersprite;
    [SerializeField] private List<Sprite> playerSpritesList;

    private float _JumpForce;

    Sequence playerRotate;


    private void Awake()
    {
        GameManager.OnGameStateChanged += HandleGameState;
    }

    private void HandleGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start: OnGameStart(); return;
            case GameState.End: OnGameEnd(); return;
            default: resetGame(); return;
        }
    }

    void OnInput(InputAction.CallbackContext context)
    {
        _rigidbody.linearVelocity = new Vector2(0, 0);
        _rigidbody.AddForce(_JumpForce * Vector2.up);

        SoundManager.Instance.playAudio((int)audioName.BirdFly);
        playerRotation();
    }

    public void OnGameStart()
    {
        _inputPress.action.Enable();
        _inputPress.action.performed += OnInput;
        _inputPress.action.canceled += OnInput;
        _JumpForce = GameManager.Instance.PlayerJumpForce;
        _rigidbody.gravityScale = GameManager.Instance.PlayerGravity;
    }
    public void resetGame()
    {
        this.transform.position = new Vector3(-1f, 0, 0);
        _rigidbody.gravityScale = 0;
        playersprite.sprite = playerSpritesList[0];
        playerRotate.Stop();
        Tween.Rotation(playersprite.gameObject.transform, new Vector3(0, 0, 0), 0f);
    }
    public void OnGameEnd()
    {
        _inputPress.action.Disable();
        _inputPress.action.performed -= OnInput;
        _inputPress.action.canceled -= OnInput;
        playersprite.sprite = playerSpritesList[1];
    }

    void playerRotation()
    {
        playerRotate.Stop();
        playerRotate = Sequence.Create();
        playerRotate.Chain(Tween.Rotation(playersprite.gameObject.transform,new Vector3(0,0,50), 0));
        playerRotate.Chain(Tween.Rotation(playersprite.gameObject.transform, new Vector3(0, 0, -50), 2f));

        playerRotate.OnComplete(() => playerRotate.Complete());
    }

}
