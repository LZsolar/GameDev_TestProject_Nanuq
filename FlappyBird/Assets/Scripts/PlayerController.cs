using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameManager _gameManager;

    [SerializeField] private InputActionReference _inputPress;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Collider2D _collider;

    private float _JumpForce;

    private void Start()
    {
        OnGameEnd();
    }

    void OnInput(InputAction.CallbackContext context)
    {
        _rigidbody.AddForce(new Vector2(0f,_JumpForce));
    }

    public void OnGameStart()
    {
        _inputPress.action.Enable();
        _inputPress.action.performed += OnInput;
        _inputPress.action.canceled += OnInput;
        _JumpForce = _gameManager._PlayerJumpForce;
        _rigidbody.gravityScale = _gameManager._PlayerGravity;
    }
    public void resetGame()
    {
        this.transform.position = new Vector3(-3.35f,0,0);
        _rigidbody.gravityScale = 0;
    }
    public void OnGameEnd()
    {
        _inputPress.action.Disable();
        _inputPress.action.performed -= OnInput;
        _inputPress.action.canceled -= OnInput;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            _gameManager.ToggleGameState(false);
            OnGameEnd();
        }
    }
}
