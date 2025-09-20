using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference _inputPress;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Collider2D _collider;

    private float _JumpForce;
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
        this.transform.position = new Vector3(-1f,0,0);
        _rigidbody.gravityScale = 0;
    }
    public void OnGameEnd()
    {
        _inputPress.action.Disable();
        _inputPress.action.performed -= OnInput;
        _inputPress.action.canceled -= OnInput;
    }

    
}
